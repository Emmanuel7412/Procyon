using System;
using System.IdentityModel.Tokens.Jwt;
using Core.Abstractions;
using Domain.Users.DTOs;
using ManageUser.Application;
using ManageUser.Application.Features.Login;
using ManageUser.Application.Features.Register;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ManageUser.API.Endpoints;

public record UserLoginRequest(UserLogin UserLogin);
public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
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
