// Licensed to the .NET Foundation under one or more agreements.

using Core.Shared;

namespace Domain.Users;

public sealed class User : Entity
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
}
