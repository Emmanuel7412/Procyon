using Microsoft.AspNetCore.Http;

namespace Core.Shared
{
    public sealed record Error(string Code, string Description, int? StatusCode)

    {
        public static readonly Error None = new(string.Empty, string.Empty, null);
        public static readonly Error NullValue = new("Error.NullValue", "Null value was provided", 400);
        public static readonly Error InvalidCredentials = new("Error.InvalidCredentials", "Invalid credentials provided", 401);
        public static readonly Error NotFound = new("Error.NotFound", "Not found", 404);

        public static implicit operator Result(Error error) => Result.Failure(error);

        public Result ToResult() => Result.Failure(this);
    }
}
