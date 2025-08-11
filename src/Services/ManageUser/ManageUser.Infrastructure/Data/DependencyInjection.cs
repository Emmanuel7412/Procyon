using ManageUser.Domain.Repositories;
using ManageUser.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageUser.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register your DbContext and other data-related services here
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DatabasePG")));

        // Register other data-related services, repositories, etc.
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
