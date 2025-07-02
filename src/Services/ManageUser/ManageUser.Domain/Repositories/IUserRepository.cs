using ManageUser.Domain.Entities;

namespace ManageUser.Domain.Repositories;

public interface IUserRepository
{

    Task<User?> FindByEmailAsync(string email);

}
