using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Configurations
{
    public class ShoppingItemConfiguration : IEntityTypeConfiguration<ShoppingItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingItem> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.Quantity)
                .IsRequired();

            builder.Property(s => s.IsPurchased)
                .IsRequired();
        }
    }
}
