using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly MilkTeaDBContext _context;

        public OrderService(MilkTeaDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await Task.FromResult(_context.Orders.ToList());
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await Task.FromResult(_context.Orders.FirstOrDefault(o => o.Id == orderId));
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null) return false;

            order.UpdateStatus(status);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
