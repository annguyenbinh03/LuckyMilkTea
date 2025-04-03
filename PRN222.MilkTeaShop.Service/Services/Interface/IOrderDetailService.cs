using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Services.Interface
{
    public interface IOrderDetailService
    {
        Task CreateOrderDetailAsync(OrderDetail orderDetail);

        Task AddToppingToOrderDetailAsync(int orderDetailId, int toppingId);
        Task AddProductToOrderAsync(int orderId, int productId, int sizeId, List<int> toppingIds);
        Task<List<OrderDetail>> GetAllOrderDetailsAsync();
    }
}
