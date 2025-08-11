using Core.Abstractions;
using ManageUser.Domain.DTOs;
using ManageUser.Domain.Entities;

namespace ManageUser.Application.Features.Register;

public sealed record UserRegisterCommand(UserRegister UserRegister)
    : ICommand<UserRegisterResponse>;
public sealed record UserRegisterResponse(UserId UserId);
