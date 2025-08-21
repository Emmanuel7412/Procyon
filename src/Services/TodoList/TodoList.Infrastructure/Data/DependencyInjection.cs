
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Domain.Repositories;
using TodoList.Infrastructure.Data.Repositories;

namespace TodoList.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register your DbContext and other data-related services here
        services.AddDbContext<TodoDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DatabasePG")));

        // Register other data-related services, repositories, etc.
        services.AddScoped<IShoppingItemRepository, ShoppingItemRepository>();

        return services;
    }
}
