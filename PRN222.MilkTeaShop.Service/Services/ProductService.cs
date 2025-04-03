﻿using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Enums;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.UnitOfWork;
using PRN222.MilkTeaShop.Service.BusinessObjects;
using PRN222.MilkTeaShop.Service.Utils;
using static Azure.Core.HttpHeader;

namespace PRN222.MilkTeaShop.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public async Task<MilkTeaModel?> GetMilkTea(int id)
        {
           var product = await _unitOfWork.Product.GetMilkTea(id);
            if (product == null)
                return null;
            MilkTeaModel model = new MilkTeaModel
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Status = product.Status,
                PriceSizeS = product.ProductSizes.ToList()[0].Price,
                PriceSizeM = product.ProductSizes.ToList()[1].Price,
                PriceSizeL = product.ProductSizes.ToList()[2].Price,
            };

            return model;
		}

		public async Task UpdateMilkTea(MilkTeaModel model)
		{
			Product? product = await _unitOfWork.Product.GetByIdAsync(model.Id);

            if(product == null) return;

			product.UpdatedAt = TimeZoneUtil.GetCurrentTime();

            if (product.Name != model.Name)
            {
                product.Name = model.Name;
            }

			if (product.Description != model.Description)
			{
				product.Description = model.Description;
			}

			if (product.ImageUrl != model.ImageUrl && !string.IsNullOrEmpty(model.ImageUrl))
			{
				product.ImageUrl = model.ImageUrl;
			}

			if (product == null)
			{
				throw new Exception("Can not parse product");
			}
			try
			{
				_unitOfWork.Product.Update(product);

                var productSizeS = await _unitOfWork.ProductSize.FirstOrDefaultAsync(filter: ps => ps.ProductId == model.Id &&  ps.SizeId == (int)ProductSizeEnum.S);
                if(productSizeS != null)
                {
					productSizeS.Price = (decimal)model.PriceSizeS;
					_unitOfWork.ProductSize.Update(productSizeS);
				}
              
				var productSizeM = await _unitOfWork.ProductSize.FirstOrDefaultAsync(filter: ps => ps.ProductId == model.Id && ps.SizeId == (int)ProductSizeEnum.M);
                if(productSizeM != null)
                {
					productSizeM.Price = (decimal)model.PriceSizeM;
					_unitOfWork.ProductSize.Update(productSizeM);
				}
				

				var productSizeL = await _unitOfWork.ProductSize.FirstOrDefaultAsync(filter: ps => ps.ProductId == model.Id && ps.SizeId == (int)ProductSizeEnum.L);
				if (productSizeL != null)
				{
					productSizeL.Price = (decimal)model.PriceSizeL;
					_unitOfWork.ProductSize.Update(productSizeL);
				}
				await _unitOfWork.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

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

        public async Task Delete(int id)
        {
            Product? product = await _unitOfWork.Product.GetByIdAsync(id);

            if (product == null) return;
            product.Status = ProductStatus.deleted.ToString();
			product.UpdatedAt = TimeZoneUtil.GetCurrentTime();

			_unitOfWork.Product.Update(product);

			await _unitOfWork.SaveChanges();
        }

		public async Task Active(int id)
		{
			Product? product = await _unitOfWork.Product.GetByIdAsync(id);

			if (product == null) return;
			product.Status = ProductStatus.active.ToString();
			product.UpdatedAt = TimeZoneUtil.GetCurrentTime();

			_unitOfWork.Product.Update(product);

			await _unitOfWork.SaveChanges();
		}
        public async Task<(IEnumerable<Product>, int)> GetToppings(string? search, int? page = null, int? pageSize = null)
        {
            var (products, totalItems) = await _unitOfWork.Product.GetToppings(search, page, pageSize);
            return (products.ToList(), totalItems);
        }

        public async Task<Product?> GetTopping(int id)
        {
            var product = await _unitOfWork.Product.GetTopping(id);
            if (product == null)
                return null;

            return product;
        }

		public async Task CreateTopping(ToppingModel model)
		{
			Product? product = model.ToProduct();
			product.CategoryId = 3;
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
				await _unitOfWork.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Product>> GetStartMilkTeas()
        {
            var products = await _unitOfWork.Product.GetStartMilkTeas();

            return products;
        }
        public async Task<List<Product>> GetCombosAsync()
        {
            return await _unitOfWork.Product.GetCombosAsync();
        }

        public async Task<List<Product>> GetToppingAsync()
        {
            return await _unitOfWork.Product.GetToppingAsync();
        }

        public async Task UpdateTopping(ToppingModel model)
        {
            Product? product = await _unitOfWork.Product.GetByIdAsync(model.Id);

            if (product == null) return;

            product.UpdatedAt = TimeZoneUtil.GetCurrentTime();

            if (product.Name != model.Name)
            {
                product.Name = model.Name;
            }

            if (product.Description != model.Description)
            {
                product.Description = model.Description;
            }

            if (product.ImageUrl != model.ImageUrl && !string.IsNullOrEmpty(model.ImageUrl))
            {
                product.ImageUrl = model.ImageUrl;
            }

            if (product.Price != model.Price)
            {
                product.Price = model.Price;
            }

            if (product == null)
            {
                throw new Exception("Can not parse product");
            }
            try
            {
                _unitOfWork.Product.Update(product);

                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<(IEnumerable<ComboModel>, int)> GetCombos(string? search, int? page = null, int? pageSize = null)
        {
            var (products, totalItems) = await _unitOfWork.Product.GetCombos(search, page, pageSize);
            List<ComboModel> comboModels = new List<ComboModel>();
            foreach (var combo in products)
            {
                var comboModel = new ComboModel
                {
                    Id = combo.Id,
                    Name = combo.Name,
                    Description = combo.Description,
                    Price = combo.Price,
                    ImageUrl = combo.ImageUrl,
                    SoldCount = combo.SoldCount,
                    Status = combo.Status,
                    UpdatedAt = combo.UpdatedAt,
                    Products = new List<ProductInCombo>()
                };

                // Populate List<ProductCombo>
                foreach (var productCombo in combo.ProductComboCombos)
                {
                    var product = await _unitOfWork.Product.GetByIdAsync(productCombo.ProductId);
                    if (product != null)
                    {
                        var productComboModel = new ProductInCombo
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Quantity = productCombo.Quantity,
                            ImageUrl = product.ImageUrl,
                            Price = product.Price,
                        };

                        //Assign Size to productComboModel if it is not null
                        if (productCombo.ProductSizeId.HasValue)
                        {
                            var productSize = await _unitOfWork.ProductSize.GetByIdAsync(productCombo.ProductSizeId, includes: ps => ps.Size);

                            productComboModel.Size = productSize.Size.Name.ToString(); //Set Size Number
                        }

                        comboModel.Products.Add(productComboModel);
                    }
                }

                comboModels.Add(comboModel);
            }
            return (comboModels, totalItems);
        }

        public async Task<Product?> GetCombo(int id)
        {
            var product = await _unitOfWork.Product.GetCombo(id);
            if (product == null)
                return null;

            return product;
        }

		public async Task CreateCombo(ComboModel model)
		{
            Product? product = new Product();
            product.Id = model.Id;
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
			product.CategoryId = 2;
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

                foreach(ProductInCombo item in model.Products)
                {
                    ProductCombo combo = new ProductCombo();
                    combo.ProductId = item.Id;
                    combo.Quantity = item.Quantity;
                    combo.Combo = product;

                    if (!string.IsNullOrEmpty(item.Size)){
						ProductSizeEnum sizeEnum = (ProductSizeEnum)Enum.Parse(typeof(ProductSizeEnum), item.Size);
						int sizeValue = (int)sizeEnum;
						ProductSize? productSize = await _unitOfWork.ProductSize.FirstOrDefaultAsync(filter: ps => ps.ProductId == item.Id && ps.SizeId == sizeValue);
                        combo.ProductSizeId = productSize.Id;
                    }
                    else
                    {
						combo.ProductSizeId = null;
						combo.ProductSize = null;
					}
                    await _unitOfWork.ProductCombo.AddAsync(combo);
				}
                await _unitOfWork.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}


 
}
