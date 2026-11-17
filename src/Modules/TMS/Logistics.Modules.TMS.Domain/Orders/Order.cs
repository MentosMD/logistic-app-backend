using Logistics.Core.Domain;
using Logistics.Modules.TMS.Domain.ValueObjects;

namespace Logistics.Modules.TMS.Domain.Orders
{
    public sealed class Order : Entity
    {
        private readonly List<CargoItem> _cargoItems = [];
        private readonly List<RouteStop> _routeStops = [];

        public OrderNumber OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public DeliveryWindow SlaWindow { get; set; }
        public Guid CustomerId { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }

        public int? AssignedDriverId { get; private set; }
        public int? AssignedTruckId { get; private set; }
        public int? ReservedDockId { get; private set; }
        public string? CancellationReason { get; private set; }

        public IReadOnlyCollection<CargoItem> CargoItems => _cargoItems.AsReadOnly();
        public IReadOnlyCollection<RouteStop> RouteStops => _routeStops.AsReadOnly();

        private Order() { }

        public static Order Create(OrderNumber orderNumber, Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new Exception("Идентификатор заказчика обязателен.");

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = orderNumber,
                CustomerId = customerId,
                Status = OrderStatus.Draft,
                CreatedAtUtc = DateTime.UtcNow
            };

            return order;
        }

        public void AddCargoItem(
            string name,
            Weight unitWeight,
            int quantity,
            CargoRequirements requirements)
        {
            EnsureDraftStatus();

            var item = CargoItem.Create(name, unitWeight, quantity, requirements);
            _cargoItems.Add(item);
        }

        public void AddRouteStop(
            int sequenceOrder,
            string address,
            Coordinates location,
            DeliveryWindow window,
            StopType type)
        {
            EnsureDraftStatus();

            if (_routeStops.Any(s => s.SequenceOrder == sequenceOrder))
                throw new Exception($"Точка маршрута с порядковым номером {sequenceOrder} уже добавлена.");

            var stop = RouteStop.Create(sequenceOrder, address, location, window, type);
            _routeStops.Add(stop);
        }

        public void Cancel()
        {
            if (Status is OrderStatus.InTransit or OrderStatus.Completed)
                throw new Exception($"Невозможно отменить заказ в статусе '{Status}'.");

            if (Status == OrderStatus.Cancelled)
                return;

            Status = OrderStatus.Cancelled;
        }

        private void EnsureDraftStatus()
        {
            if (Status != OrderStatus.Draft)
                throw new Exception($"Изменение состава заказа запрещено в статусе '{Status}'.");
        }
    }
}
