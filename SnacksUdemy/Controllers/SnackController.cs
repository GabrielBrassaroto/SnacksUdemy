using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Repositories.Interfaces;
using SnacksUdemy.ViewModels;

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
            var snacksLisViewModel = new SnackListViewModel();
            snacksLisViewModel.Snacks = _snackRepository.Snacks;
            snacksLisViewModel.CurrentCategory = "Category Current";
            return View(snacksLisViewModel);

            //var snacks = _snackRepository.Snacks;
            //return View(snacks);
        }
    }
}
