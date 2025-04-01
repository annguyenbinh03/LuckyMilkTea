using Microsoft.EntityFrameworkCore;
using PRN222.MilkTeaShop.Repository.DbContexts;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.UnitOfWork
{
    public class UnitOfWork(MilkTeaDBContext context) : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context = context;
        private IGenericRepository<Category>? _categoryRepository;
        private IGenericRepository<Employee>? _employeeRepository;
        private IGenericRepository<Order>? _orderRepository;
        private IGenericRepository<OrderDetail>? _orderDetailRepository;
        private IGenericRepository<Payment>? _paymentRepository;
        private IGenericRepository<PaymentMethod>? _paymentMethodRepository;
        private IProductRepository? _productRepository;
        private IGenericRepository<ProductCombo>? _productComboRepository;
        private IGenericRepository<ProductSize>? _productSizeRepository;
        //private IGenericRepository<Size>? _sizeRepository;
        private ISizeRepository? _sizeRepository;

        public IGenericRepository<Category> Category
        {
            get
            {

                if (this._categoryRepository == null)
                {
                    this._categoryRepository = new GenericRepository<Category>(context);
                }
                return _categoryRepository;
            }

        }

        public IGenericRepository<Employee> Employee {
            get
            {

                if (this._employeeRepository == null)
                {
                    this._employeeRepository = new GenericRepository<Employee>(context);
                }
                return _employeeRepository;
            }
        }

        public IGenericRepository<Order> Order
        {
            get
            {

                if (this._orderRepository == null)
                {
                    this._orderRepository = new GenericRepository<Order>(context);
                }
                return _orderRepository;
            }
        }

        public IGenericRepository<OrderDetail> OrderDetail
        {
            get
            {

                if (this._orderDetailRepository == null)
                {
                    this._orderDetailRepository = new GenericRepository<OrderDetail>(context);
                }
                return _orderDetailRepository;
            }
        }

        public IGenericRepository<Payment> Payment
        {
            get
            {

                if (this._paymentRepository == null)
                {
                    this._paymentRepository = new GenericRepository<Payment>(context);
                }
                return _paymentRepository;
            }
        }

        public IGenericRepository<PaymentMethod> PaymentMethod
        {
            get
            {

                if (this._paymentMethodRepository == null)
                {
                    this._paymentMethodRepository = new GenericRepository<PaymentMethod>(context);
                }
                return _paymentMethodRepository;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (this._productRepository == null)
                {
                    this._productRepository = new ProductRepository(context);
                }
                return _productRepository;
            }
        }

        public IGenericRepository<ProductCombo> ProductCombo
        {
            get
            {
                if (this._productComboRepository == null)
                {
                    this._productComboRepository = new GenericRepository<ProductCombo>(context);
                }
                return _productComboRepository;
            }
        }

        public IGenericRepository<ProductSize> ProductSize
        {
            get
            {
                if (this._productSizeRepository == null)
                {
                    this._productSizeRepository = new GenericRepository<ProductSize>(context);
                }
                return _productSizeRepository;
            }
        }

        //public IGenericRepository<Size> Size
        //{
        //    get
        //    {
        //        if (this._sizeRepository == null)
        //        {
        //            this._sizeRepository = new GenericRepository<Size>(context);
        //        }
        //        return _sizeRepository;
        //    }
        //}
        public ISizeRepository Size
        {
            get
            {
                if (this._sizeRepository == null)
                {
                    this._sizeRepository = new SizeRepository(context);
                }
                return _sizeRepository;
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
