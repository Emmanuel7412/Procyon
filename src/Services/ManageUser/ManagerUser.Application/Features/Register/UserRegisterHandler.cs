using Core.Abstractions;
using ManageUser.Application.Exceptions;
using ManageUser.Domain.Abstractions;
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
        User? existingUser = await userRepository.FindByEmailAsync(command.UserRegister.Email);
        if (existingUser != null)
        {
            throw new ExistingEmailException(command.UserRegister.Email);
        }
        if (command.UserRegister.Password != command.UserRegister.ConfirmPassword)
        {
            throw new ConfirmPasswordException();
        }
        User newUser = CreateNewUser(command.UserRegister);
        newUser.PasswordHash = tokenTools.HashPassword(newUser, command.UserRegister.Password);
        UserId newUserId = await userRepository.CreateUserAsync(newUser, cancellation);
        return new UserRegisterResponse(newUserId);
        try
        {
            return new UserRegisterResponse("id");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while processing the registration.", ex);
        }
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
