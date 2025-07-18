using Exam_Invigilator.DataAccess;
using Exam_Invigilator.Domain.Interfaces;
using Exam_Invigilator.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam_Invigilator.Service
{
    public class AdminService : IAdminService
    {
        private readonly AdminRepository _adminRepo;
        public AdminService(AdminRepository repo) => _adminRepo = repo;

        public async Task<Admin> LoginAsync(string username, string password)
        {
            return await _adminRepo.GetByCredentialsAsync(username, password);
        }
    }
}
