

namespace Logistics.Modules.TMS.Domain.ValueObjects
{
    public readonly record struct Weight
    {
        public decimal Kilograms { get; }

        private Weight(decimal kilograms) => Kilograms = kilograms;

        public static Weight Zero => new(0m);

        public static Weight FromKilograms(decimal kilograms)
        {
            if (kilograms < 0)
                throw new Exception("Вес груза не может быть отрицательным.");

            return new Weight(decimal.Round(kilograms, 3));
        }

        public static Weight operator +(Weight left, Weight right) =>
            FromKilograms(left.Kilograms + right.Kilograms);

        public static Weight operator *(Weight weight, int quantity) =>
            FromKilograms(weight.Kilograms * quantity);
    }
}
