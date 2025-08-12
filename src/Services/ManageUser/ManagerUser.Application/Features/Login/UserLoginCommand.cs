using Core.Abstractions;
using Domain.Users.DTOs;
using ManageUser.Domain.Entities;

namespace ManageUser.Application.Features.Login;

public sealed record UserLoginCommand(UserLogin UserLogin)
    : ICommand<UserLoginResponse>;

public record UserLoginResponse(string? AccessToken, string? RefreshToken, string UserId, DateTime ExpireIn);
