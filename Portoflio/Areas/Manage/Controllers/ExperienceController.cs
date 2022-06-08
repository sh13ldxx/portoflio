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
    public class ExperienceController : Controller
    {
            
        public AppDbContext _context { get; }

        public ExperienceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Expeirence> Socials = await _context.Expeirences.ToListAsync();
            return View(Socials);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Expeirence Expeirence)
        {
            if (Expeirence != null)
            {
                await _context.AddAsync(Expeirence);
                await _context.SaveChangesAsync();
                TempData["flashType"] = "success";
                TempData["flashMessage"] = "Succesful Added";
                return RedirectToAction("Index");
            }
            TempData["flashType"] = "error";
            TempData["flashMessage"] = "Something went wrong";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var WillDeleted = _context.Expeirences.Find(id);
            if (WillDeleted != null)
            {
                _context.Remove(WillDeleted);
                await _context.SaveChangesAsync();
                TempData["flashType"] = "success";
                TempData["flashMessage"] = "Deleted Permamently.";
                return RedirectToAction(nameof(Index));
            }
            TempData["flashType"] = "error";
            TempData["flashMessage"] = "Something went wrong.";
            return RedirectToAction(nameof(Index));
        }
    }
}
