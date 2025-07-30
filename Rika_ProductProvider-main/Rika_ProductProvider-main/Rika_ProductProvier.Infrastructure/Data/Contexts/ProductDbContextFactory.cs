using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rika_ProductProvier.Infrastructure.Data.Contexts;

public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
{
    public ProductDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
        optionsBuilder.UseSqlServer("add connection string whenever needed, when adding new migrations etc.");
        return new ProductDbContext(optionsBuilder.Options);
    }
}
