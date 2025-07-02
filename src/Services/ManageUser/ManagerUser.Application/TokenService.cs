using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ManageUser.Application;

public class TokenService(IConfiguration configuration) : ITokenService
{
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
}
