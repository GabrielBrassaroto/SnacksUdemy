using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Models;
using System.Diagnostics;

namespace SnacksUdemy.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            TempData["Name"] = "Gabriel Brassaroto";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}