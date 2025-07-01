// Licensed to the .NET Foundation under one or more agreements.

namespace ManageUser.API.Features.Users.GetUserByEmail;

public sealed record UserResponse
{
    public Guid Id { get; init; }

    public string Email { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }
}
