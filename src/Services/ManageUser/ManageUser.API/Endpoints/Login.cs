using Core.Shared;
using Domain.Users.DTOs;
using ManageUser.Application.Features.Login;
using Procyon.Core.Abstractions;
using Procyon.Core.Extensions;
using Procyon.Core.Shared;
using Procyon.Core.Shared.API;

namespace ManageUser.API.Endpoints;


public sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Define your authentication endpoints here
        var group = app.MapGroup("api/user");
        group.MapPost("/login", async (HttpContext context, UserLogin userLogin, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
        {
            // Handle login logic
            var userLoginCommand = new UserLoginCommand(userLogin);
            Result<UserLoginResponse> result = await commandDispatcher.Dispatch<UserLoginCommand, UserLoginResponse>(userLoginCommand, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        });
    }

}
