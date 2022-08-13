using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnacksUdemy.Areas.Services;

namespace SnacksUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminChartSalesController : Controller
    {

        private readonly SalesChartService _salesChartService;

        public AdminChartSalesController(SalesChartService salesChartService)
        {
            _salesChartService = salesChartService ?? throw new ArgumentNullException(nameof(salesChartService));
        }

        public JsonResult SalesSnack(int days)
        {
            var snacsSalesTotal = _salesChartService.GetSalesSnacks(days);
            return Json(snacsSalesTotal);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult SalesMonthly(int days)
        {
            return View();
        }


        [HttpGet]
        public IActionResult SalesWeekly(int days)
        {
            return View();
        }

    }
}
