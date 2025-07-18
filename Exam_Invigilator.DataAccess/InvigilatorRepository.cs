using Exam_Invigilator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Invigilator.DataAccess
{
    public class InvigilatorRepository
    {
        private readonly ExamDbContext _context;
        public InvigilatorRepository(ExamDbContext context) => _context = context;

        public async Task AddAsync(Invigilator invigilator)
        {
            _context.Invigilators.Add(invigilator);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Invigilator>> GetAllAsync()
        {
            return await _context.Invigilators.Include(i => i.Availabilities).ToListAsync();
        }
    }

}
