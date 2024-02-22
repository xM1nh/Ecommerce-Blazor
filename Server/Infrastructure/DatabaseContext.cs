using Microsoft.EntityFrameworkCore;
using Server.Infrastructure.EFConfigs;
using SharedLibrary.Models;

namespace Server.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEFConfig());
        modelBuilder.ApplyConfiguration(new CategoryConfig());
    }
}
