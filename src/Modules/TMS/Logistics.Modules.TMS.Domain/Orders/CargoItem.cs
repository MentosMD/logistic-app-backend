using Logistics.Core.Domain;
using Logistics.Modules.TMS.Domain.ValueObjects;


namespace Logistics.Modules.TMS.Domain.Orders
{
    public sealed class CargoItem : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public int Quantity { get; private set; }
        public Weight UnitWeight { get; private set; }
        public CargoRequirements Requirements { get; private set; }

        public Weight TotalWeight => UnitWeight * Quantity;

        private CargoItem() { }

        internal static CargoItem Create(
        string name,
        Weight unitWeight,
        int quantity,
        CargoRequirements requirements)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Наименование позиции груза не может быть пустым.");

            if (unitWeight.Kilograms <= 0)
                throw new Exception("Вес единицы груза должен быть больше нуля.");

            if (quantity <= 0)
                throw new Exception("Количество мест груза должно быть больше нуля.");

            return new CargoItem
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                UnitWeight = unitWeight,
                Quantity = quantity,
                Requirements = requirements
            };
        }
    }
}
