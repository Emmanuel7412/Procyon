using Procyon.Core.Exceptions;

namespace ManageUser.Application.Exceptions;

public class TokenException : BadRequestException
{
    public TokenException(string message)
        : base(message)
    {
    }

}
