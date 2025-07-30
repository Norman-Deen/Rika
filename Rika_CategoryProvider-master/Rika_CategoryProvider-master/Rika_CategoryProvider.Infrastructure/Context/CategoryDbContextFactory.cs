

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rika_CategoryProvider.Infrastructure.Context;

public class CategoryDbContextFactory : IDesignTimeDbContextFactory<CategoryDbContext>
{
	public CategoryDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<CategoryDbContext>();
		optionsBuilder.UseSqlServer("Server=tcp:rika-category-provider.database.windows.net,1433;Initial Catalog=Rika_CategoryProviderDb;Persist Security Info=False;User ID=category;Password=BytMig123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
		return new CategoryDbContext(optionsBuilder.Options);
	}
}
