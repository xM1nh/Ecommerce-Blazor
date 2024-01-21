using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;

namespace Server.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}
