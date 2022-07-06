using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnacksUdemy.Models
{
    public class Request
    {

        public int RequestId { get; set; }

        [Required(ErrorMessage = "Inform Name")]
        [StringLength(50)]

        public string Name { get; set; }

        [Required(ErrorMessage = "Inform LastName")]
        [StringLength(50)]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Inform Adress")]
        [StringLength(100)]
        [Display(Name = "Adress")]
        public string Adress { get; set; }


        [Required(ErrorMessage = "Inform Complement")]
        [StringLength(100)]
        [Display(Name = "Complement")]
        public string? Complement { get; set; }


        [Required(ErrorMessage = "Inform CEP")]
        [StringLength(10,MinimumLength =8)]
        [Display(Name ="CEP")]

        public string Cep { get; set; }

        [StringLength(10)]
        public string State { get; set; }


        [StringLength(50)]
        public string City { get; set; }


        [Required(ErrorMessage = "Inform Phone")]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Inform Email")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
            ErrorMessage = "The email does not have a correct format")]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total order")]
        public decimal OrderTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Items on Order")]
        public int OrderTotalItens { get; set; }

        [Display(Name = "Date Request")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateRequest { get; set; }

        [Display(Name = "Order Shipping Date")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? OrderShippingDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}
