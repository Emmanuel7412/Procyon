// Licensed to the .NET Foundation under one or more agreements.

using Core.Abstractions;

namespace ManageUser.API.Features.Users.GetUserByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
