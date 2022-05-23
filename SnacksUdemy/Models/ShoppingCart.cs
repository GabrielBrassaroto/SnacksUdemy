using Microsoft.EntityFrameworkCore;

namespace SnacksUdemy.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }


        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            //utiliza a servicer para setar  a sessao 
            //define a sessão IHttpContextAccessor não for null ele cria a sesão do htppcontext
            // o ? avalia se o GetRequiredService tem valor e for diferente null ele obtem a sessão 
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            
            //obtem um service do tipo do nosso context que foi que foi injetado no container de dependencias
            var context = services.GetService<AppDbContext>();


            //obtem  o id do carrinho na sessao ou se for null gera um id de carrinho 
            string cartId = session.GetString("ShoppingCartId") ?? Guid.NewGuid().ToString();

            //atribui o id do carrinho na sessão
            session.SetString("ShoppingCartId", cartId);


            //retorna o carrinho com o contexto atribuido ou valor 
            return new ShoppingCart(context) {

                ShoppingCartId = cartId

            };
        }


        public void AddCart (Snack snack)
        {
            var shoppingCartItem = _context.ShoppingCartItens.SingleOrDefault(
                 s => s.Snack.SnackId == snack.SnackId &&
                s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Snack = snack,
                    Quantity = 1

                };
                _context.ShoppingCartItens.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public void  RemoveCart(Snack snack)
        {
            var shoppingCartItem = _context.ShoppingCartItens.SingleOrDefault(
                 s => s.Snack.SnackId == snack.SnackId &&
                s.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem != null)
            {
                if(shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                  
                }
            }
            else
            {
                 _context.ShoppingCartItens.Remove(shoppingCartItem);
            }
            _context.SaveChanges();

        }

        public List<ShoppingCartItem> GetShoppingCartItens()
        {
            return ShoppingCartItems ?? (ShoppingCartItems
                = _context.ShoppingCartItens.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Snack)
                .ToList());
        }

        public void CleanCart()
        {
            var cartItens = _context.ShoppingCartItens
                .Where(c => c.ShoppingCartId == ShoppingCartId);

            _context.ShoppingCartItens.RemoveRange(cartItens);
            _context.SaveChanges();
        }

        public decimal GetCartBuyTotal()
        {
            return  _context.ShoppingCartItens
                .Where(_c => _c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Snack.Price * c.Quantity).Sum();
        }

    }
}
