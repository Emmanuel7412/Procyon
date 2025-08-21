using System;
using Procyon.Core.Abstractions;

namespace TodoList.Application.Features.ShoppingList.Update
{
    public sealed record UpdateShoppingItemCommand(
        Guid ItemId,
        string UpdatedBy,
        string Name,
        int Quantity
    ) : ICommand<bool>;
}
