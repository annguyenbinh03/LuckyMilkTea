using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.UnitOfWork;
using PRN222.MilkTeaShop.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PRN222.MilkTeaShop.Service.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetHighLightProduct()
        {
            var (products, totalItem) = await _unitOfWork.Product.GetAsync(orderBy: p => p.OrderBy(product => product.SoldCount), descending: true);
            return products;
        }

        public async Task<object> GetSalesData()
        {
            DateTime today = DateTime.Today;
            List<DateTime> labels = Enumerable.Range(0, 7)
                .Select(i => today.AddDays(-i))
                .ToList();
            Dictionary<DateTime, decimal> salesMap = labels.ToDictionary(date => date, _ => 0m);

            List<Order> orders = await GetOrdersInLast7Days();

            foreach (var order in orders)
            {
                DateTime orderDate = ((DateTime)order.CreatedAt).Date;
                if (salesMap.ContainsKey(orderDate))
                {
                    salesMap[orderDate] += order.TotalPrice;
                }
            }

            return new
            {
                labels = labels.Select(d => d.ToString("yyyy-MM-dd")).Reverse(),
                data = labels.Select(d => salesMap[d]).Reverse()
            };
        }

        public async Task<decimal> GetTotalRevenueByDay()
        {
            DateTime date = DateTime.Today;
            var (orders, totalItems ) = await _unitOfWork.Order.GetAsync(o => ((DateTime)o.CreatedAt).Date == date);
            decimal totalRevenue = orders.ToList().Sum(o => o.TotalPrice);
            return totalRevenue;
        }

        public async Task<decimal> GetTotalRevenueByMonth()
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = startDate.AddMonths(1);

            var (orders, totalItems) = await _unitOfWork.Order.GetAsync(o => o.CreatedAt >= startDate && o.CreatedAt < endDate);
            decimal totalRevenue = orders.AsEnumerable().Sum(o => o.TotalPrice);
            return totalRevenue;
        }

        private async Task<List<Order>> GetOrdersInLast7Days()
        {
            DateTime today = DateTime.Today.AddDays(1);
            DateTime startDate = today.AddDays(-6); // Lấy từ 7 ngày trước đến hôm nay

            var (orders, totalItems ) = await _unitOfWork.Order
                .GetAsync(o => o.CreatedAt >= startDate && o.CreatedAt <= today);
            return orders.ToList();
        }
    }
}
