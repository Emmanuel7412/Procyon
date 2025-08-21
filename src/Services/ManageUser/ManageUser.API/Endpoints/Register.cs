using Core.Shared;
using ManageUser.Application.Features.Register;
using ManageUser.Domain.DTOs;
using Procyon.Core.Abstractions;
using Procyon.Core.Extensions;
using Procyon.Core.Shared.API;

namespace ManageUser.API.Endpoints;

internal sealed class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Define your authentication endpoints here
        app.MapGroup("api/user").MapPost("/register", async (UserRegister userRegister, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
        {
            // Handle registration logic
            UserRegisterCommand userRegisterCommand = new(userRegister);
            Result<UserRegisterResponse> result = await commandDispatcher.Dispatch<UserRegisterCommand, UserRegisterResponse>(userRegisterCommand, cancellationToken);
            return result.Match(
                onSuccess: response => Results.Ok(response),
                onFailure: result => Results.Problem(title: result.Error.Code, detail: result.Error.Description, statusCode: result.Error.StatusCode));
        });
    }

}
