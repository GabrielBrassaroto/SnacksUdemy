using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Models;
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

        public IActionResult List(string category)
        {
            IEnumerable<Snack> snacks;
            string categoryCurrent = string.Empty;
            if (string.IsNullOrEmpty(category))
            {

                snacks = _snackRepository.Snacks.OrderBy(x => x.SnackId);
                categoryCurrent = "All Snacks";
            }
            else
            {
                if (string.Equals("Normal", category, StringComparison.OrdinalIgnoreCase))
                {
                    snacks = _snackRepository.Snacks.Where(x => x.Category.
                    CategoryName.Equals("Normal")).OrderBy(x => x.Name);
                }
                else
                {
                    snacks = _snackRepository.Snacks.Where(x => x.Category.
                    CategoryName.Equals("Natural")).OrderBy(x => x.Name);
                }
                categoryCurrent = category;
            }

            var snacksLisViewModel = new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = categoryCurrent
            };
            return View(snacksLisViewModel);

            //var snacks = _snackRepository.Snacks;
            //return View(snacks);
        }
    }
}
