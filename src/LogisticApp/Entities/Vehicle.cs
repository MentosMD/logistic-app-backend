namespace LogisticApp.Entities
{
    public class Vehicle
    {
        public Guid Id {  get; set; }
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public double MaxWeight { get; set; }
        public double VolumeCapacity { get; set; }
        public Guid? DriverId { get; set; }
        public virtual Driver? Driver { get; set; }
    }
}
