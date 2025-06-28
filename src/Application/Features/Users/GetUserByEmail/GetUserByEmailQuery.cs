// Licensed to the .NET Foundation under one or more agreements.

using Application.Features.Users.GetUserByEmail;
using Core.Abstractions;

namespace Application.Features.Users.GetUserById;

public sealed record GetUserByEmailQuery(string Email) : IQuery<UserResponse>;
