using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<(IEnumerable<Product>, int)> GetMilkTeas(string? search, int? page = null, int? pageSize = null);
        Task<Product?> GetMilkTea(int id);
        Task<(IEnumerable<Product>, int)> GetToppings(string? search, int? page = null, int? pageSize = null);
		Task<Product?> GetTopping(int id);

	}
}
