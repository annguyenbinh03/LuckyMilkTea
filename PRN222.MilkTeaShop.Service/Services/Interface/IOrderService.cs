using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN222.MilkTeaShop.Repository.Models;

namespace PRN222.MilkTeaShop.Service.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task CreateOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
        Task<(IEnumerable<Order>, int)> GetOrders(string? search, int? page = null, int? pageSize = null);       
    }
}
