namespace ManageUser.Domain.DTOs;

public readonly record struct TokenModel
{
    public required string AccessToken { get; init; }

    public string RefreshToken { get; init; }
}
