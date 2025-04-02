using Azure;
using PRN222.MilkTeaShop.Repository.Enums;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.UnitOfWork;
using PRN222.MilkTeaShop.Service.BusinessObjects;
using PRN222.MilkTeaShop.Service.Utils;
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

        public async Task CreateMilkTea(MilkTeaModel model)
        {
            Product? product = model.ToProduct();
            product.CategoryId = 1;
			product.Status = ProductStatus.active.ToString();
			product.CreatedAt = TimeZoneUtil.GetCurrentTime();
            product.UpdatedAt = TimeZoneUtil.GetCurrentTime();

            if (product == null)
            {
                throw new Exception("Can not parse product");
            }
            try
            {
                await _unitOfWork.Product.AddAsync(product);

                var productSizeS = new ProductSize
                {
                    Product = product,
                    SizeId = 1,
                    Price = (decimal)model.PriceSizeS
                };
                await _unitOfWork.ProductSize.AddAsync(productSizeS);

				var productSizeM = new ProductSize
				{
					Product = product,
					SizeId = 2,
					Price = (decimal)model.PriceSizeM
				};
				await _unitOfWork.ProductSize.AddAsync(productSizeM);

				var productSizeL = new ProductSize
				{
					Product = product,
					SizeId = 3,
					Price = (decimal)model.PriceSizeL
				};
				await _unitOfWork.ProductSize.AddAsync(productSizeL);

				await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<Product>> GetAll()
        {
            var (products, totalItems) = await _unitOfWork.Product.GetAsync();

            return products.ToList(); 
        }

        public async Task<(IEnumerable<Product>, int)> GetMilkTeas(string? search, int? page = null, int? pageSize = null)
        {
            var (products, totalItems) = await _unitOfWork.Product.GetMilkTeas(search, page, pageSize);
            return (products.ToList(), totalItems);
        }

         
    }
}
