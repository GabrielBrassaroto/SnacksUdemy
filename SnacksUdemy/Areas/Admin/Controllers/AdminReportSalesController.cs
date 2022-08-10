using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Areas.Services;

namespace SnacksUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminReportSalesController : Controller
    {
        private readonly ReportSalesService reportSalesService;

        public AdminReportSalesController(ReportSalesService _reportSalesService)
        {
           reportSalesService = _reportSalesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ReportSalesSimple( DateTime? minDate,
            DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = minDate.Value.ToString("yyyy-MM-dd");

            var result = await reportSalesService.FindByDateAsync(minDate,maxDate);

            return View(result);
        }
    }
}
