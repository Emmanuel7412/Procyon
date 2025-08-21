using Core.Shared;

namespace Procyon.Core.Abstractions
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task<Result> Handle(TCommand command, CancellationToken cancellation);
    }

    public interface ICommandHandler<in TCommand, TResponse> where TCommand : ICommand<TResponse>
    {
        Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellation);
    }
}
