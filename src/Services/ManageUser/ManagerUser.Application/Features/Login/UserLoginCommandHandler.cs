using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Core.Abstractions;
using ManageUser.Application;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ManageUser.Application.Features.Login
{
    public class UserLoginCommandHandler(IUserRepository userRepository, ITokenTools tokenTools, IConfiguration configuration) : ICommandHandler<UserLoginCommand, UserLoginResponse>
    {

        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        public async Task<UserLoginResponse> Handle(UserLoginCommand command, CancellationToken cancellation)
        {
            User? user = await userRepository.FindByEmailAsync(command.UserLogin.Email);
            if (user == null || string.IsNullOrEmpty(command.UserLogin.Password) || !VerifyPassword(user, command.UserLogin.Password))
            {
                return new UserLoginResponse(null); ; // User not found or password is incorrect
            }
            var token = await tokenTools.GenerateTokenAsync(new UserTokenGenerate
            {
                User = user,
                ExpireDate = DateTime.Now.AddMinutes(
                    double.TryParse(configuration["Jwt:ExpireAccess"], out var expireMinutes)
                     ? expireMinutes : throw new InvalidOperationException("JWT expiration time not configured (Jwt:ExpireAccess)."))
            });
            return new UserLoginResponse(token);

        }


        private bool VerifyPassword(User user, string password)
        {
            try
            {
                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                return result == PasswordVerificationResult.Success;
            }
            catch (Exception)
            {
                // Handle exceptions related to password verification
                throw new InvalidOperationException("Password verification failed. Please check the password format or hashing algorithm.");

            }
        }
    }
}
