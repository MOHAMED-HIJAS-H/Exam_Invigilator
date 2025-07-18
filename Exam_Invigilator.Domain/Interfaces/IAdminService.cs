using Exam_Invigilator.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Invigilator.Domain.Interfaces
{
    public interface IAdminService
    {
        Task<Admin> LoginAsync(string username, string password);
    }
}
