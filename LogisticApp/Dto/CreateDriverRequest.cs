using LogisticApp.Enums;

namespace LogisticApp.Dto
{
    public class CreateDriverRequest
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public StatusDriver Status { get; set; }
        public Guid? VehicleId { get; set; }
    }
}
