using Core.Abstractions;
using ManageUser.Application.Exceptions;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.Repositories;

namespace ManageUser.Application.Features.Token;

public class RefreshTokenHandler(ITokenRepository userRepository, ITokenTools tokenTools) : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand command, CancellationToken cancellation)
    {
        var tokenModel = command.TokenModel;
        var principal = tokenTools.GetPrincipalFromExpiredToken(tokenModel.AccessToken);
        var username = principal?.Identity?.Name ?? throw new ArgumentException("Invalid token");

        var tokenInfo = await userRepository.GetTokenInfo(username);
        if (tokenInfo == null
            || tokenInfo.RefreshToken != tokenModel.RefreshToken
            || tokenInfo.ExpiredAt <= DateTime.UtcNow)
        {
            throw new TokenException("Invalid refresh token. Please login again.");
        }

        var newAccessToken = tokenTools.GenerateAccessToken(principal.Claims);
        var newRefreshToken = tokenTools.GenerateRefreshToken();
        tokenInfo.RefreshToken = newRefreshToken;
        await userRepository.SaveTokenInfo(tokenInfo, cancellation);
        return new RefreshTokenResponse(newAccessToken, newRefreshToken);
    }
}
