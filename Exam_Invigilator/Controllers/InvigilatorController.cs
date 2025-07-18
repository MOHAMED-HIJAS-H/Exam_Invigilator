using Exam_Invigilator.Domain.Interfaces;
using Exam_Invigilator.Domain.Models;
using Exam_Invigilator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Exam_Invigilator.Controllers
{
    public class InvigilatorController : Controller
    {
        private readonly IInvigilatorService _invigilatorService;

        public InvigilatorController(IInvigilatorService invigilatorService)
        {
            _invigilatorService = invigilatorService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string contact)
        {
            var invigilator = await _invigilatorService.FindByEmailAndContactAsync(email, contact);
            if (invigilator != null)
            {
                HttpContext.Session.SetInt32("InvigilatorId", invigilator.Id);
                HttpContext.Session.SetString("InvigilatorName", invigilator.Name);
                return RedirectToAction("MySchedule");
            }

            ViewBag.Error = "Invalid Credentials";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Invigilator invigilator, List<DayOfWeek> availabilities)
        {
            invigilator.Availabilities = availabilities.Select(day => new Availability { Day = day }).ToList();
            await _invigilatorService.AddInvigilatorAsync(invigilator);
            return RedirectToAction("Login");
        }


        [HttpGet]
        public async Task<IActionResult> MySchedule()
        {
            var id = HttpContext.Session.GetInt32("InvigilatorId");
            if (id == null)
                return RedirectToAction("Login");

            var schedule = await _invigilatorService.GetApprovedScheduleByInvigilatorAsync(id.Value);
            return View(schedule);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }

}
