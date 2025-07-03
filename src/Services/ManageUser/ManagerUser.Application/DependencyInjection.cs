using System;
using Core;
using Core.Abstractions;
using ManageUser.Application.Features.Login;
using ManageUser.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageUser.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register application services here
        services.AddScoped<ITokenTools, TokenTools>(); // Example of registering a token service
        return services;
    }

}
