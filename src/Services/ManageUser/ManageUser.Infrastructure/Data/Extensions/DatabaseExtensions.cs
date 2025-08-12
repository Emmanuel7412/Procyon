using System;
using ManageUser.Domain.Constants;
using ManageUser.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ManageUser.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        await SeedUsersAsync(serviceProvider);
    }

    private static async Task SeedUsersAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationUser>>();
        var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

        if (userManager == null || roleManager == null)
        {
            throw new InvalidOperationException("UserManager or RoleManager is not configured.");
        }


        // Ensure roles exist
        await CreateRoles(logger, roleManager, Roles.Admin, Roles.User);

        // Seed users

        ApplicationUser user1 = new()
        {
            UserName = "manu@gmail.com",
            FirstName = "Manu",
            LastName = "Poirier",
            Email = "manu@gmail.com",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        ApplicationUser user2 = new()
        {
            UserName = "anna@gmail.com",
            FirstName = "Anna",
            LastName = "Albino",
            Email = "anna@gmail.com",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        await CreateUser(logger, userManager, new Tuple<ApplicationUser, string>(user1, Roles.Admin), new Tuple<ApplicationUser, string>(user2, Roles.User));

    }


    private static async Task CreateUser(ILogger<ApplicationUser> logger, UserManager<ApplicationUser> userManager, params Tuple<ApplicationUser, string>[] userInfos)
    {
        foreach (var userInfo in userInfos)
        {
            if (await userManager.FindByEmailAsync(userInfo.Item1?.Email) == null)
            {

                var createUserResult = await userManager
                      .CreateAsync(user: userInfo.Item1, password: "Pass@123");

                // Validate user creatio
                if (createUserResult.Succeeded == false)
                {
                    var errors = createUserResult.Errors.Select(e => e.Description);
                    logger.LogError(
                        $"Failed to create admin user. Errors: {string.Join(", ", errors)}"
                    );

                }

                // adding role to user
                var addUserToRoleResult = await userManager
                                .AddToRoleAsync(user: userInfo.Item1, role: userInfo.Item2);

                if (addUserToRoleResult.Succeeded == false)
                {
                    var errors = addUserToRoleResult.Errors.Select(e => e.Description);
                    logger.LogError($"Failed to add {userInfo.Item2} role to user. Errors : {string.Join(",", errors)}");
                }
                logger.LogInformation("User is created");
            }
        }
    }

    private static async Task CreateRoles(ILogger<ApplicationUser> logger, RoleManager<IdentityRole> roleManager, params string[] rolenames)
    {
        foreach (var rolename in rolenames)
        {
            if (!await roleManager.RoleExistsAsync(rolename))
            {
                logger.LogInformation($"{rolename} role is creating");
                var roleResult = await roleManager.CreateAsync(new IdentityRole(rolename));
                if (roleResult.Succeeded == false)
                {
                    var roleErros = roleResult.Errors.Select(e => e.Description);
                    logger.LogError($"Failed to create {rolename} role. Errors : {string.Join(",", roleErros)}");


                }
                logger.LogInformation($"{rolename} role is created");
            }
        }

    }
}
