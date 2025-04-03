using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }

        public async Task<(IEnumerable<Product>, int)> GetMilkTeas(string? search, int? page = null, int? pageSize = null)
        {
            IQueryable<Product> query = _dbSet.Where(p => p.CategoryId == 1);

			if (!string.IsNullOrEmpty(search))
            {
				query = query.Where(e => e.Name.ToLower().Contains(search));
			}

			int totalItems = await query.CountAsync();

			query = query
               .Include(p => p.ProductSizes)
               .ThenInclude(ps => ps.Size);

			query = query
			.OrderBy(p => p.Status == "active" ? 1 : 0)
			.ThenBy(p => p.UpdatedAt).Reverse();

			if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

			return (await query.ToListAsync(), totalItems);
        }

        public async Task<Product?> GetMilkTea(int id)
        {
            IQueryable<Product> query = _dbSet;
            query = query.Include(p => p.ProductSizes)
              .ThenInclude(ps => ps.Size);

			var keyName = _context.Model
								 .FindEntityType(typeof(Product))?
								 .FindPrimaryKey()?
								 .Properties
								 .Select(x => x.Name)
								 .FirstOrDefault(); 

			return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
		}

		public async Task<(IEnumerable<Product>, int)> GetToppings(string? search, int? page = null, int? pageSize = null)
		{
			IQueryable<Product> query = _dbSet.Where(p => p.CategoryId == 2);

			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(e => e.Name.ToLower().Contains(search));
			}

			int totalItems = await query.CountAsync();

			query = query
			.OrderBy(p => p.Status == "active" ? 1 : 0)
			.ThenBy(p => p.UpdatedAt).Reverse();

			if (page.HasValue && pageSize.HasValue)
			{
				query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
			}

			return (await query.ToListAsync(), totalItems);
		}

		public async Task<Product?> GetTopping(int id)
		{
			IQueryable<Product> query = _dbSet;

			var keyName = _context.Model
								 .FindEntityType(typeof(Product))?
								 .FindPrimaryKey()?
								 .Properties
								 .Select(x => x.Name)
								 .FirstOrDefault();

			return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
		}

        public async Task<List<Product>> GetStartMilkTeas()
        {
            return await _dbSet
                .Where(p => p.CategoryId == 1 || p.CategoryId == 2)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .ToListAsync();
        }
    }
}
