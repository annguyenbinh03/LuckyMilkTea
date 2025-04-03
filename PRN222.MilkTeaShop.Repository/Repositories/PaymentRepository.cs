using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Models;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MilkTeaDBContext _context;

        public PaymentRepository(MilkTeaDBContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Set<Payment>().FindAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
        {
            return await _context.Set<Payment>().Include(P => P.PaymentMethod).ToListAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Set<Payment>().AddAsync(payment);
            await _context.SaveChangesAsync();
        }
        public async Task<Payment?> GetPaymentByOrderIdAsync(int orderId)
        {
            return await _context.Set<Payment>()
                .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment); // Mark the payment entity as modified
            await _context.SaveChangesAsync(); // Save changes to the database
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
