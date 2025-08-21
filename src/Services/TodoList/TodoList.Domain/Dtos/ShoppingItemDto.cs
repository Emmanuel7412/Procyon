using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Domain.Dtos
{
    public record struct ShoppingItemDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
