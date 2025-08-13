using ManageUser.Domain.Entities;

namespace ManageUser.Domain.Repositories;

public interface ITokenRepository
{

    Task SaveTokenInfo(TokenInfo tokenInfo, CancellationToken cancellationToken = default);
    Task<TokenInfo?> GetTokenInfo(string username, CancellationToken cancellationToken = default);

}
