using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared;
using Procyon.Core.Abstractions;
using TodoList.Domain.Repositories;

namespace TodoList.Application.Features.ShoppingList.Delete
{
    public class DeleteShoppingItemCommandHandler(IShoppingItemRepository repository) : ICommandHandler<DeleteShoppingItemCommand, Guid>
    {

        public async Task<Result<Guid>> Handle(DeleteShoppingItemCommand command, CancellationToken cancellationToken)
        {
            var item = await repository.GetByIdAsync(command.Id, cancellationToken);
            if (item == null)
                return Result.Failure<Guid>(Error.NotFound);

            await repository.DeleteAsync(item.Id, cancellationToken);
            return Result.Success(command.Id);
        }
    }
}
