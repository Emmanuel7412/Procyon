using System.IdentityModel.Tokens.Jwt;
using Domain.Users.DTOs;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ManageUser.Application;

public class UserManager(IUserRepository userRepository, ITokenService tokenService, IConfiguration configuration)
{
    // This class can be used to manage user-related operations, such as creating, updating, or deleting users.
    // It can also include methods for user authentication and authorization.
    // For now, it is empty and can be expanded as needed.
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    public async Task<JwtSecurityToken?> LoginUserAsync(UserLogin login)
    {
        User? user = await _userRepository.FindByEmailAsync(login.Email);
        if (user == null || string.IsNullOrEmpty(login.Password) || !VerifyPassword(user, login.Password))
        {
            return null; // User not found or password is incorrect
        }
        return await _tokenService.GenerateTokenAsync(new UserTokenGenerate
        {
            User = user,
            ExpireDate = DateTime.Now.AddMinutes(
                double.TryParse(configuration["Jwt:ExpireAccess"], out var expireMinutes)
                 ? expireMinutes : throw new InvalidOperationException("JWT expiration time not configured (Jwt:ExpireAccess)."))
        });
    }

    private bool VerifyPassword(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
        return result == PasswordVerificationResult.Success;
    }
}
