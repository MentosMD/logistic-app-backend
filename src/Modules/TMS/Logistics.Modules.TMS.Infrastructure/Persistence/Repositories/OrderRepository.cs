using Logistics.Modules.TMS.Application.Abstractions;
using Logistics.Modules.TMS.Domain.Orders;
using Logistics.Modules.TMS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Modules.TMS.Infrastructure.Persistence.Repositories
{
    public sealed class OrderRepository(TmsDbContext dbContext) : IOrderRepository
    {
        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await dbContext.Orders
                .Include(o => o.CargoItems)
                .Include(o => o.RouteStops)
                .AsSplitQuery()
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> GetByOrderNumberAsync(OrderNumber orderNumber)
        {
            return await dbContext.Orders
                .Include(o => o.CargoItems)
                .Include(o => o.RouteStops)
                .AsSplitQuery()
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<IReadOnlyList<Order>> GetActiveOrdersAsync()
        {
            return await dbContext.Orders
                .AsNoTracking()
                .Include(o => o.CargoItems)
                .Include(o => o.RouteStops)
                .AsSplitQuery()
                .Where(o => o.Status != OrderStatus.Completed && o.Status != OrderStatus.Cancelled)
                .OrderByDescending(o => o.CreatedAtUtc)
                .ToListAsync();
        }

        public async Task<bool> ExistsByOrderNumberAsync(OrderNumber orderNumber)
        {
            return await dbContext.Orders
                .AnyAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task AddAsync(Order order)
        {
            await dbContext.Orders.AddAsync(order);
        }

        public void Remove(Order order)
        {
            dbContext.Orders.Remove(order);
        }
    }
}
