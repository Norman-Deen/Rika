using Microsoft.EntityFrameworkCore;
using Rika_CategoryProvider.Infrastructure.Context;
using Rika_CategoryProvider.Infrastructure.Entities;
using System.Linq.Expressions;

namespace Rika_CategoryProvider.Infrastructure.Repos;

public class BaseRepository <T> : IBaseRepository<T> where T : class
{
    private readonly CategoryDbContext _context;

    public BaseRepository(CategoryDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> GetByNameAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<T> GetCategoryAsync(Expression<Func<T, bool>> predicate)
    {
        try
        {
            if (await _context.Set<T>().AnyAsync(predicate))
            {
                return await _context.Set<T>().FirstOrDefaultAsync(predicate);
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync()

    {
        return await _context.Set<T>().ToListAsync();
    }
}

