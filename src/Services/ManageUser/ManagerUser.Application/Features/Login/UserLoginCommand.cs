using Domain.Users.DTOs;
using Procyon.Core.Abstractions;

namespace ManageUser.Application.Features.Login;

public sealed record UserLoginCommand(UserLogin UserLogin)
    : ICommand<UserLoginResponse>;

public record UserLoginResponse(string? AccessToken, string? RefreshToken, string UserId);
