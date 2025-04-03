using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN222.MilkTeaShop.Repository.Models;

namespace PRN222.MilkTeaShop.Service.Services.Interface
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task CreatePaymentAsync(Payment payment);
        Task UpdatePaymentStatusAsync(int paymentId, string newStatus);
        Task DeletePaymentAsync(int paymentId);
        Task<Payment> GetPaymentByOrderIdAsync(int orderId);
    }
}
