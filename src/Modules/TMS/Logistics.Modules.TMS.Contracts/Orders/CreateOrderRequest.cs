using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Modules.TMS.Contracts.Orders
{
    public sealed record CreateOrderRequest(
            string OrderNumber,
            Guid CustomerId,
            IReadOnlyList<CreateCargoItemRequest> CargoItems,
            IReadOnlyList<CreateRouteStopRequest> RouteStops
    )
}
