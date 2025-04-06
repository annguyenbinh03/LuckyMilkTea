using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Services.Interface
{
    public interface IDashboardService
    {
        Task<object> GetSalesData();
        Task<decimal> GetTotalRevenueByDay();
        Task<decimal> GetTotalRevenueByMonth();

        Task<IEnumerable<Product>> GetHighLightProduct();

    }
}
