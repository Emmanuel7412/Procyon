using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared;
using Procyon.Core.Abstractions;
using Procyon.Core.Extensions;
using Procyon.Core.Shared;
using Procyon.Core.Shared.API;
using TodoList.Application.Features.ShoppingList.Get;
using TodoList.Domain.Dtos;

namespace TodoList.API.Endpoints.ShoppingItems
{
    public class Get : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/shopping-items", async (HttpContext context, IQueryDispatcher queryDispatcher, CancellationToken cancellationToken) =>
        {

            Result<List<ShoppingItemDto>> result = await queryDispatcher.Dispatch<GetShoppingListQuery, List<ShoppingItemDto>>(new GetShoppingListQuery(), cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        });
        }
    }
}
