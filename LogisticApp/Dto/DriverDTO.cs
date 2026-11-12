using LogisticApp.Enums;

namespace LogisticApp.Dto
{
    public class DriverDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public StatusDriver Status { get; set; }
    }
}
