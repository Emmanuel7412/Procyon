using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Shared;
using Procyon.Core.Abstractions;
using TodoList.Domain.Dtos;
using TodoList.Domain.Repositories;

namespace TodoList.Application.Features.ShoppingList.Get
{
    public class GetShoppingListHandler(
        IShoppingItemRepository shoppingItemRepository) : IQueryHandler<GetShoppingListQuery, List<ShoppingItemDto>>
    {
        private readonly IShoppingItemRepository _shoppingItemRepository = shoppingItemRepository;

        public async Task<Result<List<ShoppingItemDto>>> Handle(GetShoppingListQuery query, CancellationToken cancellationToken)
        {
            var items = await _shoppingItemRepository.GetAllAsync();
            var itemDtos = items.Select(item => new ShoppingItemDto
            {
                Id = item.Id,
                CreatedAt = item.CreatedAt,
                CreatedBy = item.CreatedBy,
                Name = item.Name,
                Quantity = item.Quantity
            }).ToList();

            return Result.Success(itemDtos);
        }
    }
}
