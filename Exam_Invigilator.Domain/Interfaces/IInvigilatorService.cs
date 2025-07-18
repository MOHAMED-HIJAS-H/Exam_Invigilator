using System.Collections.Generic;
using System.Threading.Tasks;
using Exam_Invigilator.Domain.Models;

namespace Exam_Invigilator.Domain.Interfaces
{
    public interface IInvigilatorService
    {
        Task<List<Invigilator>> GetAllAsync();
        Task AddInvigilatorAsync(Invigilator invigilator);
        Task<List<Allocation>> GenerateDraftScheduleAsync();
        Task ApproveScheduleAsync();
        Task DeleteInvigilatorAsync(int id);
        Task<Invigilator> GetByIdAsync(int id);
        Task UpdateInvigilatorAsync(Invigilator invigilator, List<DayOfWeek> selectedDays);
        Task<bool> HasUnapprovedDraftAsync();
        Task DeleteDraftAsync();

        Task<List<Allocation>> GetApprovedAllocationsAsync();

        Task<Invigilator> FindByEmailAndContactAsync(string email, string contact);
        Task<List<Allocation>> GetApprovedScheduleByInvigilatorAsync(int invigilatorId);

    }
}
