using System.IdentityModel.Tokens.Jwt;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;

namespace ManageUser.Domain.Abstractions;

public interface ITokenTools
{
    Task<JwtSecurityToken> GenerateTokenAsync(UserTokenGenerate userTokenGenerate);
    bool VerifyPassword(User user, string password);
    string HashPassword(User user, string password);
}
