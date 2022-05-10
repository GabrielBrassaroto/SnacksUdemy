using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Repositories.Interfaces;

namespace SnacksUdemy.Controllers
{
    public class SnackController : Controller
    {
        private readonly ISnackRepository _snackRepository;

        public SnackController(ISnackRepository snackRepository)
        {
            _snackRepository = snackRepository;
        }

        public IActionResult List()
        {
            ViewData["Titulo"] = "All Snacks";
            ViewData["Date"] = DateTime.Now;
            var snacks = _snackRepository.Snacks;
            var totalSnacks = snacks.Count();
            ViewBag.Total = "Total Snacks";
            ViewBag.TotalSnacks = totalSnacks;

            return View(snacks);
        }
    }
}
