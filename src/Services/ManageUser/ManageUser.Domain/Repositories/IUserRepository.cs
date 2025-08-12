using ManageUser.Domain.Entities;

namespace ManageUser.Domain.Repositories;

public interface IUserRepository
{

    // Task<User?> FindByEmailAsync(string email);
    // Task<UserId> CreateUserAsync(User user, CancellationToken cancellationToken = default);
    Task SaveTokenInfo(TokenInfo tokenInfo, CancellationToken cancellationToken = default);

}
