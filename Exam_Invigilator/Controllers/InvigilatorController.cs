using Exam_Invigilator.Domain.Interfaces;
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
            var inv = await _invigilatorService.FindByEmailAndContactAsync(email, contact);
            if (inv != null)
            {
                HttpContext.Session.SetInt32("InvigilatorId", inv.Id);
                return RedirectToAction("MySchedule");
            }

            ViewBag.Error = "Invalid Credentials";
            return View();
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
