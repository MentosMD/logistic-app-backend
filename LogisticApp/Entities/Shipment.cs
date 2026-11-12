using LogisticApp.Enums;

namespace LogisticApp.Entities
{
    public class Shipment
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;

        public Guid DriverId { get; set; }
        public Driver Driver { get; set; } = null!;

        public StatusShipment Status { get; set; } = StatusShipment.Created;
        public DateTime PlannedETA { get; set; }
        public DateTime? PredictedETA { get; set; } // Заполняется ИИ агентом
        public PriorityStatus Priority { get; set; }
    }
}
