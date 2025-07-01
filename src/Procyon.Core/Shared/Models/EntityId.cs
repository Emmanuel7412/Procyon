namespace Procyon.Core.Shared.Models;

public record EntityId
{
    public Guid value { get; }

    private EntityId(Guid value) => this.value = value;

    public static EntityId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("EntityId cannot be empty.", nameof(value));
        }
        return new EntityId(value);
    }

    public override string ToString() => value.ToString();
}
