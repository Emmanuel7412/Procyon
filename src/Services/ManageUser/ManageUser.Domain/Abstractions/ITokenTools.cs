using System;
using System.IdentityModel.Tokens.Jwt;
using ManageUser.Domain.DTOs;

namespace ManageUser.Domain.Abstractions;

public interface ITokenTools
{
    Task<JwtSecurityToken> GenerateTokenAsync(UserTokenGenerate userTokenGenerate);
}
