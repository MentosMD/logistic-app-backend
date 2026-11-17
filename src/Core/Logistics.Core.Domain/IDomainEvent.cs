namespace Logistics.Core.Domain
{
    public interface IDomainEvent
    {
        DateTime OccurredOnUtc => DateTime.UtcNow;
    }
}
