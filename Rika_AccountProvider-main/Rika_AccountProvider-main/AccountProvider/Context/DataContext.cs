using AccountProvider.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountProvider.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<UserEntity> Users { get; set; }
}
