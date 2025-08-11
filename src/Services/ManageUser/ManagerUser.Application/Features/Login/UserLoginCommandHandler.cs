using System.IdentityModel.Tokens.Jwt;
using Core.Abstractions;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace ManageUser.Application.Features.Login
{
    public class UserLoginCommandHandler(IUserRepository userRepository, ITokenTools tokenTools, IConfiguration configuration) : ICommandHandler<UserLoginCommand, UserLoginResponse>
    {

        public async Task<UserLoginResponse?> Handle(UserLoginCommand command, CancellationToken cancellation)
        {
            User? user = await userRepository.FindByEmailAsync(command.UserLogin.Email);
            if (user == null || string.IsNullOrEmpty(command.UserLogin.Password) || !tokenTools.VerifyPassword(user, command.UserLogin.Password))
            {
                return null; // User not found or password is incorrect
            }
            var token = await tokenTools.GenerateTokenAsync(new UserTokenGenerate
            {
                User = user,
                ExpireDate = DateTime.Now.AddMinutes(
                    double.TryParse(configuration["Jwt:ExpireAccess"], out var expireMinutes)
                     ? expireMinutes : throw new InvalidOperationException("JWT expiration time not configured (Jwt:ExpireAccess)."))
            });
            return new UserLoginResponse(new JwtSecurityTokenHandler().WriteToken(token), user.Id, token.ValidTo);

        }
    }
}
