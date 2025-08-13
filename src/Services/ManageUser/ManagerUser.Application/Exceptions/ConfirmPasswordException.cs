using Procyon.Core.Exceptions;

namespace ManageUser.Application.Exceptions;

public class ConfirmPasswordException : BadRequestException
{
    public ConfirmPasswordException()
        : base($"The password confirmation does not match the original password.")
    {
    }

}
