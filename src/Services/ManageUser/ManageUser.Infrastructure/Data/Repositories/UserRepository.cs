using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using ManageUser.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ManageUser.Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{

    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
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
