using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Service.Services;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Staff.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public IndexModel(IOrderService orderService, IPaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }

        public IEnumerable<Order> Orders { get; set; }
        public string ErrorMessage { get; set; } // Thêm trường để chứa thông báo lỗi
        public async Task OnGetAsync()
        {
            Orders = await _orderService.GetAllOrdersAsync();
            Console.WriteLine(Orders);
        }
        public async Task<IActionResult> OnPostUpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            var payment = await _paymentService.GetPaymentByOrderIdAsync(orderId);

            if (newStatus == "Completed" && (payment == null || payment.Status != "Completed")) // Kiểm tra thanh toán đã hoàn thành chưa
            {
                ErrorMessage = "Bạn không thể thay đổi trạng thái đơn hàng khi chưa thanh toán thành công.";
                Orders = await _orderService.GetAllOrdersAsync();
                return Page();
            }

            // Cập nhật trạng thái đơn hàng
            if (newStatus == "Cancelled" && (payment == null || payment.Status == "Completed"))
            {
                ErrorMessage = "Trạng thái 'Cancelled' chỉ có thể chọn khi thanh toán thất bại hoặc đang chờ.";

                Orders = await _orderService.GetAllOrdersAsync();

                return Page();
            }


            order.Status = newStatus;
            await _orderService.UpdateOrderAsync(order);
            return RedirectToPage();
        }
    }
}
