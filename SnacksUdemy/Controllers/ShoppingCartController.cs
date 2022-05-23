using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Models;
using SnacksUdemy.Repositories.Interfaces;
using SnacksUdemy.ViewModels;

namespace SnacksUdemy.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ISnackRepository _snackRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(ISnackRepository snackRepository,
           ShoppingCart shoppingCart)
        {
            _snackRepository = snackRepository;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
          var itens = _shoppingCart.GetShoppingCartItens();

            _shoppingCart.ShoppingCartItems = itens;

            var shoppingCartVM = new  ShoppingCartViewModel{
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetCartBuyTotal()


            };
            return View("Index",shoppingCartVM);
        }


        public IActionResult AddItemInShoppingCart(int snackID)
        {
            var snackSelected = _snackRepository.Snacks
                .FirstOrDefault(p => p.SnackId == snackID);
            if (snackSelected != null)
            {
                _shoppingCart.AddCart(snackSelected);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveItemInShoppingCart()
        {
            return View();
        }
    }
}
