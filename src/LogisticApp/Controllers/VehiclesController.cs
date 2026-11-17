using LogisticApp.Dto;
using LogisticApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogisticApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesService _vehiclesService;

        public VehiclesController(IVehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<VehicleDTO>> Create([FromBody] CreateVehicleRequest request)
        {
            var vehicle = await _vehiclesService.CreateVehicleAsync(request);
            return Ok(vehicle);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<VehicleDTO>>> GetAll()
        {
            var vehicles = await _vehiclesService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<VehicleDTO>> Update([FromBody] VehicleDTO request)
        {
            await _vehiclesService.Update(request);
            return Ok();
        }
    }
}
