using Logistics.Modules.TMS.Domain.Orders;
using Logistics.Modules.TMS.Domain.ValueObjects;

namespace Logistics.Modules.TMS.Application.Abstractions
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);

        Task<Order?> GetByOrderNumberAsync(OrderNumber orderNumber);

        Task<IReadOnlyList<Order>> GetActiveOrdersAsync();

        Task<bool> ExistsByOrderNumberAsync(OrderNumber orderNumber);

        Task AddAsync(Order order);

        void Remove(Order order);
    }
}
