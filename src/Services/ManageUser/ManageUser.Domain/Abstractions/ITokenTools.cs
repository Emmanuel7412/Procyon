using System.Security.Claims;

namespace ManageUser.Domain.Abstractions;

public interface ITokenTools
{
    //Task<JwtSecurityToken> GenerateTokenAsync(UserTokenGenerate userTokenGenerate);
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken);
}
