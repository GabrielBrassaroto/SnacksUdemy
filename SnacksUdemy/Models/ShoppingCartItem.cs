using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnacksUdemy.Models
{
    [Table("ShoppingCartItens")]
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public Snack Snack { get; set; }

        public int Quantity { get; set; }

        [StringLength(200)]
        public string ShoppingCartId { get; set; }
    }
}
