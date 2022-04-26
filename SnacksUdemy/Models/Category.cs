using System.ComponentModel.DataAnnotations;

namespace SnacksUdemy.Models
{
    public class Category
    {
        public int CategoryId { get; set; }


        public string  Name { get; set; }

        public string Descripton { get; set; }

        //Foreign key navegation
        public List<Snack> Snacks { get; set; }
    }
}
