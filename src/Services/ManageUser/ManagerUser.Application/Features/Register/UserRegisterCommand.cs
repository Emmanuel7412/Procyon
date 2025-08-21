using ManageUser.Domain.DTOs;
using Procyon.Core.Abstractions;

namespace ManageUser.Application.Features.Register;

public sealed record UserRegisterCommand(UserRegister UserRegister)
    : ICommand<UserRegisterResponse>;
public sealed record UserRegisterResponse(string UserId);
