using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portoflio.DAL;
using Portoflio.Models;
using Portoflio.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portoflio.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class SkillController : Controller
    {
        public AppDbContext _context { get; }
        public IHostingEnvironment _env;

        public SkillController(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            HomeVm homeVm = new HomeVm
            {
                Socials = await _context.Socials.ToListAsync(),
                Skills = await _context.Skills.Include(n => n.SkillSettings).ToListAsync()
                //                SkillName = await _context.SkillSettings.Include(s => s.Skills).ToListAsync(),
                //Skills = await _context.Skills.Include(s => s.SkillSettings).ToListAsync()
            };
            return View(homeVm);
        }

        public async Task<IActionResult> Update(int id)
        {
            return View();
        }

        public async Task<IActionResult> Images(int id)
        {
            ViewBag.SkillId = id;
            return View();
        }

        public async Task<IActionResult> GetImages(int id)
        {
            List<SkillImageVm> skillImages = new List<SkillImageVm>();

            var files = await _context.SkillSettings.Where(s => s.SkillsId == id).ToListAsync();

            foreach (var file in files)
            {
                var fileUploadedPath = Path.Combine("assets/uploads", file.Image);
                var filePath = Path.Combine(_env.WebRootPath, fileUploadedPath);

                if (System.IO.File.Exists(filePath))
                {
                    skillImages.Add(new SkillImageVm
                    {
                        Name = file.Image,
                        Path = $"/{fileUploadedPath}",
                        Size = new FileInfo(filePath).Length
                    });
                }

            }

            return Ok(skillImages);
        }


        [HttpPost]
        public async Task<IActionResult> RemoveImage(string fileName)
        {
            var skillImage = await _context.SkillSettings.Where(n => n.Image == fileName).FirstOrDefaultAsync();
            if (skillImage is null) return BadRequest();
            var filePath = Path.Combine(_env.WebRootPath, "assets/uploads", skillImage.Image);

            _context.SkillSettings.Remove(skillImage);
            await _context.SaveChangesAsync();

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            return Ok("Image successfull deleted.");
        }

        [HttpPost]
        public async Task<IActionResult> Images(int id, List<IFormFile> files)
        {
            var uploads = Path.Combine(_env.WebRootPath, "assets/uploads");
            List<SkillSettings> skillSettings = new List<SkillSettings>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        skillSettings.Add(new Models.SkillSettings
                        {
                            SkillsId = id,
                            Image = fileName
                        });
                    }
                }
            }

            await _context.SkillSettings.AddRangeAsync(skillSettings);
            await _context.SaveChangesAsync();

            return Ok("Images Uploaded Successfull.");
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Skills Skill)
        {
            if (Skill != null)
            {
                await _context.AddAsync(Skill);
                await _context.SaveChangesAsync();
                TempData["flashType"] = "success";
                TempData["flashMessage"] = "Succesful Added";
                return RedirectToAction("Index");
            }
            TempData["flashType"] = "error";
            TempData["flashMessage"] = "Something went wrong";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id )
        {
            var WillDeleted = _context.Skills.Find(id);
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
