using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Modules.TMS.Infrastructure.Persistence
{
    internal sealed class TmsDbContextFactory : IDesignTimeDbContextFactory<TmsDbContext>
    {
        public TmsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TmsDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=LogisticsDesignTime;Trusted_Connection=True;TrustServerCertificate=True;",
                sql => sql.MigrationsHistoryTable("__EFMigrationsHistory", "tms"));

            return new TmsDbContext(optionsBuilder.Options);
        }
    }
}
