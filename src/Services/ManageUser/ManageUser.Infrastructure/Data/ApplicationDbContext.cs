using ManageUser.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManageUser.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    // DbSet properties for your entities
    public DbSet<TokenInfo> TokenInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entities here ou use configuration files
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }


}
