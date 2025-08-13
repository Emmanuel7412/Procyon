using Core.Abstractions;
using ManageUser.Domain.DTOs;

namespace ManageUser.Application.Features.Token;

public sealed record RefreshTokenCommand(TokenModel TokenModel)
    : ICommand<RefreshTokenResponse>;

public record RefreshTokenResponse(string? AccessToken, string? RefreshToken);
