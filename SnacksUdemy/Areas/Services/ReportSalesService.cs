using Microsoft.EntityFrameworkCore;
using SnacksUdemy.Models;

namespace SnacksUdemy.Areas.Services
{
    public class ReportSalesService
    {

        private readonly AppDbContext context;

        public ReportSalesService(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<List<Request>> FindByDateAsync(DateTime? mindate, DateTime? maxdate)
        {
            var result = from obj in context.Requests select obj;

            if (mindate.HasValue)
            {
                result = result.Where(x => x.OrderShippingDate >= mindate.Value);
            }

            if (maxdate.HasValue)
            {
                result = result.Where(x => x.OrderShippingDate >= mindate.Value);
            }

            return await result.Include (l => l.RequestItens).
                ThenInclude( l => l.Snack)
                .OrderByDescending(x => x.OrderShippingDate)
                .ToListAsync();
        }
    }
}
