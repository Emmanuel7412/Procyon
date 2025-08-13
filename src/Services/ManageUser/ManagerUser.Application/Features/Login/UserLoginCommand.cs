using Core.Abstractions;
using Domain.Users.DTOs;

namespace ManageUser.Application.Features.Login;

public sealed record UserLoginCommand(UserLogin UserLogin)
    : ICommand<UserLoginResponse>;

public record UserLoginResponse(string? AccessToken, string? RefreshToken, string UserId);
