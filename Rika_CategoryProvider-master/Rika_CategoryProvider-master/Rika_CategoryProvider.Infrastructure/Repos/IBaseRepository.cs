using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rika_CategoryProvider.Infrastructure.Repos
{
	public interface IBaseRepository<T> where T : class
	{
		Task<T> AddAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(T entity);
		Task<T?> GetByIdAsync(int id);
		Task<T?> GetByNameAsync(Expression<Func<T, bool>> predicate);
		Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetCategoryAsync(Expression<Func<T, bool>> predicate);
    }
}
