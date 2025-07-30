using Microsoft.EntityFrameworkCore;
using Rika_OrderProvider.Infrastructure.Data.Contexts;
using Rika_OrderProvider.Infrastructure.Data.Repositories.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Rika_OrderProvider.Infrastructure.Data.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly OrderDbContext _dbContext;

    public BaseRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        try
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR:: CreateAsync: " + ex.Message); }
        return null!;
    }

    public virtual async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> filter)
    {
        try
        {
            var result = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
            if (result != null)
            {
                return result!;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: GetOneAsync: " + ex.Message); }
        return null!;
    }

    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        try
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: GetOneAsync: " + ex.Message); }
        return null!;
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> filter, TEntity updatedEntity)
    {
        try
        {
            var entity = await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
            if (entity != null && updatedEntity != null)
            {
                _dbContext.Entry(entity).CurrentValues.SetValues(updatedEntity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: UpdateAsync: " + ex.Message); }
        return null!;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> filter)
    {
        try
        {
            var entity = await _dbContext.Set<TEntity>().SingleOrDefaultAsync(filter);
            if(entity != null)
            {
                _dbContext.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: DeleteAsync: " + ex.Message); }
        return false;
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
    {
        try
        {
            return await _dbContext.Set<TEntity>().AnyAsync(filter);
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: ExistsAsync: " + ex.Message); }
        return false;
    }
}
