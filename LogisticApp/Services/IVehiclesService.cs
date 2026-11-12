using LogisticApp.Dto;

namespace LogisticApp.Services
{
    public interface IVehiclesService
    {
        public Task<VehicleDTO> CreateVehicleAsync(CreateVehicleRequest request);
        public Task<List<VehicleDTO>> GetAllVehiclesAsync();
        public Task Update(VehicleDTO vehicle);
    }
}
