using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portoflio.DAL;
using Portoflio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portoflio.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class AboutController : Controller
    {
        public AppDbContext  _context { get; }
        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<About> abouts = await _context.About.ToListAsync();
            return View(abouts);
        }
        public async Task<IActionResult> Edit(int id)
        {
            About abouts = await _context.About.FindAsync(id);
            return View(abouts);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(About abouts)
        {
            var ExistDb = await _context.About.FirstOrDefaultAsync(x=>x.Id== abouts.Id);
            if (ExistDb == null) return NotFound();
            ExistDb.about = abouts.about;
            ExistDb.Image = abouts.Image;
            ExistDb.Email = abouts.Email;
            ExistDb.Email=abouts.Email;
            ExistDb.Birthday = abouts.Birthday;
            ExistDb.Phone = abouts.Phone;
            TempData["flashType"] = "success";
            TempData["flashMessage"] = "Updated";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
