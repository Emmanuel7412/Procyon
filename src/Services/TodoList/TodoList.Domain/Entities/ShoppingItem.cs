using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Procyon.Core.Shared.Models;

namespace TodoList.Domain.Entities
{
    public class ShoppingItem : Entity<Guid>
    {
        public ShoppingItem(Guid id, DateTime createdAt, string name, int quantity, bool isPurchased)
        {
            Id = id;
            CreatedAt = createdAt;
            Name = name;
            Quantity = quantity;
            IsPurchased = isPurchased;
        }

        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool IsPurchased { get; set; }
    }
}
