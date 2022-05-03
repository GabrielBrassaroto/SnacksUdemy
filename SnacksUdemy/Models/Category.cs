using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnacksUdemy.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [StringLength(100,ErrorMessage = "The maximum length is 100 characters")]
        [Required(ErrorMessage = "Inform category name")]
        [Display(Name ="Name")]
        public string  CategoryName { get; set; }

        [StringLength(200, ErrorMessage = "The maximum length is 200 characters")]
        [Required(ErrorMessage = "Inform descripton name")]
        [Display(Name = "Name")]
        public string Descripton { get; set; }

        //Foreign key navegation
        public List<Snack> Snacks { get; set; }
    }
}
