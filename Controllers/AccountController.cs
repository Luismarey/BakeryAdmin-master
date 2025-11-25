using BakeryAdmin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BakeryAdmin.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null)
            {
                var userName = await _userManager.GetUserNameAsync(user);
                if (string.IsNullOrEmpty(userName))
                {
                    ModelState.AddModelError(string.Empty, "Invalid login");
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(userName, password, false, false);
                if(result.Succeeded) return Redirect(returnUrl ?? "/");
            }
            ModelState.AddModelError(string.Empty, "Logeo invalido");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
