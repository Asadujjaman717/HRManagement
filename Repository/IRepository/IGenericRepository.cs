using System.Linq.Expressions;

namespace HrManagement.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
