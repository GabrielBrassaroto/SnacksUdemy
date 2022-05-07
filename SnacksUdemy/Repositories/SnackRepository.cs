using Microsoft.EntityFrameworkCore;
using SnacksUdemy.Models;
using SnacksUdemy.Repositories.Interfaces;

namespace SnacksUdemy.Repositories
{
    public class SnackRepository : ISnackInterface
    {

        private readonly AppDbContext _context;

        public SnackRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Snack> Snacks => _context.Snacks.Include( c => c.Category);

        public IEnumerable<Snack> SnackFavorites => _context.Snacks.Where
            ( p => p.IsFavoriteSnack).Include( c => c.Category);   

        public Snack FindById(int snackId) => _context.Snacks.FirstOrDefault( p => p.SnackId == snackId);   

    }
}
