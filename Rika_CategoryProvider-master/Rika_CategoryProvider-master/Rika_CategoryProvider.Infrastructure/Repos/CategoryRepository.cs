using Microsoft.EntityFrameworkCore;
using Rika_CategoryProvider.Infrastructure.Context;
using Rika_CategoryProvider.Infrastructure.Entities;
using System.Linq.Expressions;

namespace Rika_CategoryProvider.Infrastructure.Repos;

public class CategoryRepository : BaseRepository<CategoryEntity>
{
    public CategoryRepository(CategoryDbContext context) : base(context)
    {
    }
    
}

