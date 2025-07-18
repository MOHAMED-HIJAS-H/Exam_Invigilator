using Microsoft.AspNetCore.Mvc;
using Exam_Invigilator.Domain.Interfaces;
using Exam_Invigilator.Domain.Models;

namespace Exam_Invigilator.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IInvigilatorService _invigilatorService;

        public AdminController(IAdminService adminService, IInvigilatorService invigilatorService)
        {
            _adminService = adminService;
            _invigilatorService = invigilatorService;
        }

        // ---------- Login ----------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var admin = await _adminService.LoginAsync(username, password);
            if (admin != null)
            {
                HttpContext.Session.SetString("AdminUser", admin.Username);
                return RedirectToAction("Index");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ---------- Dashboard ----------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var invigilators = await _invigilatorService.GetAllAsync();
            var hasDraft = await _invigilatorService.HasUnapprovedDraftAsync();
            ViewBag.HasDraft = hasDraft;
            return View(invigilators);
        }

        // ---------- Add Invigilator ----------
        [HttpGet]
        public IActionResult AddInvigilator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddInvigilator(Invigilator invigilator, List<DayOfWeek> SelectedDays)
        {
            invigilator.Availabilities = SelectedDays
                .Select(day => new Availability { Day = day })
                .ToList();

            await _invigilatorService.AddInvigilatorAsync(invigilator);
            return RedirectToAction("Index");
        }

        // ---------- Edit Invigilator ----------
        [HttpGet]
        public async Task<IActionResult> EditInvigilator(int id)
        {
            var inv = await _invigilatorService.GetByIdAsync(id);
            if (inv == null) return NotFound();
            return View(inv);
        }

        [HttpPost]
        public async Task<IActionResult> EditInvigilator(Invigilator updatedInvigilator, List<DayOfWeek> SelectedDays)
        {
            await _invigilatorService.UpdateInvigilatorAsync(updatedInvigilator, SelectedDays);
            return RedirectToAction("Index");
        }

        // ---------- Delete Invigilator ----------
        [HttpPost]
        public async Task<IActionResult> DeleteInvigilator(int id)
        {
            await _invigilatorService.DeleteInvigilatorAsync(id);
            return RedirectToAction("Index");
        }

        // ---------- Generate Draft ----------
        [HttpPost]
        public async Task<IActionResult> GenerateDraft()
        {
            var hasDraft = await _invigilatorService.HasUnapprovedDraftAsync();
            if (hasDraft)
            {
                TempData["Warning"] = "❗ A draft schedule already exists. Please approve or delete it before generating a new one.";
                return RedirectToAction("Index");
            }

            var draft = await _invigilatorService.GenerateDraftScheduleAsync();
            return View("DraftSchedule", draft);
        }

        // ---------- Approve Draft ----------
        [HttpPost]
        public async Task<IActionResult> ApproveSchedule()
        {
            await _invigilatorService.ApproveScheduleAsync();
            TempData["Success"] = "✅ Schedule approved successfully.";
            return RedirectToAction("Index");
        }

        // ---------- Delete Draft ----------
        [HttpPost]
        public async Task<IActionResult> DeleteDraft()
        {
            await _invigilatorService.DeleteDraftAsync();
            TempData["Success"] = "✅ Draft schedule deleted successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ScheduleByDay()
        {
            var schedule = await _invigilatorService.GetApprovedAllocationsAsync(); // only approved
            return View("ScheduleByDay", schedule);
        }

    }
}
