using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Repository.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<(IEnumerable<T>, int)> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        string? search = null,
        Expression<Func<T, object>>? searchBy = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool descending = false,
        int? page = null,
        int? pageSize = null,
        params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(object id, params Expression<Func<T, object>>[] includes);
        Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(object id);

        Task<int> CountAsync();
    }
}
