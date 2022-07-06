using Microsoft.EntityFrameworkCore;
using SnacksUdemy.Models;

namespace SnacksUdemy
{
    public class AppDbContext : DbContext
    {
        //contructor EF
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Classes mapeed
        public DbSet<Category> Categorìes { get; set; }
        public DbSet<Snack> Snacks { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItens { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
