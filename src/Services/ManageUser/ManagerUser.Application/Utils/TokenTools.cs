using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ManageUser.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ManageUser.Application;

public class TokenTools(IConfiguration configuration) : ITokenTools
{
    //private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
    // public Task<JwtSecurityToken> GenerateTokenAsync(UserTokenGenerate userTokenGenerate)
    // {
    //     // Implement token generation logic here
    //     if (userTokenGenerate == null || userTokenGenerate.User == null)
    //     {
    //         throw new ArgumentNullException(nameof(userTokenGenerate), "UserTokenGenerate cannot be null");
    //     }

    //     Claim[] claims = new[]
    //     {
    //         new Claim("UserId", userTokenGenerate.User.Id.ToString()),
    //         new Claim("Email", userTokenGenerate.User.Email),
    //         new Claim("FirstName", userTokenGenerate.User.FirstName),
    //         new Claim("LastName", userTokenGenerate.User.LastName ?? string.Empty),
    //         new Claim(ClaimTypes.Role, "Admin"),
    //     };
    //     var jwtConfig = configuration.GetSection("Jwt");
    //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!)); // Replace with your secret key
    //     var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    //     return Task.FromResult(new JwtSecurityToken(
    //         issuer: configuration["Jwt:Issuer"],
    //         audience: configuration["Jwt:Audience"],
    //         claims: claims,
    //         expires: userTokenGenerate.ExpireDate,
    //         signingCredentials: creds
    //     ));
    // }

    // public string HashPassword(User user, string password) => _passwordHasher.HashPassword(user, password);

    // public bool VerifyPassword(User user, string password)
    // {
    //     try
    //     {
    //         var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
    //         return result == PasswordVerificationResult.Success;
    //     }
    //     catch (Exception)
    //     {
    //         // Handle exceptions related to password verification
    //         throw new InvalidOperationException("Password verification failed. Please check the password format or hashing algorithm.");
    //     }
    // }
    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        // Create a symmetric security key using the secret key from the configuration.
        var authSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(15),
            SigningCredentials = new SigningCredentials
                          (authSigningKey, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        // Create a 32-byte array to hold cryptographically secure random bytes
        var randomNumber = new byte[32];

        // Use a cryptographically secure random number generator 
        // to fill the byte array with random values
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);

        // Convert the random bytes to a base64 encoded string 
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        // Define the token validation parameters used to validate the token.
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:Audience"],
            ValidIssuer = configuration["JWT:Issuer"],
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey
                       (Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
        };
        var tokenHandler = new JwtSecurityTokenHandler();

        // Validate the token and extract the claims principal and the security token.
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
        // Cast the security token to a JwtSecurityToken for further validation.
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        // Ensure the token is a valid JWT and uses the HmacSha256 signing algorithm.
        // If no throw new SecurityTokenException
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals
        (SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;

    }
}
