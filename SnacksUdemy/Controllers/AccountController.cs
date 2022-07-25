using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Models;

namespace SnacksUdemy.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            //find user with name 
            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            //if se user diferent null 
            if (user != null)
            {
                //usuario, password and cookies close navegador false and block user if password incorrect
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
               //if se result true return view index snacks
                if (result.Succeeded)
                {
                    //verification se exists value in  ReturnUrl
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Failed Login ");
            return View(loginVM);
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = registroVM.UserName };
                var result = await _userManager.CreateAsync(user,registroVM.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Register", "Failed realize the register");
                }
            }
             return View(registroVM);
        }



        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
