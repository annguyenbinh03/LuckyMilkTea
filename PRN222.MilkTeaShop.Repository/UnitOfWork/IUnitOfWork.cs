using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Category> Category { get; }
        IGenericRepository<Employee> Employee { get; }
        IGenericRepository<Order> Order { get; }
        IGenericRepository<OrderDetail> OrderDetail { get; }
        IGenericRepository<Payment> Payment { get; }
        IGenericRepository<PaymentMethod> PaymentMethod { get; }
        IGenericRepository<Product> Product { get; }
        IGenericRepository<ProductCombo> ProductCombo { get; }
        IGenericRepository<ProductSize> ProductSize { get; }
        IGenericRepository<Size> Size { get; }
        Task SaveChanges();
    }
}
