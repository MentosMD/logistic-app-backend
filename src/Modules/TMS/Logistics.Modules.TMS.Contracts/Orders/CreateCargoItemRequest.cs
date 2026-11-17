using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Modules.TMS.Contracts.Orders
{
    public sealed record CreateCargoItemRequest(
        string Name,
        decimal WeightKg,
        int Quantity,
        bool RequiresRefrigeration,
        bool IsHazardousAdr
    );
}
