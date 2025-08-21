using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared;
using Procyon.Core.Abstractions;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;

namespace TodoList.Application.Features.ShoppingList.Create
{
    public class CreateShoppingItemHandler(IShoppingItemRepository shoppingItemRepository) : ICommandHandler<CreateShoppingItemCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateShoppingItemCommand command, CancellationToken cancellation)
        {
            // Implementation for creating a shopping item
            var shoppingItem = new ShoppingItem(Guid.NewGuid(), DateTime.UtcNow, command.CreatedBy, command.Name, command.Quantity, false);

            await shoppingItemRepository.AddAsync(shoppingItem, cancellation);

            return Result.Success(shoppingItem.Id);
        }
    }

}
