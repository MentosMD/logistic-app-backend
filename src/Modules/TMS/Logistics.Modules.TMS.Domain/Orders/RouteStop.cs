using Logistics.Core.Domain;
using Logistics.Modules.TMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Modules.TMS.Domain.Orders
{
    public sealed class RouteStop : Entity
    {
        public int SequenceOrder { get; private set; }
        public string Address { get; private set; } = string.Empty;
        public Coordinates Location { get; private set; }
        public DeliveryWindow Window { get; private set; }
        public StopType Type { get; private set; }
        public DateTime? ArrivedAtUtc { get; private set; }

        public bool IsCompleted => ArrivedAtUtc.HasValue;

        private RouteStop() { }

        internal static RouteStop Create(
            int sequenceOrder,
            string address,
            Coordinates location,
            DeliveryWindow window,
            StopType type)
        {
            if (sequenceOrder <= 0)
                throw new Exception("Порядковый номер точки маршрута должен быть положительным.");

            if (string.IsNullOrWhiteSpace(address))
                throw new Exception("Адрес точки маршрута не может быть пустым.");

            return new RouteStop
            {
                Id = Guid.NewGuid(),
                SequenceOrder = sequenceOrder,
                Address = address.Trim(),
                Location = location,
                Window = window,
                Type = type
            };
        }

        internal void MarkArrived(DateTime arrivedAtUtc)
        {
            if (IsCompleted)
                throw new Exception($"Прибытие на точку '{Address}' уже зафиксировано.");

            ArrivedAtUtc = arrivedAtUtc;
        }
    }
}
