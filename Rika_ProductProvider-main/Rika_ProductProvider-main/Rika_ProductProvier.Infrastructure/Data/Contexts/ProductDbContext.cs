using Microsoft.EntityFrameworkCore;
using Rika_ProductProvier.Infrastructure.Data.Entities;

namespace Rika_ProductProvier.Infrastructure.Data.Contexts;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductColorEntity> ProductColors { get; set; }
    public DbSet<ProductSizeEntity> ProductSizes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductEntity>()
            .HasOne(p => p.ProductColor)
            .WithMany(pc => pc.Products)
            .HasForeignKey(p => p.ProductColorId);

        // Unique Indexes
        modelBuilder.Entity<ProductEntity>()
            .HasIndex(p => new { p.ProductName, p.ProductColorId, p.ProductSizeId })
            .IsUnique();

        modelBuilder.Entity<ProductColorEntity>()
            .HasIndex(pc => pc.ColorName)
            .IsUnique();

        modelBuilder.Entity<ProductSizeEntity>()
            .HasIndex(ps => ps.ProductSizeName)
            .IsUnique();
    }
}