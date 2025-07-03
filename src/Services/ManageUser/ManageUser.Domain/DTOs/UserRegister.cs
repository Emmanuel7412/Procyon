using System;

namespace ManageUser.Domain.DTOs;

public readonly record struct UserRegister
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string ConfirmPassword { get; init; }

}
