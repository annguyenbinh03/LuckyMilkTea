using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.Repositories;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Staff.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService  _orderService;

        public CreateModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public Order Order { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _orderService.CreateOrderAsync(Order);
            return RedirectToPage("Index");
        }
    }
}
