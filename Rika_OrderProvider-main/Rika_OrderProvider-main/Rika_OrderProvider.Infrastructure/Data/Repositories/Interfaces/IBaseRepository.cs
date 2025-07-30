using System.Linq.Expressions;

namespace Rika_OrderProvider.Infrastructure.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<T> GetOneAsync(Expression<Func<T, bool>> filter);
    Task<List<T>> GetAllAsync();
    Task<T> UpdateAsync(Expression<Func<T, bool>> filter, T entity);
    Task<bool> DeleteAsync(Expression<Func<T, bool>> filter);
    Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
}