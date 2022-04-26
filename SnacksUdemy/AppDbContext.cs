﻿using Microsoft.EntityFrameworkCore;
using SnacksUdemy.Models;

namespace SnacksUdemy
{
    public class AppDbContext : DbContext
    {
        //contructor EF
        public AppDbContext(DbContextOptions<AppDbContext> options)
        {
        }

        //Classes mapeed
        public DbSet<Category> Categorìes { get; set; }
        public DbSet<Snack> Snacks { get; set; }
    }
}
