using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portoflio.Models;
using Portoflio.ViewModel.Authorzation;
using System.Threading.Tasks;

namespace Portoflio.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
        {
            _userManager = userManager;
            _signManager = signManager; 
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm register)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser newUser=new AppUser
            {
                Name =register.FirstName,
                Surname =register.LastName,
                UserName=register.username,
                Email= register.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser,register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            await _signManager.SignInAsync(newUser, true);

            return RedirectToAction("Index", "Home", new
            {
                area = "Manage"
            }); 

        }
        public async Task<IActionResult> SignOut()
        {
            await _signManager.SignOutAsync();
            return RedirectToAction("Register");
        }
        public async Task<IActionResult> SignIn()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(AppUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signManager.PasswordSignInAsync(user.Email, user.PasswordHash, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }
    }
}
