using ManageUser.Domain.Entities;

namespace ManageUser.Domain.DTOs;

public class UserTokenGenerate
{
    public ApplicationUser User { get; set; } = default!;
    public DateTime ExpireDate { get; set; }
    public DateTime? ExpireRefreshToken { get; set; }
}
