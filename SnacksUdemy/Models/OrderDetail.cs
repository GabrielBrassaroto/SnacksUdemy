using System.ComponentModel.DataAnnotations.Schema;

namespace SnacksUdemy.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        //key foreign 
        public int RequestId { get; set; }
        //key foreign 
        public int SnackId { get; set; }
        public int Lenght { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        //key foreign 
        public virtual Snack Snack { get; set; }
        //key foreign 
        public virtual Request Request { get; set; }
    }
}
