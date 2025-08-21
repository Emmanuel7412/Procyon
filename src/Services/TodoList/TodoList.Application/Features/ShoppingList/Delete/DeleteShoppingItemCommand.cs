using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Procyon.Core.Abstractions;

namespace TodoList.Application.Features.ShoppingList.Delete
{
    public sealed record DeleteShoppingItemCommand(Guid Id) : ICommand<Guid>;
}
