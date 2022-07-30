using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Models;
using SnacksUdemy.Repositories.Interfaces;

namespace SnacksUdemy.Controllers
{
    public class RequestController : Controller
    {
        private readonly IRequestRepository _requestRepository;

        private readonly ShoppingCart _shoppingCart;
        public RequestController(IRequestRepository requestRepository, ShoppingCart shoppingCart)
        {
            _requestRepository = requestRepository;
            _shoppingCart = shoppingCart;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout(Request request)
        {
            int totalItensRequest = 0;
            decimal priceTotalRequest = 0.0m;

            //get items from shopping cart
            List<ShoppingCartItem> items = _shoppingCart.GetShoppingCartItens();
            _shoppingCart.ShoppingCartItems = items;

            //check if you have items in your cart
            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                //modelstate add message in validation model
                ModelState.AddModelError("", "Your cart is empty add an item to cart...");
            }

            //calculate cart value
            foreach (var item in items)
            {
                totalItensRequest += item.Quantity;
                priceTotalRequest += (item.Snack.Price * item.Quantity);
            }


            request.OrderTotalItens = totalItensRequest;
            request.OrderTotal = priceTotalRequest;

            //validate order data
            if (ModelState.IsValid)
            {
                //create  request and details
                _requestRepository.CreateRequest(request);

                //define messages client
                ViewBag.CheckoutCompleteMessage = "Thank you for your request";
                ViewBag.TotalRequest = _shoppingCart.GetCartBuyTotal();

                ///clear cart shopping cart
                _shoppingCart.CleanCart();

                return View("~/Views/Request/CheckoutCompleto.cshtml", request);
            }
            return View(request);
        }
    }
}
