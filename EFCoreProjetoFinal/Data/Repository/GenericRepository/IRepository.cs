using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace EFCoreProjetoFinal.Data.Repository.GenericRepository
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);

        Task<T> GetByIdAsync(Guid id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetDataAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            int? skip = null,
            int? take = null);

        Task<bool> SaveChanges();
    }
}
