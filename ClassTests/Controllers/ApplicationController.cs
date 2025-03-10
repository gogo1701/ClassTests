using ClassTests.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ClassTests.Data;
using ClassTests.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using ClassTests.Models.ViewModels;

namespace ClassTests.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ApplicationController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            Application? application = _context.Applications.FirstOrDefault(x => x.UserId == userId);
            ApplicationIndexViewModel viewModel = new();

            if (application != null)
            {
                viewModel.doesApplicationExist = true;

                return View(viewModel);
            }
            else
            {
                viewModel.doesApplicationExist = false;

                return View(viewModel);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Status()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            Application? application = _context.Applications.FirstOrDefault(x => x.UserId == userId);
            ApplicationIndexViewModel viewModel = new();

            if (application != null)
            {

                viewModel.Application = application;
                viewModel.doesApplicationExist = true;

                return View(viewModel);
            }
            else
            {
                viewModel.doesApplicationExist = false;

                return View(viewModel);
            }

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ApplicationIndexViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            model.Application.UserId = user.Id;
            model.Application.Status = ApplicationStatus.PENDING;

            _context.Applications.Add(model.Application);
            await _context.SaveChangesAsync();

            return RedirectToAction("Status");
        }
    }
}
