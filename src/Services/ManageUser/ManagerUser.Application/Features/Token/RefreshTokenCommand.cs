using ManageUser.Domain.DTOs;
using Procyon.Core.Abstractions;

namespace ManageUser.Application.Features.Token;

public sealed record RefreshTokenCommand(TokenModel TokenModel)
    : ICommand<RefreshTokenResponse>;

public record RefreshTokenResponse(string? AccessToken, string? RefreshToken);
