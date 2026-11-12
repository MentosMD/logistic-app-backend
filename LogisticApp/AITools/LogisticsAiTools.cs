using LogisticApp.Dto;
using LogisticApp.Services;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace LogisticApp.AITools
{
    [McpServerToolType()]
    public class LogisticsAiTools
    {
        private readonly IDriverService _driverService;

        public LogisticsAiTools(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [McpServerTool(Name = "create_driver")]
        [Description("Creates a new driver in the system.")]
        public async Task<string> CreateDriver(
            [Description("Полное имя водителя, например: Иван Иванов")] string fullName,
            [Description("Номер водительского удостоверения")] string licenseNumber,
            [Description("Контактный телефон")] string phone)
        {
            var request = new CreateDriverRequest
            {
                FullName = fullName,
                LicenseNumber = licenseNumber,
                Phone = phone,
                Status = Enums.StatusDriver.Active
            };

            var driver = await _driverService.CreateDriverAsync(request);

            return $"Успех! Водитель {driver.FullName} зарегистрирован. Его ID: {driver.Id}";
        }
    }
}
