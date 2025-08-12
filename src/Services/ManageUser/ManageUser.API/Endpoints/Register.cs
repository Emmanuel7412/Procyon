using Core.Abstractions;
using Domain.Users.DTOs;
using ManageUser.Application.Features.Register;
using ManageUser.Domain.DTOs;
using Procyon.Core.Shared.API;

namespace ManageUser.API.Endpoints;

public record UserLoginRequest(UserLogin UserLogin);
internal sealed class Register : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        // Define your authentication endpoints here
        app.MapPost("/register", async (HttpContext context, UserRegister userRegister, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
        {
            // Handle registration logic
            UserRegisterCommand userRegisterCommand = new(userRegister);
            UserRegisterResponse userRegisterResponse = await commandDispatcher.Dispatch<UserRegisterCommand, UserRegisterResponse>(userRegisterCommand, cancellationToken);
            if (userRegisterResponse.UserId == null)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Results.InternalServerError("Registration failed");
            }
            return Results.Ok(userRegisterResponse);

        });
    }

}
