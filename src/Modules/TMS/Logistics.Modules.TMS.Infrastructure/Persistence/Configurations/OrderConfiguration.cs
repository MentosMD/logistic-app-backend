using Logistics.Modules.TMS.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Logistics.Modules.TMS.Infrastructure.Persistence.Configurations
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", "tms");

            builder.HasKey(o => o.Id);

            builder.Property<byte[]>("RowVersion")
                .IsRowVersion();

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(o => o.AssignedDriverId);
            builder.Property(o => o.AssignedTruckId);
            builder.Property(o => o.ReservedDockId);

            builder.Property(o => o.CancellationReason)
                .HasMaxLength(500);

            builder.HasIndex(o => o.Status);
            builder.HasIndex(o => o.AssignedDriverId);
            builder.HasIndex(o => o.AssignedTruckId);

            builder.ComplexProperty(o => o.OrderNumber, nb =>
            {
                nb.Property(n => n.Value)
                  .HasColumnName("OrderNumber")
                  .HasMaxLength(32)
                  .IsRequired();
            });

            builder.ComplexProperty(o => o.SlaWindow, wb =>
            {
                wb.Property(w => w.FromUtc)
                  .HasColumnName("SlaFromUtc")
                  .IsRequired();

                wb.Property(w => w.ToUtc)
                  .HasColumnName("SlaToUtc")
                  .IsRequired();
            });
        }
    }
}
