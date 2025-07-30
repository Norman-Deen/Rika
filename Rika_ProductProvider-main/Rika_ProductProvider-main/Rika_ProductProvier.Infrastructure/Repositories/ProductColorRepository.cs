using Rika_ProductProvier.Infrastructure.Data.Contexts;
using Rika_ProductProvier.Infrastructure.Data.Entities;

namespace Rika_ProductProvier.Infrastructure.Repositories;

public class ProductColorRepository(ProductDbContext context) : BaseRepository<ProductColorEntity>(context)
{
    private readonly ProductDbContext _context = context;
}
