using System;
using Procyon.Core.Exceptions;

namespace ManageUser.Application.Exceptions;

public class ExistingEmailException : BadRequestException
{
    public ExistingEmailException(string email)
        : base($"User with email '{email}' already exists.")
    {
    }

}
