using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ManageUser.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{

    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public Task<UserId> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        _context.Users.Add(user);
        return _context.SaveChangesAsync(cancellationToken)
            .ContinueWith(task => user.Id, cancellationToken);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    }
}
