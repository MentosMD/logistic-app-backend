using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LogisticApp
{
    // Простая дизайн-тайм фабрика, возвращающая DbContext с жестко заданной строкой подключения.
    // Это предотвращает выполнение кода Program.cs во время работы EF Tools.
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LogisticsDbContext>
    {
        public LogisticsDbContext CreateDbContext(string[] args)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<LogisticsDbContext>();
                // Явно указываем простую строку подключения для design-time
                var connectionString = "Server=localhost;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);

                return new LogisticsDbContext(optionsBuilder.Options);
            }
            catch (Exception ex)
            {
                // Сохраняем полный стектрейс в файл для диагностики design-time ошибок
                try
                {
                    var path = Path.Combine(Path.GetTempPath(), "ef_design_time_error.txt");
                    File.WriteAllText(path, ex.ToString());
                }
                catch
                {
                    // ignore file write failures
                }

                // Делаем исключение более информативным для диагностики в design-time
                throw new InvalidOperationException($"Failed to create LogisticsDbContext in design-time factory: {ex.Message}. See ef_design_time_error.txt in temp folder for details.", ex);
            }
        }
    }
}
