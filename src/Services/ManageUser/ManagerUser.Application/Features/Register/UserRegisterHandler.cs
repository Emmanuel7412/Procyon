using System;
using Core.Abstractions;
using ManageUser.Application.Exceptions;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using Procyon.Core.Exceptions;

namespace ManageUser.Application.Features.Register;

public class UserRegisterHandler(IUserRepository userRepository, ITokenTools tokenTools) : ICommandHandler<UserRegisterCommand, UserRegisterResponse>
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
    }

    private static User CreateNewUser(UserRegister userRegister)
    {
        return new User
        {
            FirstName = userRegister.FirstName,
            LastName = userRegister.LastName,
            Email = userRegister.Email
        };
    }
}
