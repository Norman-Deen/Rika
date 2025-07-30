using Rika_OrderProvider.Infrastructure.Data.Contexts;
using Rika_OrderProvider.Infrastructure.Data.Entities;

namespace Rika_OrderProvider.Infrastructure.Data.Repositories;

public class OrderCustomerRepository(OrderDbContext dbContext) : BaseRepository<OrderCustomerEntity>(dbContext)
{

}
