using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Procyon.Core.Abstractions;
using Procyon.Core.Extensions;
using Procyon.Core.Shared;
using Procyon.Core.Shared.API;
using TodoList.Application.Features.ShoppingList.Update;

namespace TodoList.API.Endpoints.ShoppingItems
{
    public sealed class UpdateRequest
    {
        public Guid Id { get; set; }
        public Guid UpdatedBy { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }

    public class Update : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/shopping-items");
            group.MapPut("", async (HttpContext context, UpdateRequest updateRequest, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
            {
                var updateCommand = new UpdateShoppingItemCommand(
                    updateRequest.Id,
                    updateRequest.UpdatedBy.ToString(),
                    updateRequest.Name,
                    updateRequest.Quantity
                );

                Result<bool> result = await commandDispatcher.Dispatch<UpdateShoppingItemCommand, bool>(updateCommand, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            });
        }
    }
}
