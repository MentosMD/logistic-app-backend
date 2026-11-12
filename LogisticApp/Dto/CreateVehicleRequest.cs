namespace LogisticApp.Dto
{
    public class CreateVehicleRequest
    {
        public string PlateNumber { get; set; }
        public string Model { get; set; }
        public double MaxWeight { get; set; }
        public double VolumeCapacity { get; set; }
        public Guid? DriverId { get; set; }
    }
}
