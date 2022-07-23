using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Models;

namespace SnacksUdemy.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager; 
            _signInManager = signInManager; 
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl   
            });
        }

        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if(!ModelState.IsValid)
                return View(loginVM);

            //find user  by name
            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                //user, password, false cook de entrada and login failed not block account user
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to login");
            return View(loginVM);
        }
    }
}
