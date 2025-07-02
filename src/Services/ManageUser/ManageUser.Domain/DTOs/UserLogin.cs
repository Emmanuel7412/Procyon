using System;

namespace Domain.Users.DTOs;

public record struct UserLogin
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}
