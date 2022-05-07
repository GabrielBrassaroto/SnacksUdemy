using SnacksUdemy.Models;

namespace SnacksUdemy.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
