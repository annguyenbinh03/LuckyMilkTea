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
        [FromQuery]
        public int OrderId { get; set; }

        [FromQuery]
        public double Amount { get; set; } 
        public void OnGet(int orderId, double amount)
        {
            if (orderId == 0)
            {
                ModelState.AddModelError(string.Empty, "OrderId is invalid.");
                return;
            }
            Console.WriteLine(amount);
            Amount = amount;
            OrderId = orderId;
            Payment = new Payment { 
                OrderId = orderId,
                //PaymentMethodId = 1,
                Amount = (int)amount,

            }; // Đảm bảo Payment được khởi tạo
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Payment == null)
            {
                ModelState.AddModelError(string.Empty, "Payment object is null.");
                return Page();
            }

            if (Payment.PaymentMethodId == 1) // VNPay
            {
                // Điều hướng tới trang xử lý VNPay
                return RedirectToPage("/Payments/VNPay", new { amount = Payment.Amount, orderId = Payment.OrderId });
            }
            else if (Payment.PaymentMethodId == 2) // Cash
            {
                // Nếu là Cash, tạo payment và lưu vào cơ sở dữ liệu
                await _paymentService.CreatePaymentAsync(Payment);

                // Sau khi lưu thanh toán, quay lại trang danh sách đơn hàng
                return RedirectToPage("/Payments/Index");
            }

            // Nếu không chọn phương thức hợp lệ
            ModelState.AddModelError(string.Empty, "Invalid payment method.");
            return Page();
        }



    }
}
