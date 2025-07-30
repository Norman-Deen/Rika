using Microsoft.EntityFrameworkCore;
using Rika_CategoryProvider.Infrastructure.Entities;

namespace Rika_CategoryProvider.Infrastructure.Context
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CategoryProductEntity> CategoryProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProductEntity>()
                .HasKey(cp => new { cp.CategoryId, cp.ProductCategoryId });

            modelBuilder.Entity<CategoryProductEntity>()
                .HasOne(cp => cp.Category)
                .WithMany()
                .HasForeignKey(cp => cp.CategoryId);
        }
    }
}
