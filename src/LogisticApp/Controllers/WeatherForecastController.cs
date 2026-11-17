using LogisticApp.Dto;
using LogisticApp.Entities;
using LogisticApp.Enums;
using LogisticApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogisticApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public WeatherForecastController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<DriverDTO>> CreateDriver([FromBody] CreateDriverRequest request)
        {
            var driver = await _driverService.CreateDriverAsync(request);

            var response = new DriverDTO
            {
                Id = driver.Id,
                FullName = driver.FullName,
                Phone = driver.Phone,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                Status = driver.Status
            };

            return response;
        }

        [HttpGet("[action]")]
        public async Task<List<DriverDTO>> GetAll()
        {
            var drivers = await _driverService.GetAll();

            return drivers.Select(driver => new DriverDTO
            {
                Id = driver.Id,
                FullName = driver.FullName,
                Phone = driver.Phone,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                Status = driver.Status
            }).ToList();
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<DriverDTO>> GetDriverById(Guid id)
        {
            var driver = await _driverService.GetDriverById(id);
            if (driver == null)
            {
                return NotFound();
            }
            var response = new DriverDTO
            {
                Id = driver.Id,
                FullName = driver.FullName,
                Phone = driver.Phone,
                LicenseNumber = driver.LicenseNumber,
                LicenseExpiryDate = driver.LicenseExpiryDate,
                Status = driver.Status
            };
            return Ok(response);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<DriverDTO>>> Filter([FromQuery] DriverFilterRequest request) 
        {
            var drivers = await _driverService.FilterDriversAsync(request.FullName, request.LicenseNumber, request.Status);
            return Ok(drivers);
        }
    }
}
