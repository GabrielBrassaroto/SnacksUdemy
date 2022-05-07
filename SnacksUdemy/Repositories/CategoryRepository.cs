﻿using SnacksUdemy.Models;

namespace SnacksUdemy.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
       private readonly AppDbContext _context;   

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Categories => _context.Categorìes;
    }
}
