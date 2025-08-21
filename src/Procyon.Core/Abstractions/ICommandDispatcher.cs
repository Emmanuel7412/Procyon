using Core.Shared;

namespace Procyon.Core.Abstractions
{
    public interface ICommandDispatcher
    {
        Task<Result<TCommandResult>> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation) where TCommand : ICommand<TCommandResult>;
    }
}
