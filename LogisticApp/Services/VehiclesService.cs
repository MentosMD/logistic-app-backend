using LogisticApp.Dto;
using LogisticApp.Entities;

namespace LogisticApp.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly LogisticsDbContext _dbContext;

        public VehiclesService(LogisticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VehicleDTO> CreateVehicleAsync(CreateVehicleRequest request)
        {
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                PlateNumber = request.PlateNumber,
                Model = request.Model,
                MaxWeight = request.MaxWeight,
                VolumeCapacity = request.VolumeCapacity
            };

            await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();

            return new VehicleDTO
            {
                Id = vehicle.Id,
                PlateNumber = vehicle.PlateNumber,
                Model = vehicle.Model,
                MaxWeight = vehicle.MaxWeight,
                VolumeCapacity = vehicle.VolumeCapacity,
                DriverId = vehicle.DriverId
            };
        }   

        public async Task<List<VehicleDTO>> GetAllVehiclesAsync()
        {
            return _dbContext.Vehicles.Select(v => new VehicleDTO
            {
                Id = v.Id,
                PlateNumber = v.PlateNumber,
                Model = v.Model,
                MaxWeight = v.MaxWeight,
                VolumeCapacity = v.VolumeCapacity,
                DriverId = v.DriverId
            }).ToList();
        }

        public async Task Update(VehicleDTO vehicle)
        {
            var existingVehicle = await _dbContext.Vehicles.FindAsync(vehicle.Id);

            if (existingVehicle == null)
                throw new InvalidOperationException("Vehicle not found");

            existingVehicle.PlateNumber = vehicle.PlateNumber;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.MaxWeight = vehicle.MaxWeight;
            existingVehicle.VolumeCapacity = vehicle.VolumeCapacity;
            existingVehicle.DriverId = vehicle.DriverId;

            _dbContext.Vehicles.Update(existingVehicle);
            await _dbContext.SaveChangesAsync();
        }
    }
}
