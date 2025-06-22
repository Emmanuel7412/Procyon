
using SharedKernel;

namespace Core.Abstractions
{
    public interface ICommandDispatcher
    {
        Task<Result<TResponse>> Dispatch<TCommand, TResponse>(TCommand command, CancellationToken cancellation);
    }
}
