using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;

namespace TodoList.Infrastructure.Data.Repositories
{
    public class ShoppingItemRepository : IShoppingItemRepository
    {
        private readonly TodoDbContext _dbContext;

        public ShoppingItemRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShoppingItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.ShoppingItems.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<ShoppingItem>> GetAllAsync()
        {
            return await _dbContext.ShoppingItems.ToListAsync();
        }

        public async Task<IEnumerable<ShoppingItem>> GetAllNotPurchasedAsync()
        {
            return await _dbContext.ShoppingItems
                .Where(item => !item.IsPurchased)
                .ToListAsync();
        }

        public async Task AddAsync(ShoppingItem item, CancellationToken cancellation)
        {
            await _dbContext.ShoppingItems.AddAsync(item, cancellation);
            await _dbContext.SaveChangesAsync(cancellation);
        }

        public async Task UpdateAsync(ShoppingItem item)
        {
            _dbContext.ShoppingItems.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellation)
        {
            var item = await GetByIdAsync(id, cancellation);
            if (item != null)
            {
                _dbContext.ShoppingItems.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
