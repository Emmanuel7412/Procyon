using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared;
using Procyon.Core.Abstractions;
using Procyon.Core.Extensions;
using Procyon.Core.Shared;
using Procyon.Core.Shared.API;
using TodoList.Application.Features.ShoppingList.Create;

namespace TodoList.API.Endpoints.ShoppingItems
{
    public sealed class Request
    {
        public Guid CreateBy { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
    public class Create : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/shopping-items");
            group.MapPost("", async (HttpContext context, Request createRequest, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
            {
                // Handle create logic
                var createCommand = new CreateShoppingItemCommand(createRequest.CreateBy.ToString(), createRequest.Name, createRequest.Quantity);

                Result<Guid> result = await commandDispatcher.Dispatch<CreateShoppingItemCommand, Guid>(createCommand, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            });
        }
    }
}
