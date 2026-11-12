using LogisticApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LogisticApp
{
    public class LogisticsDbContext : DbContext
    {
        public LogisticsDbContext() { }
        public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options) { }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        //public DbSet<Warehouse> Warehouses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Vehicle)
                .WithOne(v => v.Driver)
                .HasForeignKey<Driver>(d => d.VehicleId);
        }
    }
}
