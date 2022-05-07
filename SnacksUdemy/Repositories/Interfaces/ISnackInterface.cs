using SnacksUdemy.Models;

namespace SnacksUdemy.Repositories.Interfaces
{
    public interface ISnackInterface
    {
        IEnumerable<Snack> Snacks { get; }

        IEnumerable<Snack> SnackFavorites { get; }

       Snack FindById(int snackId);
    }
}
