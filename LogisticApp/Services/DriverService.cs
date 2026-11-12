using LogisticApp.Dto;
using LogisticApp.Entities;
using LogisticApp.Enums;
using Microsoft.EntityFrameworkCore;

namespace LogisticApp.Services
{
    public class DriverService : IDriverService
    {
        private readonly LogisticsDbContext _dbContext;

        public DriverService(LogisticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Driver> CreateDriverAsync(CreateDriverRequest request)
        {
            var driver = new Driver();
            if (request.VehicleId.HasValue)
            {
                var vehicle = await _dbContext.Vehicles.FindAsync(request.VehicleId.Value);
                if (vehicle == null)
                {
                    throw new Exception("Vehicle not found");
                }
                driver.VehicleId = request.VehicleId.Value;
            }

            driver.Id = Guid.NewGuid();
            driver.FullName = request.FullName;
            driver.Phone = request.Phone;
            driver.LicenseNumber = request.LicenseNumber;
            driver.LicenseExpiryDate = DateTime.UtcNow.AddYears(5); // Example expiry date
            driver.Status = request.Status;

           await _dbContext.AddAsync(driver);
           await _dbContext.SaveChangesAsync();

            return driver;
        }

        public async Task<IEnumerable<DriverDTO>> FilterDriversAsync(string? fullName, string? licenseNumber, StatusDriver? status)
        {
            var queryable = _dbContext.Drivers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(fullName))
                queryable = queryable.Where(d => d.FullName.Contains(fullName));

            if (!string.IsNullOrWhiteSpace(licenseNumber))
                queryable = queryable.Where(d => d.LicenseNumber.Contains(licenseNumber));

            if (status.HasValue)
                queryable = queryable.Where(d => d.Status == status.Value);

            return queryable.Select(d => new DriverDTO
            {
                Id = d.Id,
                FullName = d.FullName,
                Phone = d.Phone,
                LicenseNumber = d.LicenseNumber,
                LicenseExpiryDate = d.LicenseExpiryDate,
                Status = d.Status
            }).ToList();
        }

        public async Task<List<Driver>> GetAll()
        {
            return await _dbContext.Drivers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Driver> GetDriverById(Guid id)
        {
            var driver = await _dbContext.Drivers.FirstOrDefaultAsync(d => d.Id == id);
            if (driver == null) { 
                throw new Exception("Driver not found");
            }
            return driver;
        }
    }
}
