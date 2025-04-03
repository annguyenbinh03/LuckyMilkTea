using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN222.MilkTeaShop.Repository.Models;

namespace PRN222.MilkTeaShop.Staff.Pages.Payments
{
    public class VNPayReturnModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string Amount { get; set; }
        public string OrderInfo { get; set; }

        public VNPayReturnModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(
            string vnp_Amount,
            string vnp_Command,
            string vnp_CreateDate,
            string vnp_IpAddr,
            string vnp_Locale,
            string vnp_OrderInfo,
            string vnp_OrderType,
            string vnp_TmnCode,
            string vnp_TxnRef,
            string vnp_SecureHash)
        {
            string hashSecret = _configuration["VNPay:HashSecret"];

            // Kiểm tra SecureHash để xác thực tính hợp lệ của dữ liệu
            bool isValid = VNPayHelper.ValidateSecureHash(vnp_Amount, vnp_Command, vnp_CreateDate,
                                                          vnp_IpAddr, vnp_Locale, vnp_OrderInfo,
                                                          vnp_OrderType, vnp_TmnCode, vnp_TxnRef,
                                                          vnp_SecureHash, hashSecret);

            if (isValid)
            {
                // Lưu thông tin vào cơ sở dữ liệu hoặc session, v.v.
                Amount = vnp_Amount;
                OrderInfo = vnp_OrderInfo;

                // Đánh dấu giao dịch là hợp lệ
                IsValid = true;
                Message = "Thanh toán thành công!";
            }
            else
            {
                // Nếu không hợp lệ, thông báo lỗi
                IsValid = false;
                Message = "Thanh toán không thành công, vui lòng kiểm tra lại.";
            }

            return Page();
        }
    }
}