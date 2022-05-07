using SnacksUdemy.Models;

namespace SnacksUdemy.Repository
{
    public class CategoryRepository : ICategoryInterface
    {
       private readonly AppDbContext _context;   

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Categories => _context.Categorìes;
    }
}
