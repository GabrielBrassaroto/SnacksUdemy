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

                snacks = _snackRepository.Snacks.Where(
                    x => x.Category.CategoryName.Equals(category))
                    .OrderBy(c => c.Name);
                categoryCurrent = category;
            }




            var snacksLisViewModel = new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = categoryCurrent
            };

            return View(snacksLisViewModel);
        }

        public ActionResult Details(int snackId)
        {
           var snack = _snackRepository.Snacks.FirstOrDefault( l => l.SnackId == snackId );
            return View(snack);
        }

        public ViewResult Search(string seachString)
        {
            IEnumerable<Snack> snacks;
            string categoryCurrent = string.Empty;

            if (string.IsNullOrEmpty(seachString))
            {
                snacks = _snackRepository.Snacks.OrderBy(p => p.SnackId);
                categoryCurrent = "All Snacks";

            }
            else
            {
                snacks = _snackRepository.Snacks.Where(p => p.Name.ToLower()
                .Contains(seachString.ToLower()));
                if (snacks.Any())
                {
                    categoryCurrent = "Snack";
                }
                else
                {
                    categoryCurrent = "No snacks were found ";
                }
            }

            return View("~/Views/Snack/List.cshtml", new SnackListViewModel {
                Snacks = snacks,
                CurrentCategory = categoryCurrent});
        }

    }
}
