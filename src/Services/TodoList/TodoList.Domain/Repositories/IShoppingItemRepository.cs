using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Repositories
{
    public interface IShoppingItemRepository
    {
        Task<ShoppingItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<ShoppingItem>> GetAllAsync();
        Task<IEnumerable<ShoppingItem>> GetAllNotPurchasedAsync();
        Task AddAsync(ShoppingItem item);
        Task UpdateAsync(ShoppingItem item);
        Task DeleteAsync(Guid id);
    }
}
