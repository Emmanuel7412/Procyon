namespace Procyon.Core.Shared.Models;

public interface IEntity<T>
{
    public T Id { get; set; }
}

