using ManageUser.Application.Features.Token;
using ManageUser.Domain.DTOs;
using Procyon.Core.Abstractions;
using Procyon.Core.Extensions;
using Procyon.Core.Shared.API;

namespace ManageUser.API.Endpoints;

public sealed class Token : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGroup("api/token").MapPost("/refesh", async (HttpContext context, TokenModel tokenModel, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
       {
           var refreshTokenCommand = new RefreshTokenCommand(tokenModel);
           var result = await commandDispatcher.Dispatch<RefreshTokenCommand, RefreshTokenResponse>(refreshTokenCommand, cancellationToken);
           return result.Match(
                onSuccess: response => Results.Ok(response),
                onFailure: result => Results.Problem(title: result.Error.Code, detail: result.Error.Description, statusCode: result.Error.StatusCode));
       });
    }
}
