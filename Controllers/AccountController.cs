using BakeryAdmin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BakeryAdmin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Buscar al usuario por su Email
            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user != null)
            {
                //Iniciar sesion usando el UserName real;
                var result = await _signInManager.PasswordSignInAsync(
                    user.UserName,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false
                );
                
                if(result.Succeeded)
                {
                    if(user.MustChangePassword)
                    {
                        return RedirectToAction("ChangePasswordFirstTime", "Account", new { userId = user.Id });
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");                        }
                    }
                }
            }
            // Aqui el login fallo
            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            return View(Model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if(Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult ChangePasswordFirstTime(string userId)
        {
            var model = new ChangePasswordViewModel { userId = userId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordFirstTime(ChangePasswordViewModel model) 
        {
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null) 
            {
                return NotFound();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded) 
            {
                user.MustChangePassword = false;
                await _userManager.UpdateAsync(user);

                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AcessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        
    }
}
