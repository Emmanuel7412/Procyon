using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Procyon.Core.Abstractions;

namespace TodoList.Application.Features.ShoppingList.Create
{
    public sealed record CreateShoppingItemCommand(string CreatedBy, string Name, int Quantity) : ICommand<Guid>;


}
