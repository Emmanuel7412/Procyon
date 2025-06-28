namespace Core.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> 
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellation);
}
