using Core.Shared;

namespace Procyon.Core.Abstractions
{
    public interface IQueryDispatcher
    {
        Task<Result<TQueryResult>> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation) where TQuery : IQuery<TQueryResult>;
    }
}
