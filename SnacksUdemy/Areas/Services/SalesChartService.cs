using SnacksUdemy.Models;

namespace SnacksUdemy.Areas.Services
{
    public class SalesChartService
    {

        private readonly AppDbContext context;

        public SalesChartService(AppDbContext context)
        {
            this.context = context;
        }

        public List<SnackGraphic> GetSalesSnacks(int days = 360)
        {
            var date = DateTime.Now.AddDays(-days);

            var snacks = (from pd in context.OrderDetails
                          join l in context.Snacks on pd.SnackId equals l.SnackId
                        //  where pd.Request.DateRequest >= date
                          group pd by new { pd.SnackId, l.Name, pd.Lenght }
                          into g
                          select new
                          {
                              SnackName = g.Key.Name,
                              SnackLenght = g.Sum(q => q.Lenght),
                              SnackPriceTotal = g.Sum(a => a.Price * a.Lenght)
                          });

            var list = new List<SnackGraphic>();

            foreach (var item in snacks)
            {
                var snack = new SnackGraphic();
                snack.SnackName = item.SnackName;
                snack.SnackLenght = item.SnackLenght;
                snack.SnackPriceTotal = item.SnackPriceTotal;
                list.Add(snack);
            }

            return list;
        }

    }
}
