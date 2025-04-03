using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.Repositories;
using PRN222.MilkTeaShop.Repository.UnitOfWork;
using PRN222.MilkTeaShop.Service.Services.Interface;

namespace PRN222.MilkTeaShop.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var (orders, totalItems) = await _unitOfWork.Order.GetAsync(includes: o => o.OrderDetails, orderBy: o => o.OrderBy(order => order.CreatedAt), descending: true);

            foreach (var order in orders)
            {
                List<OrderDetail> details = new List<OrderDetail>();
                foreach (var orderDetail in order.OrderDetails)
                {
                    var detail = await _unitOfWork.OrderDetail.GetByIdAsync(orderDetail.Id, od => od.Product,   od => od.Size);
                    if (detail != null)
                        details.Add(detail);
                }
                order.OrderDetails = details;
            }
            return orders;
        }
    }
}
