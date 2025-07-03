using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ManageUser.Application;

public class TokenTools(IConfiguration configuration) : ITokenTools
{
    private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
    public Task<JwtSecurityToken> GenerateTokenAsync(UserTokenGenerate userTokenGenerate)
    {
        // Implement token generation logic here
        if (userTokenGenerate == null || userTokenGenerate.User == null)
        {
            throw new ArgumentNullException(nameof(userTokenGenerate), "UserTokenGenerate cannot be null");
        }

        Claim[] claims = new[]
        {
            new Claim("UserId", userTokenGenerate.User.Id.ToString()),
            new Claim("Email", userTokenGenerate.User.Email),
            new Claim("FirstName", userTokenGenerate.User.FirstName),
            new Claim("LastName", userTokenGenerate.User.LastName ?? string.Empty),
            new Claim(ClaimTypes.Role, "Admin"),
        };
        var jwtConfig = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!)); // Replace with your secret key
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        return Task.FromResult(new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: userTokenGenerate.ExpireDate,
            signingCredentials: creds
        ));
    }

    public string HashPassword(User user, string password) => _passwordHasher.HashPassword(user, password);

    public bool VerifyPassword(User user, string password)
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
