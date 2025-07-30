using Microsoft.EntityFrameworkCore;
using Rika_OrderProvider.Infrastructure.Data.Entities;

namespace Rika_OrderProvider.Infrastructure.Data.Contexts;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderProductEntity> OrderProducts { get; set; }
    public DbSet<OrderCustomerEntity> OrderCustomers { get; set; }
    public DbSet<OrderAddressEntity> OrderAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.OrderCustomer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.OrderCustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        

        modelBuilder.Entity<OrderEntity>()
            .HasOne(o => o.OrderAddress)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.OrderAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderEntity>()
            .HasMany(o => o.OrderProducts)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderProductEntity>()
            .HasKey(op => new { op.OrderId, op.ArticleNumber });
    }
}