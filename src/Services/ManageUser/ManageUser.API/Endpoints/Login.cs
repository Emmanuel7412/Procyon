using Core.Abstractions;
using Domain.Users.DTOs;
using ManageUser.Application.Features.Login;

namespace ManageUser.API.Endpoints;


public sealed class Login : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Define your authentication endpoints here
        app.MapPost("/login", async (HttpContext context, UserLogin userLogin, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
        {
            // Handle login logic
            var userLoginCommand = new UserLoginCommand(userLogin);
            var tokenResponse = await commandDispatcher.Dispatch<UserLoginCommand, UserLoginResponse>(userLoginCommand, cancellationToken);

            if (tokenResponse?.AccessToken == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid login credentials");
                return Results.Unauthorized();
            }
            return Results.Ok(tokenResponse);

        });
    }

}
