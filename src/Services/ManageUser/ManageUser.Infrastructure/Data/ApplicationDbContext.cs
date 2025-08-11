using ManageUser.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageUser.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    // DbSet properties for your entities
    // public DbSet<YourEntity> YourEntities { get; set; }
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entities here ou use configuration files
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }


}
