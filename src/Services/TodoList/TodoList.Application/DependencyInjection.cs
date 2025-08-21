using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Procyon.Core.Abstractions;
using TodoList.Application.Features.ShoppingList.Get;
using TodoList.Domain.Dtos;

namespace TodoList.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register application services here
            // services.AddScoped<ICommandHandler<CreateShoppingItemCommand, Guid>, CreateShoppingItemHandler>();
            //services.AddScoped<IQueryHandler<GetShoppingListQuery, List<ShoppingItemDto>>, GetShoppingListHandler>();

            return services;
        }


    }
}
