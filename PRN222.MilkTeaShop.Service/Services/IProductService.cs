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
        Task<List<Product>> GetAll();
        Task<(IEnumerable<Product>, int)> GetMilkTeas(string? search, int? page = null, int? pageSize = null);
        Task<MilkTeaModel?> GetMilkTea(int id);
        Task CreateMilkTea(MilkTeaModel model);
        Task UpdateMilkTea(MilkTeaModel model);
        Task<(IEnumerable<Product>, int)> GetToppings(string? search, int? page = null, int? pageSize = null);
		Task<Product?> GetTopping(int id);
        Task CreateTopping(ToppingModel model);
        Task UpdateTopping(ToppingModel model);
        Task<(IEnumerable<ComboModel>, int)> GetCombos(string? search, int? page = null, int? pageSize = null);
        Task<Product?> GetCombo(int id);
		Task CreateCombo(ComboModel model);
		Task Delete(int id);
		Task Active(int id);

        Task<List<Product>> GetStartMilkTeas();

        Task<List<Product>> GetCombosAsync();

        Task<List<Product>> GetToppingAsync();
    }
}

