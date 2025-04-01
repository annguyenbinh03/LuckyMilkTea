using PRN222.MilkTeaShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
       Task<List<Product>> GetMilkTeas();
    }
}
