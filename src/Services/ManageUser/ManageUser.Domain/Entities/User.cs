// Licensed to the .NET Foundation under one or more agreements.


using Procyon.Core.Shared.Models;

namespace ManageUser.Domain.Entities;

public class User : Entity<UserId>
{
    public required string Email { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}
