using license.Entity;
using Microsoft.EntityFrameworkCore;

namespace license;

public class UserDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<User> Users { get; set; }

    protected readonly IConfiguration Configuration;

    public UserDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(Configuration.GetConnectionString("Database"));
    }
}