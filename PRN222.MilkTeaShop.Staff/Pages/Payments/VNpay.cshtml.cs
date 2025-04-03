using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PRN222.MilkTeaShop.Repository.Models;
using System;

namespace PRN222.MilkTeaShop.Staff.Pages.Payments
{
    public class VNpayModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public string OrderId { get; set; }
        public string Amount { get; set; }
        public string PaymentUrl { get; set; }

        // Constructor để inject IConfiguration
        public VNpayModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // OnGet để xử lý yêu cầu và tạo URL thanh toán VNPay
        public IActionResult OnGet(string amount, string orderId)
        {
            // Kiểm tra tham số amount và orderId có hợp lệ không
            if (string.IsNullOrEmpty(amount) || string.IsNullOrEmpty(orderId))
            {
                return BadRequest("Invalid amount or orderId");
            }

            string tmnCode = _configuration["VNPay:TmnCode"];
            string hashSecret = _configuration["VNPay:HashSecret"];
            string returnUrl = _configuration["VNPay:ReturnUrl"];
            string url = _configuration["VNPay:Url"];

            Amount = amount;
            OrderId = orderId;

            try
            {
                // Tạo request thanh toán VNPay
                PaymentModel vnpRequest = new PaymentModel
                {
                    vnp_TmnCode = tmnCode,
                    vnp_Amount = (int.Parse(amount) * 100).ToString(), // Số tiền * 100 (VNĐ)
                    vnp_CreateDate = DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
                    vnp_IpAddr = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    vnp_OrderInfo = $"Thanh toán đơn hàng #{orderId}",
                    vnp_ReturnUrl = returnUrl, // URL quay lại sau khi thanh toán
                    vnp_TxnRef = DateTime.UtcNow.Ticks.ToString() // Mã giao dịch duy nhất
                };

                // Gọi helper để tạo URL thanh toán
                PaymentUrl = VNPayHelper.CreatePaymentUrl(vnpRequest, hashSecret);

                // Chuyển hướng đến VNPay
                return Redirect(PaymentUrl);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating payment URL: {ex.Message}");
            }
        }

    }
}
