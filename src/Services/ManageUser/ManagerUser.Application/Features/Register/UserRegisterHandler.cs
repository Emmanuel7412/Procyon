using Core.Abstractions;
using ManageUser.Application.Exceptions;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.Constants;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Procyon.Core.Exceptions;

namespace ManageUser.Application.Features.Register;

public class UserRegisterHandler(UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager, ILogger<UserRegisterHandler> logger) : ICommandHandler<UserRegisterCommand, UserRegisterResponse>
{
    public async Task<UserRegisterResponse> Handle(UserRegisterCommand command, CancellationToken cancellation)
    {

        var user = command.UserRegister;
        ApplicationUser? existingUser = await userManager.FindByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new ExistingEmailException(user.Email);
        }
        if (user.Password != user.ConfirmPassword)
        {
            throw new ConfirmPasswordException();
        }
        ApplicationUser newUser = new()
        {
            Email = user.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = user.Email,
            LastName = user.LastName,
            FirstName = user.FirstName,
            EmailConfirmed = true
        };

        var createUserResult = await userManager.CreateAsync(newUser, user.Password);

        if (createUserResult.Succeeded == false)
        {
            var errors = createUserResult.Errors.Select(e => e.Description);
            logger.LogError(
                $"Failed to create user. Errors: {string.Join(", ", errors)}"
            );
            throw new InternalServerException($"Failed to create user. Errors: {string.Join(", ", errors)}");
        }
        var addUserToRoleResult = await userManager.AddToRoleAsync(user: newUser, role: Roles.User);
        if (addUserToRoleResult.Succeeded == false)
        {
            var errors = addUserToRoleResult.Errors.Select(e => e.Description);
            logger.LogError($"Failed to add role to the user. Errors : {string.Join(",", errors)}");
        }
        return new UserRegisterResponse(newUser.Id);



        // User? existingUser = await userRepository.FindByEmailAsync(command.UserRegister.Email);
        // if (existingUser != null)
        // {
        //     throw new InvalidOperationException("User with this email already exists.");
        // }
        // if (command.UserRegister.Password != command.UserRegister.ConfirmPassword)
        // {
        //     throw new InvalidOperationException("Passwords do not match.");
        // }
        // User newUser = CreateNewUser(command.UserRegister);
        // newUser.PasswordHash = tokenTools.HashPassword(newUser, command.UserRegister.Password);
        // UserId newUserId = await userRepository.CreateUserAsync(newUser, cancellation);
        // return new UserRegisterResponse(newUserId);
    }

    // private static User CreateNewUser(UserRegister userRegister)
    // {
    //     return new User
    //     {
    //         FirstName = userRegister.FirstName,
    //         LastName = userRegister.LastName,
    //         Email = userRegister.Email
    //     };
    // }
}
