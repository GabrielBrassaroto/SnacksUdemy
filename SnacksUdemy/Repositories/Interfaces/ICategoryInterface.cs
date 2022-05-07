using SnacksUdemy.Models;

namespace SnacksUdemy.Repository
{
    public interface ICategoryInterface
    {
        IEnumerable<Category> Categories { get; }
    }
}
