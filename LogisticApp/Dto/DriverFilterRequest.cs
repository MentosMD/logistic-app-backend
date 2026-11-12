using LogisticApp.Enums;

namespace LogisticApp.Dto
{
    public class DriverFilterRequest
    {
        public string FullName { get; set; }
        public string LicenseNumber { get; set; }
        public StatusDriver? Status { get; set; }
    }
}
