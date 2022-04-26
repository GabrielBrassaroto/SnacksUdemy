using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnacksUdemy.Models
{
    [Table("Snacks")]
    public class Snack
    {
        [Key]
        public int SnackId { get; set; }

        [Required(ErrorMessage = "The name of the snack must be informed ")]
        [Display(Name = "Name Snack")]
        public string Name { get; set; }

        [Display(Name = "Descripton Snack")]
        [MinLength(20, ErrorMessage = "Description must be at least  {1} characters long.")]
        [MaxLength(200, ErrorMessage = "The description cannot exceed the number of {1}characters ")]
        public string ShortDescripton { get; set; }

        [NotMapped]
        public DateTime DateCreate { get; set; }

        public string DetailedDescripton { get; set;}

        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public string IsFavoriteSnack { get; set; }

        public string Stock { get; set; }




        //foreign key navegation
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }
}
