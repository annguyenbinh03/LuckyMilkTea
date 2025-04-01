using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetMilkTeas()
        {
            return await _dbSet
               .Where(p => p.CategoryId == 1)
               .Include(p => p.ProductSizes)
               .ThenInclude(ps => ps.Size)
               .ToListAsync();
        }
    }
}
