using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Modules.TMS.Contracts.Orders
{
    public sealed record CreateRouteStopRequest(
        int SequenceOrder,
        string Address,
        double Latitude,
        double Longitude,
        DateTime WindowStartUtc,
        DateTime WindowEndUtc,
        RouteStopType Type);
}
