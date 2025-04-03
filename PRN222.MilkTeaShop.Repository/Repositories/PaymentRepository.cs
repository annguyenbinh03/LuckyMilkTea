using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Models;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DbContext _context;

        public PaymentRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Set<Payment>().FindAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Set<Payment>().ToListAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Set<Payment>().AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Set<Payment>().Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            var payment = await GetPaymentByIdAsync(paymentId);
            if (payment != null)
            {
                _context.Set<Payment>().Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }

}
