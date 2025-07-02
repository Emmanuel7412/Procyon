using System;
using ManageUser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ManageUser.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        await SeedUsersAsync(dbContext);
    }

    private static async Task SeedUsersAsync(ApplicationDbContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            User user1 = new() { Id = UserId.Of(Guid.NewGuid()), FirstName = "Manu", LastName = "Poirier", Email = "manu@gmail.com", PasswordHash = "hashedpassword1" };
            User user2 = new() { Id = UserId.Of(Guid.NewGuid()), FirstName = "Anna", LastName = "Albino", Email = "anna@gmail.com", PasswordHash = "hashedpassword2" };
            await context.Users.AddRangeAsync([user1, user2]);
            await context.SaveChangesAsync();
        }
    }
}
