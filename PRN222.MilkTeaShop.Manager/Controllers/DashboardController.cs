using Microsoft.AspNetCore.Mvc;

namespace PRN222.MilkTeaShop.Manager.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
