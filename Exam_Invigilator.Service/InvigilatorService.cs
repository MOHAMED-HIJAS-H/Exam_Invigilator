using Microsoft.EntityFrameworkCore;
using Exam_Invigilator.Domain.Interfaces;
using Exam_Invigilator.Domain.Models;
using Exam_Invigilator.DataAccess; 

namespace Exam_Invigilator.Service
{
    public class InvigilatorService : IInvigilatorService
    {
        private readonly ExamDbContext _context;
        public InvigilatorService(ExamDbContext context)
        {
            _context = context;
        }

        public async Task<List<Invigilator>> GetAllAsync()
        {
            return await _context.Invigilators.Include(i => i.Availabilities).ToListAsync();
        }

        public async Task AddInvigilatorAsync(Invigilator invigilator)
        {
            _context.Invigilators.Add(invigilator);
            await _context.SaveChangesAsync();
        }

 

        public async Task<List<Allocation>> GenerateDraftScheduleAsync()
        {
            // Only fetch venues and invigilators
            var venues = await _context.Venues.ToListAsync();
            var invigilators = await _context.Invigilators.Include(i => i.Availabilities).ToListAsync();

            var draft = new List<Allocation>();

            foreach (var day in Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>())
            {
                var usedInvigilators = new HashSet<int>();

                foreach (var venue in venues)
                {
                    var availableInv = invigilators
                        .FirstOrDefault(i =>
                            i.Availabilities.Any(a => a.Day == day) &&
                            !usedInvigilators.Contains(i.Id) &&
                            !_context.Allocations.Any(a =>
                                a.Day == day &&
                                a.VenueId == venue.Id &&
                                a.InvigilatorId == i.Id &&
                                !a.IsApproved)); // Don't add if already exists as unapproved

                    if (availableInv != null)
                    {
                        draft.Add(new Allocation
                        {
                            Day = day,
                            VenueId = venue.Id,
                            InvigilatorId = availableInv.Id,
                            IsApproved = false
                        });

                        usedInvigilators.Add(availableInv.Id);
                    }
                }
            }

            await _context.Allocations.AddRangeAsync(draft);
            await _context.SaveChangesAsync();
            return draft;
        }



        public async Task ApproveScheduleAsync()
        {
            var drafts = await _context.Allocations.Where(a => !a.IsApproved).ToListAsync();
            drafts.ForEach(a => a.IsApproved = true);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvigilatorAsync(int id)
        {
            var inv = await _context.Invigilators.Include(i => i.Availabilities).FirstOrDefaultAsync(i => i.Id == id);
            if (inv != null)
            {
                _context.Availabilities.RemoveRange(inv.Availabilities);
                _context.Invigilators.Remove(inv);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Invigilator> GetByIdAsync(int id)
        {
            return await _context.Invigilators
                .Include(i => i.Availabilities)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdateInvigilatorAsync(Invigilator invigilator, List<DayOfWeek> selectedDays)
        {
            var existing = await _context.Invigilators
                .Include(i => i.Availabilities)
                .FirstOrDefaultAsync(i => i.Id == invigilator.Id);

            if (existing != null)
            {
                existing.Name = invigilator.Name;
                existing.Contact = invigilator.Contact;

                // Replace availability
                _context.Availabilities.RemoveRange(existing.Availabilities);
                existing.Availabilities = selectedDays.Select(day => new Availability { Day = day }).ToList();

                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> HasUnapprovedDraftAsync()
        {
            return await _context.Allocations.AnyAsync(a => !a.IsApproved);
        }

        public async Task DeleteDraftAsync()
        {
            var unapproved = await _context.Allocations.Where(a => !a.IsApproved).ToListAsync();
            _context.Allocations.RemoveRange(unapproved);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Allocation>> GetApprovedAllocationsAsync()
        {
            return await _context.Allocations
                .Include(a => a.Invigilator)
                .Include(a => a.Venue)
                .Where(a => a.IsApproved)
                .ToListAsync();
        }

        // ✅ 1. For Login: Find invigilator by Email and Contact
        public async Task<Invigilator?> FindByEmailAndContactAsync(string name, string contact)
        {
            return await _context.Invigilators
                .FirstOrDefaultAsync(i => i.Name == name && i.Contact == contact);
        }


        // ✅ 2. Get approved schedule for the logged-in invigilator
        public async Task<List<Allocation>> GetApprovedScheduleByInvigilatorAsync(int invigilatorId)
        {
            return await _context.Allocations
                .Include(a => a.Venue)
                .Include(a => a.Invigilator)
                .Where(a => a.InvigilatorId == invigilatorId && a.IsApproved)
                .ToListAsync();
        }





    }
}
