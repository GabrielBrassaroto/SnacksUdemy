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
        [StringLength(50,MinimumLength =10,ErrorMessage = "The minimum {0} value and maximum {1} value")]  
        public string Name { get; set; }

        [Required(ErrorMessage = "The description must be informed")]
        [Display(Name = "Descripton Snack")]
        [MinLength(20, ErrorMessage = "Description must be at least  {1} characters long.")]
        [MaxLength(200, ErrorMessage = "The description cannot exceed the number of {1}characters ")]
        public string ShortDescripton { get; set; }

        [NotMapped]
        public DateTime DateCreate { get; set; }

        [Required(ErrorMessage = "The description must be informed")]
        [Display(Name = "Descripton Snack")]
        [MinLength(20, ErrorMessage = "Description must be at least  {1} characters long.")]
        [MaxLength(200, ErrorMessage = "The description cannot exceed the number of {1}characters ")]

        public string DetailedDescripton { get; set;}

        [Required(ErrorMessage = "The Price must be informed")]
        [Display(Name = "Price")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1,999.99,ErrorMessage = "The price must be between 1 and 999")]
        public decimal Price { get; set; }

        [Display(Name= "Malk normal image")]
        [StringLength(200,ErrorMessage = "The {0} must have a maximum of {1} characters")]
        public string ImageUrl { get; set; }

        [Display(Name = "Malk Thumbnail image")]
        [StringLength(200, ErrorMessage = "The {0} must have a maximum of {1} characters")]
        public string ImageThumbnailUrl { get; set; }

        [Display(Name = "Favorite?")]
        public bool IsFavoriteSnack { get; set; }

        [Display(Name = "Stock")]
        public bool Stock { get; set; }


        //foreign key navegation
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }
}
