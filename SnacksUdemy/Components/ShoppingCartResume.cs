using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Models;
using SnacksUdemy.ViewModels;

namespace SnacksUdemy.Components
{
    public class ShoppingCartResume : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartResume(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            // var itens = _shoppingCart.GetShoppingCartItens();

            var itens = new List<ShoppingCartItem>()
            {
                new ShoppingCartItem(),
                new ShoppingCartItem()
            };

            _shoppingCart.ShoppingCartItems = itens;

            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetCartBuyTotal()
            };

            return View(shoppingCartVM);
        }
    }
}
