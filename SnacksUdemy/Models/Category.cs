namespace SnacksUdemy.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string  Name { get; set; }

        public string Descripton { get; set; }

        public List<Snack> Snacks { get; set; }
    }
}
