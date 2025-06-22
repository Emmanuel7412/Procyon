using SharedKernel;

namespace Core.Abstractions
{
    public interface IQueryDispatcher
    {
        Task<Result<TResponse>> Dispatch<TQuery, TResponse>(TQuery query, CancellationToken cancellation);
    }
}
