namespace ManageUser.Domain.Entities;

public record UserId
{
    public Guid Value { get; }
    private UserId(Guid value) => Value = value;
    public static UserId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("OrderId cannot be empty.");
        }

        return new UserId(value);
    }
}
