using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Procyon.Core.Abstractions;
using Procyon.Core.Extensions;
using Procyon.Core.Shared;
using Procyon.Core.Shared.API;
using TodoList.Application.Features.ShoppingList.Create;
using TodoList.Application.Features.ShoppingList.Delete;

namespace TodoList.API.Endpoints.ShoppingItems
{

    public class Delete : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/shopping-items");
            group.MapDelete("/{id}", async (string id, HttpContext context, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
            {
                var itemId = Guid.TryParse(id, out var parsedId) ? parsedId : throw new ArgumentException("Invalid item ID format", nameof(id));
                var deleteCommand = new DeleteShoppingItemCommand(itemId);

                Result<Guid> result = await commandDispatcher.Dispatch<DeleteShoppingItemCommand, Guid>(deleteCommand, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            });
        }
    }
}
