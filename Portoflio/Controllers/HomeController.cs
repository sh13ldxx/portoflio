using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portoflio.DAL;
using Portoflio.Models;
using Portoflio.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Portoflio.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task <IActionResult> Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Socials = await _context.Socials.ToListAsync(),
                Skills = await _context.Skills.Include(x=>x.SkillSettings).ToListAsync(),
                Interests=await _context.Interests.ToListAsync(),
                Abouts=await _context.About.ToListAsync(),
                Expeirences=await _context.Expeirences.ToListAsync(),
                Projects=await _context.Projects.ToListAsync(),
                //                SkillName = await _context.SkillSettings.Include(s => s.Skills).ToListAsync(),
                //Skills = await _context.Skills.Include(s => s.SkillSettings).ToListAsync()
            };
            return View(homeVm);
        }

    }
}
