namespace Core.Shared
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}
