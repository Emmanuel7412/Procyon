using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManageUser.Infrastructure.Data.Repositories;

public class UserRepository : ITokenRepository
{

    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TokenInfo?> GetTokenInfo(string username, CancellationToken cancellationToken = default)
    {
        return await _context.TokenInfos.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
    }

    public async Task SaveTokenInfo(TokenInfo tokenInfo, CancellationToken cancellationToken = default)
    {
        //save refreshToken with exp date in the database
        var existingTokenInfo = _context.TokenInfos.
                    FirstOrDefault(a => a.Username == tokenInfo.Username);

        // If tokenInfo is null for the user, create a new one
        if (existingTokenInfo == null)
        {
            _context.TokenInfos.Add(tokenInfo);
        }
        // Else, update the refresh token and expiration
        else
        {
            existingTokenInfo.RefreshToken = tokenInfo.RefreshToken;
            tokenInfo.ExpiredAt = tokenInfo.ExpiredAt;
        }

        await _context.SaveChangesAsync();
    }
}


