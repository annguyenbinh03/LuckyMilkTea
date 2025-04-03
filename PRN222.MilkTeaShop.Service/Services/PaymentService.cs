using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.Repositories;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _paymentRepository.GetPaymentByIdAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _paymentRepository.GetAllPaymentsAsync();
        }

        public async Task CreatePaymentAsync(Payment payment)
        {
            await _paymentRepository.AddPaymentAsync(payment);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            await _paymentRepository.UpdatePaymentAsync(payment);
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            await _paymentRepository.DeletePaymentAsync(paymentId);
        }
    }
}
