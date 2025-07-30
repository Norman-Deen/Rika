using Microsoft.EntityFrameworkCore;
using Rika_ProductProvier.Infrastructure.Data.Contexts;
using Rika_ProductProvier.Infrastructure.Data.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rika_ProductProvier.Infrastructure.Repositories;

public class ProductRepository(ProductDbContext context) : BaseRepository<ProductEntity>(context)
{
    private readonly ProductDbContext _context = context;

    public override async Task<ProductEntity> CreateOneAsync(ProductEntity entity)
    {
        try
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = await _context.Products
                .Include(x => x.ProductColor)
                .Include(x => x.ProductSize)
                .FirstOrDefaultAsync(x => x.Id == entity.Id );

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public override async Task<List<ProductEntity>> GetAllAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        try
        {
            return await _context.Products
                .Include(x => x.ProductColor)
                .Include(x => x.ProductSize)
                .Where(predicate)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public override async Task<List<ProductEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Products
                .Include(x => x.ProductColor)
                .Include(x => x.ProductSize)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public override async Task<ProductEntity> GetOneAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        try
        {
            var result = await _context.Products
                .Include(x => x.ProductColor)
                .Include(x => x.ProductSize)
                .FirstOrDefaultAsync(predicate);
            return result!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public override async Task<ProductEntity> UpdateOneAsync(ProductEntity entity)
    {
        try
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            var updatedEntity = await _context.Products.Include(x => x.ProductColor).Include(x => x.ProductSize).FirstOrDefaultAsync(x => x.Id == entity.Id);

                return updatedEntity!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}