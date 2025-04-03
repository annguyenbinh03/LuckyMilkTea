using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.Models
{
    public class PaymentModel
    {
        public string vnp_TmnCode { get; set; }
        public string vnp_Amount { get; set; }
        public string vnp_Command { get; set; } = "pay";
        public string vnp_CreateDate { get; set; }
        public string vnp_CurrCode { get; set; } = "VND";
        public string vnp_IpAddr { get; set; }
        public string vnp_Locale { get; set; } = "vn";
        public string vnp_OrderInfo { get; set; }
        public string vnp_OrderType { get; set; } = "other";
        public string vnp_ReturnUrl { get; set; }
        public string vnp_TxnRef { get; set; }
        public string vnp_Version { get; set; } = "2.1.0";
        public string vnp_SecureHash { get; set; }
    }
}
