using Microsoft.EntityFrameworkCore;
using Logistics.Modules.TMS.Application.Abstractions;
using Logistics.Modules.TMS.Domain.Orders;

namespace Logistics.Modules.TMS.Infrastructure.Persistence
{
    public sealed class TmsDbContext(DbContextOptions<TmsDbContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders => Set<Order>();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>()
                .HaveMaxLength(256);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("tms");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TmsDbContext).Assembly);
        }
    }
}
