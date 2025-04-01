using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public class SizeRepository : GenericRepository<Size>, ISizeRepository {
        public SizeRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<Size>> GetListSize()
        {
            return await _dbSet
             .ToListAsync();
        }
    }
}
