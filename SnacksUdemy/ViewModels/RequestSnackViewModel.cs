using SnacksUdemy.Models;

namespace SnacksUdemy.ViewModels
{
    public class RequestSnackViewModel
    {
        public Request Request { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
