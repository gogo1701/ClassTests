using ClassTests.Data;
using ClassTests.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClassTests.Controllers
{
    public class WeekController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeekController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var schedule = await _context.Tests.ToListAsync();
            return View(schedule);
        }

        [Authorize]
        public async Task<IActionResult> ViewTask(int id)
        {
            var item = await _context.Tests.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _context.Tests.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            var days = new List<SelectListItem>
        {
            new SelectListItem { Value = "Monday", Text = "Monday" },
            new SelectListItem { Value = "Tuesday", Text = "Tuesday" },
            new SelectListItem { Value = "Wednesday", Text = "Wednesday" },
            new SelectListItem { Value = "Thursday", Text = "Thursday" },
            new SelectListItem { Value = "Friday", Text = "Friday" }
        };

            ViewBag.Days = days;

            return View(task);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Test model)
        {
            if (ModelState.IsValid)
            {
                _context.Tests.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }

            return View(model);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Test newItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newItem);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Tests.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Tests.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestExists(int id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }

    }
}
