

using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Data;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    // DbSet properties for your entities
    // public DbSet<YourEntity> YourEntities { get; set; }
    public DbSet<ShoppingItem> ShoppingItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure your entities here ou use configuration files
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }


}
