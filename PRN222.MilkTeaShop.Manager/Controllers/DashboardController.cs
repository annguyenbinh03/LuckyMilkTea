using Microsoft.AspNetCore.Mvc;
using PRN222.MilkTeaShop.Service.Services;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Manager.Controllers
{
    [Route("api/dashboard")]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<IActionResult> Index()
        {
            var dailyRevenue =  await _dashboardService.GetTotalRevenueByDay();
            var monthlyRevenue = await _dashboardService.GetTotalRevenueByMonth();
            var products = await _dashboardService.GetHighLightProduct();

            ViewBag.DailyRevenue = dailyRevenue;
            ViewBag.MonthyRevenue = monthlyRevenue;
            ViewBag.AnnualRevenue = monthlyRevenue;
            ViewBag.Products = products;
            return View();
        }

        [HttpGet("sales-data")]
        public async Task<IActionResult> GetSalesData()
        {
            var salesData = await _dashboardService.GetSalesData();
            return Ok(salesData);
        }
    }
}
