using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Models;
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
            IQueryable<Product> query = _dbSet;

            int totalItems = await query.Where(p => p.CategoryId == 1).CountAsync();

            query = query
               .Where(p => p.CategoryId == 1)
               .Include(p => p.ProductSizes)
               .ThenInclude(ps => ps.Size);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, e.Name), $"%{search}%"));
            }
            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return (await query.ToListAsync(), totalItems);
        }
    }
}
