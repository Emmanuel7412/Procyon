using Core.Abstractions;
using ManageUser.Domain.DTOs;

namespace ManageUser.Application.Features.Register;

public sealed record UserRegisterCommand(UserRegister UserRegister)
    : ICommand<UserRegisterResponse>;
public sealed record UserRegisterResponse(string UserId);
