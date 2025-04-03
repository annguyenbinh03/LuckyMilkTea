using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Service.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task<(IEnumerable<Product>, int)> GetMilkTeas(string? search, int? page = null, int? pageSize = null);
        public Task CreateMilkTea(MilkTeaModel model);
        public Task UpdateMilkTea(MilkTeaModel model);
		Task<MilkTeaModel?> GetMilkTea(int id);
		Task Delete(int id);
		Task Active(int id);
	}
}

