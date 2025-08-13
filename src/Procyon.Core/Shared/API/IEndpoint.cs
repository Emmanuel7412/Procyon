using Microsoft.AspNetCore.Routing;

namespace Procyon.Core.Shared.API
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
