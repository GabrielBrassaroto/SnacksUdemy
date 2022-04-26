namespace SnacksUdemy.Models
{
    public class Snack
    {
        public int SnackId { get; set; }

        public string Name { get; set; }

        public string ShortDescripton { get; set; }

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
