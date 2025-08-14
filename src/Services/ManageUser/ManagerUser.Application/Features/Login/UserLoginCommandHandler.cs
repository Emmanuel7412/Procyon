using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Abstractions;
using Core.Shared;
using ManageUser.Domain.Abstractions;
using ManageUser.Domain.Entities;
using ManageUser.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ManageUser.Application.Features.Login
{
    public class UserLoginCommandHandler(UserManager<ApplicationUser> userManager,
                ITokenRepository userRepository,
                  ITokenTools tokenTools) : ICommandHandler<UserLoginCommand, UserLoginResponse>
    {

        public async Task<Result<UserLoginResponse>> Handle(UserLoginCommand command, CancellationToken cancellation)
        {
            ApplicationUser? user = await userManager.FindByEmailAsync(command.UserLogin.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, command.UserLogin.Password))
            {
                return Result.Failure<UserLoginResponse>(Error.InvalidCredentials); // Invalid credentials
            }

            // creating the necessary claims
            List<Claim> authClaims = [
                    new (ClaimTypes.Name, user.UserName),
                    new (ClaimTypes.Email, user.Email),
                new ("firstname", user.FirstName),
                new ("lastname", user.LastName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
                // unique id for token
        ];

            var userRoles = await userManager.GetRolesAsync(user);

            // adding roles to the claims. So that we can get the user role from the token.
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // generating access token
            var token = tokenTools.GenerateAccessToken(authClaims);

            string refreshToken = tokenTools.GenerateRefreshToken();

            var tokenInfo = new TokenInfo
            {
                Username = user.UserName,
                RefreshToken = refreshToken,
                ExpiredAt = DateTime.UtcNow.AddDays(7)
            };
            await userRepository.SaveTokenInfo(tokenInfo, cancellation);

            return Result.Success(new UserLoginResponse(token, refreshToken, user.Id));
        }
    }
}
