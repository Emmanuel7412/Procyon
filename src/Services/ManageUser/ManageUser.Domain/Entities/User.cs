// Licensed to the .NET Foundation under one or more agreements.


using Procyon.Core.Shared.Models;

namespace ManageUser.Domain.Entities;

public class User : Entity<UserId>
{
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public string LastName { get; set; } = default!;
    public required string PasswordHash { get; set; }
}
