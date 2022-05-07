using SnacksUdemy.Models;

namespace SnacksUdemy.Repositories.Interfaces
{
    public interface ISnackRepository
    {
        IEnumerable<Snack> Snacks { get; }

        IEnumerable<Snack> SnackFavorites { get; }

       Snack FindById(int snackId);
    }
}
