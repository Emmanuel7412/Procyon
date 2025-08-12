using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;

namespace ManageUser.Domain.Abstractions;

public interface ITokenTools
{
    //Task<JwtSecurityToken> GenerateTokenAsync(UserTokenGenerate userTokenGenerate);
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
    // bool VerifyPassword(User user, string password);
    // string HashPassword(User user, string password);
}
