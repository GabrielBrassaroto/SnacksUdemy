using Microsoft.AspNetCore.Mvc;

namespace SnacksUdemy.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
