using Microsoft.EntityFrameworkCore;
using Rika_OrderProvider.Infrastructure.Data.Contexts;
using Rika_OrderProvider.Infrastructure.Data.Entities;
using System.Linq.Expressions;

namespace Rika_OrderProvider.Infrastructure.Data.Repositories;

public class OrderProductRepository(OrderDbContext dbContext) : BaseRepository<OrderProductEntity>(dbContext)
{
    private readonly OrderDbContext _dbContext = dbContext;

    public async Task<List<OrderProductEntity>> GetAllProductsAsync(Expression<Func<OrderProductEntity, bool>> filter)
    {
        return await _dbContext.OrderProducts.Where(filter).ToListAsync();
    }
}
