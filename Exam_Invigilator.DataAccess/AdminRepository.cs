using Exam_Invigilator.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Invigilator.DataAccess
{
    public class AdminRepository
    {
        private readonly ExamDbContext _context;
        public AdminRepository(ExamDbContext context) => _context = context;

        public async Task<Admin> GetByCredentialsAsync(string username, string password)
        {
            return await _context.Admins.FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
        }
    }

}
