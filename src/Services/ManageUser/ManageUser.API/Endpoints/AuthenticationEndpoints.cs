using System;
using System.IdentityModel.Tokens.Jwt;
using Core.Abstractions;
using Domain.Users.DTOs;
using ManageUser.Application;
using ManageUser.Application.Features.Login;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ManageUser.API.Endpoints;

public record UserLoginRequest(UserLogin UserLogin);
public static class AuthenticationEndpoints
{
    // how to use it in program.cs
    // app.MapAuthenticationEndpoints();
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        // Define your authentication endpoints here
        app.MapPost("/login", async (HttpContext context, UserLogin userLogin, ICommandDispatcher commandDispatcher, CancellationToken cancellationToken) =>
        {
            // Handle login logic
            var userLoginCommand = new UserLoginCommand(userLogin);
            var tokenResponse = await commandDispatcher.Dispatch<UserLoginCommand, UserLoginResponse>(userLoginCommand, cancellationToken);

            if (tokenResponse?.Token == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid login credentials");
                return Results.Unauthorized();
            }
            return Results.Ok(tokenResponse);

        });

        app.MapPost("/register", async (HttpContext context) =>
        {
            // Handle registration logic
            await context.Response.WriteAsync("register endpoint");

        });
    }

}
