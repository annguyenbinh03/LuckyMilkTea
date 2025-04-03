using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Models;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContext _context;

        public OrderRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Set<Order>().FindAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Set<Order>().ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Set<Order>().AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Set<Order>().Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order != null)
            {
                _context.Set<Order>().Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
