using Core.Abstractions;
using ManageUser.Application.Features.Token;
using ManageUser.Domain.DTOs;
using Procyon.Core.Shared.API;

namespace ManageUser.API.Endpoints;

public sealed class Token : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGroup("token").MapPost("/refesh", async (HttpContext context, TokenModel tokenModel, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
       {
           var refreshTokenCommand = new RefreshTokenCommand(tokenModel);
           var tokenResponse = await commandDispatcher.Dispatch<RefreshTokenCommand, RefreshTokenResponse>(refreshTokenCommand, cancellationToken);
           return Results.Ok(tokenResponse);
       });
    }
}
