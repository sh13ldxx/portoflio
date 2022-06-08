using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portoflio.DAL;
using Portoflio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portoflio.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class HomeController : Controller
    {
        public AppDbContext _context { get;  }

        public HomeController(AppDbContext context)
        {
            _context=context;
        }


        public async Task<IActionResult> Index()
        {
            List<Social> Socials = await _context.Socials.ToListAsync();
            return View(Socials);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Social socials)
        {
            if (socials!=null)
            {
                await _context.AddAsync(socials);
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
            var WillDeleted = _context.Socials.Find(id);
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
        public async Task<IActionResult> Update(int id)
        {
            Social social = await _context.Socials.FindAsync(id);
            return View(social);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(Social social)
        {
            var ExistDb = _context.Socials.FirstOrDefault(c => c.Id == social.Id);
            if (ExistDb == null) return NotFound();
            ExistDb.Key=social.Key;
            ExistDb.Value=social.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
