namespace Domain.Users.DTOs;
// This DTO is used for user login operations, containing the necessary fields for authentication.
public readonly record struct UserLogin
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}
