using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAll()
        {
            var (products, totalItems) = await _unitOfWork.Product.GetAsync();

            return products.ToList(); 
        }
    }
}
