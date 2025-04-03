using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace PRN222.MilkTeaShop.Repository.Models
{
    public static class VNPayHelper
    {
        public static string CreatePaymentUrl(PaymentModel vnpRequest, string hashSecret)
        {
            // Tạo chuỗi query string từ các tham số của vnpRequest
            var queryString = $"?vnp_TmnCode={vnpRequest.vnp_TmnCode}&vnp_Amount={vnpRequest.vnp_Amount}&vnp_CreateDate={vnpRequest.vnp_CreateDate}...";
            // Tính toán và thêm SecureHash vào URL
            string secureHash = GenerateSecureHash(queryString, hashSecret);
            queryString += $"&vnp_SecureHash={secureHash}";
            return "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html" + queryString;
        }

        public static bool ValidateSecureHash(string amount, string command, string createDate, string ipAddr, string locale, string orderInfo, string orderType, string tmnCode, string txnRef, string secureHash, string hashSecret)
        {
            // Tạo chuỗi tham số giống như khi tạo URL thanh toán
            string data = $"{amount}{command}{createDate}{ipAddr}{locale}{orderInfo}{orderType}{tmnCode}{txnRef}";
            string expectedHash = GenerateSecureHash(data, hashSecret);

            return expectedHash == secureHash;
        }

        public static string GenerateSecureHash(string data, string hashSecret)
        {
            using (var sha256 = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data + hashSecret);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
