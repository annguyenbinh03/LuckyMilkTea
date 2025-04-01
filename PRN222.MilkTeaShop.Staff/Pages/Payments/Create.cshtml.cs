using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Service.Services;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Staff.Pages.Payments
{
    public class CreateModel : PageModel
    {
        private readonly IPaymentService _paymentService;

        private readonly IOrderService _orderService;

        public CreateModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [BindProperty]
        public Payment Payment { get; set; }

        public int OrderId { get; set; }

        public void OnGet(int orderId)
        {
            if (orderId == 0)
            {
                ModelState.AddModelError(string.Empty, "OrderId is invalid.");
                return;
            }

            OrderId = orderId;
            Payment = new Payment { OrderId = orderId }; // Đảm bảo Payment được khởi tạo
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Payment == null)
            {
                ModelState.AddModelError(string.Empty, "Payment object is null.");
                return Page();
            }

            // Kiểm tra xem OrderId có hợp lệ không
            var order = await _orderService.GetOrderByIdAsync(Payment.OrderId);
            if (order == null)
            {
                ModelState.AddModelError(string.Empty, "Order not found.");
                return Page();
            }

            await _paymentService.CreatePaymentAsync(Payment);
            return RedirectToPage("/Orders/Index");
        }


    }
}
