using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.Repositories;

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

    public async Task<List<Product>> GetProductsByCategoryIds(List<int> categoryIds)
    {
        return await _dbSet
           .Where(p => categoryIds.Contains(p.CategoryId))
           .Include(p => p.ProductSizes)
           .ThenInclude(ps => ps.Size)
           .ToListAsync();
    }
}
