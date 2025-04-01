using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<List<Product>> GetMilkTeas();
        Task<List<Product>> GetProductsByCategoryIds(List<int> categoryIds);

    }
}

