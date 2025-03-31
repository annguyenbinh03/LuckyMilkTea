using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<(IEnumerable<T>, int)> GetAsync(Expression<Func<T, bool>>? filter = null, string? search = null, Expression<Func<T, object>>? searchBy = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool descending = false, int? page = null, int? pageSize = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(search) && searchBy != null)
            {
                string propertyName = GetPropertyName(searchBy);
                query = query.Where(e => EF.Functions.Like(EF.Property<string>(e, propertyName), $"%{search}%"));
            }

            int totalItems = await query.CountAsync();

            if (orderBy != null)
            {
                query = orderBy(query);
                query = descending ? orderBy(query).Reverse() : orderBy(query);
            }

            if (page.HasValue && pageSize.HasValue)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return (await query.ToListAsync(), totalItems);
        }

        public async Task<T?> GetByIdAsync(object id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            // Thêm Include nếu có
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            // Lấy tên của khóa chính từ EF Core metadata
            var keyName = _context.Model
                                  .FindEntityType(typeof(T))?
                                  .FindPrimaryKey()?
                                  .Properties
                                  .Select(x => x.Name)
                                  .FirstOrDefault(); // Lấy tên của primary key

            if (keyName == null)
                throw new InvalidOperationException($"Can not find primary key for {typeof(T).Name}");

            // Tìm entity theo khóa chính
            return await query.FirstOrDefaultAsync(e => EF.Property<object>(e, keyName) == id);
        }

        public async Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        private string GetPropertyName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            else if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression operand)
            {
                return operand.Member.Name;
            }
            throw new InvalidOperationException("Invalid expression format");
        }
    }
}
