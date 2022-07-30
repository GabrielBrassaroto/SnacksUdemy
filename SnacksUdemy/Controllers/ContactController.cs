using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SnacksUdemy.Controllers
{
    [Authorize(Roles = "Admin")]///validation controller identity
    public class ContactController : Controller
    {
      
        [AllowAnonymous]//allow unauthenticated users
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) { return View(); }   
            return RedirectToAction("Login","Account");
        }
    }
}
