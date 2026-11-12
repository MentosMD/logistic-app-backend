using LogisticApp.Enums;

namespace LogisticApp.Entities
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public StatusDriver Status { get; set; }
        public Guid? VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
