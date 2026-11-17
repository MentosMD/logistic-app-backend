namespace Logistics.Modules.TMS.Domain.ValueObjects
{
    public readonly record struct OrderNumber
    {
        public string Value { get; init; }

        private OrderNumber(string value) => Value = value;

        public static OrderNumber Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Номер заказа не может быть пустым.");

            var normalized = value.Trim().ToUpperInvariant();
            if (normalized.Length > 32)
                throw new Exception("Номер заказа не может превышать 32 символа.");

            return new OrderNumber(normalized);
        }

        public override string ToString() => Value;
    }
}
