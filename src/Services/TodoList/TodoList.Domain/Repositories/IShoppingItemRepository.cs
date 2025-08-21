using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Repositories
{
    public interface IShoppingItemRepository
    {
        Task<ShoppingItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ShoppingItem>> GetAllAsync();
        Task<IEnumerable<ShoppingItem>> GetAllNotPurchasedAsync();
        Task AddAsync(ShoppingItem item, CancellationToken cancellation);
        Task UpdateAsync(ShoppingItem item);
        Task DeleteAsync(Guid id, CancellationToken cancellation);
    }
}
