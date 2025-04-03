using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Services
{
    public class OrderDetailService
        : IOrderDetailService
    {
        private readonly MilkTeaDBContext _context;

        public OrderDetailService(MilkTeaDBContext context)
        {
            _context = context;
        }
        public async Task CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
        }
        public async Task AddToppingToOrderDetailAsync(int orderDetailId, int toppingId)
        {
            var topping = await _context.Products.FirstOrDefaultAsync(p => p.Id == toppingId && p.CategoryId == 3);
            if (topping == null)
            {
                throw new Exception("Topping not found");
            }

            var parentOrderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (parentOrderDetail == null)
            {
                throw new Exception("Parent order detail not found");
            }

            var orderDetail = new OrderDetail
            {
                OrderId = parentOrderDetail.OrderId,
                ProductId = toppingId,
                Quantity = 1,
                Price = topping.Price ?? 0,
                ParentId = orderDetailId
            };

            await CreateOrderDetailAsync(orderDetail);
        }
        public async Task AddProductToOrderAsync(int orderId, int productId, int sizeId, List<int> toppingIds)
        {
            // Lấy giá từ ProductSize
            var productSize = await _context.ProductSizes
                .FirstOrDefaultAsync(ps => ps.ProductId == productId && ps.SizeId == sizeId);
            if (productSize == null)
            {
                throw new Exception("Invalid product size");
            }

            // Thêm OrderDetail cho sản phẩm chính
            var orderDetail = new OrderDetail
            {
                OrderId = orderId,
                ProductId = productId,
                SizeId = sizeId,
                Quantity = 1,
                Price = productSize.Price
            };
            await CreateOrderDetailAsync(orderDetail);

            // Thêm topping nếu có
            foreach (var toppingId in toppingIds)
            {
                await AddToppingToOrderDetailAsync(orderDetail.Id, toppingId);
            }
        }
        public async Task<List<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails
                .Include(od => od.Product)
                .Include(od => od.Size)
                .Include(od => od.Order)
                .ToListAsync();
        }
    }
}
