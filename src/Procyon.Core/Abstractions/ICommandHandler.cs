namespace Core.Abstractions
{
    public interface ICommandHandler<in TCommand>
    {
        Task Handle(TCommand command, CancellationToken cancellation);
    }

    public interface ICommandHandler<in TCommand, TResponse>
    {
        Task<TResponse> Handle(TCommand command, CancellationToken cancellation);
    }
}
