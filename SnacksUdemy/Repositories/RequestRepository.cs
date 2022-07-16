using SnacksUdemy.Models;
using SnacksUdemy.Repositories.Interfaces;

namespace SnacksUdemy.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public RequestRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        void IRequestRepository.CreateRequest(Request request)
        {
            request.DateRequest = DateTime.Now;
            _appDbContext.Requests.Add(request);
            _appDbContext.SaveChanges();

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var cartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Lenght = cartItem.Quantity,
                    SnackId = cartItem.Snack.SnackId,
                    RequestId = request.RequestId,
                    Price   =   cartItem.Snack.Price

                };
                _appDbContext.OrderDetails.Add(orderDetail);
            }
            _appDbContext.SaveChanges();
        }
    }
}
