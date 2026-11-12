using LogisticApp.Dto;
using LogisticApp.Entities;
using LogisticApp.Enums;

namespace LogisticApp.Services
{
    public interface IDriverService
    {
        public Task<List<Driver>> GetAll();
        public Task<Driver> GetDriverById(Guid id);
        public Task<Driver> CreateDriverAsync(CreateDriverRequest request);
        public Task<IEnumerable<DriverDTO>> FilterDriversAsync(string? fullName, string? licenseNumber, StatusDriver? status);
    }
}
