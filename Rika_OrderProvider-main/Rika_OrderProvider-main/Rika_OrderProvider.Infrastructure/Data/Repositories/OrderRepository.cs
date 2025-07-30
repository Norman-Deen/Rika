using Microsoft.EntityFrameworkCore;
using Rika_OrderProvider.Infrastructure.Data.Contexts;
using Rika_OrderProvider.Infrastructure.Data.Entities;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text.Json;

namespace Rika_OrderProvider.Infrastructure.Data.Repositories;

public class OrderRepository(OrderDbContext dbContext) : BaseRepository<OrderEntity>(dbContext)
{
    private readonly OrderDbContext _dbContext = dbContext;
    public override async Task<List<OrderEntity>> GetAllAsync()
    {
        try
        {
            var orders = await _dbContext.Orders
                 .Include(x => x.OrderCustomer)
                 .Include(x => x.OrderAddress)
                 .Include(x => x.OrderProducts)
                 .ToListAsync();
            return orders;

        }
        catch (Exception ex)
        {
            var message = ex;
            return null!;
        }
    }

    public override async Task<OrderEntity> GetOneAsync(Expression<Func<OrderEntity, bool>> filter)
    {
        try
        {
            var order = await _dbContext.Orders
                 .Include(x => x.OrderCustomer)
                 .Include(x => x.OrderAddress)
                 .Include(x => x.OrderProducts)
                 .FirstOrDefaultAsync(filter);

            if (order != null)
            {
                return order!;
            }
           

        }
        catch (Exception ex)
        {
            var message = ex;
            
        }
        return null!;
    }
}
