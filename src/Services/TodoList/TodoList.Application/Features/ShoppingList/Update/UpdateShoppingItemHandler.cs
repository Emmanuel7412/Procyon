using System.Threading;
using System.Threading.Tasks;
using Core.Shared;
using Procyon.Core.Abstractions;
using TodoList.Domain.Repositories;

namespace TodoList.Application.Features.ShoppingList.Update
{
    public class UpdateShoppingItemHandler(IShoppingItemRepository shoppingItemRepository) : ICommandHandler<UpdateShoppingItemCommand, bool>
    {

        async Task<Result<bool>> ICommandHandler<UpdateShoppingItemCommand, bool>.Handle(UpdateShoppingItemCommand command, CancellationToken cancellation)
        {
            var item = await shoppingItemRepository.GetByIdAsync(command.ItemId, cancellation);
            if (item == null)
            {
                return Result.Failure<bool>(Error.NotFound);
            }

            item.Name = command.Name;
            item.Quantity = command.Quantity;
            await shoppingItemRepository.UpdateAsync(item);

            return Result.Success(true);
        }
    }
}
