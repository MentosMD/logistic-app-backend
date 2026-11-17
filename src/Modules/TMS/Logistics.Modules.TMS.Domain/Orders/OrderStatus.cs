namespace Logistics.Modules.TMS.Domain.Orders
{
    public enum OrderStatus
    {
        Draft = 0,       // Черновик: можно добавлять/удалять груз и точки маршрута
        Confirmed = 1,   // Подтвержден: состав груза зафиксирован, ждет назначения фуры
        Assigned = 2,    // Назначены водитель и грузовик
        InTransit = 3,   // Фура выехала на маршрут
        Delivered = 4,   // Все точки маршрута пройдены, груз сдан
        Completed = 5,   // Заказ завершен 
        Cancelled = 6    // Заказ отменен (до выезда в рейс)
    }
}
