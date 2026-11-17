namespace Logistics.Modules.TMS.Domain.ValueObjects
{
    public readonly record struct Coordinates
    {
        public double Latitude { get; }
        public double Longitude { get; }

        private Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public static Coordinates Create(double latitude, double longitude)
        {
            if (latitude is < -90 or > 90)
                throw new Exception($"Некорректная широта: {latitude}. Допустимый диапазон: [-90, 90].");

            if (longitude is < -180 or > 180)
                throw new Exception($"Некорректная долгота: {longitude}. Допустимый диапазон: [-180, 180].");

            return new Coordinates(latitude, longitude);
        }
    }
}
