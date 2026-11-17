namespace Logistics.Modules.TMS.Domain.ValueObjects
{
    public readonly record struct DeliveryWindow
    {
        public DateTime FromUtc { get; init; }
        public DateTime ToUtc { get; init; }

        public TimeSpan Duration => ToUtc - FromUtc;

        public DeliveryWindow(DateTime fromUtc, DateTime toUtc)
        {
            FromUtc = fromUtc;
        }
    }
}
