using Core.Shared;

namespace Procyon.Core.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellation);
}
