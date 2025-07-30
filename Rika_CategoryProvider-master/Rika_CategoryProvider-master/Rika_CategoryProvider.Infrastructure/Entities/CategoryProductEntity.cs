
namespace Rika_CategoryProvider.Infrastructure.Entities;

public class CategoryProductEntity
{
	public int CategoryId { get; set; }
	public CategoryEntity Category { get; set; } = null!;
	public int ProductCategoryId { get; set; }
}
