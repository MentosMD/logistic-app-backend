using FluentValidation;
using Logistics.Modules.TMS.Contracts.Orders;

namespace Logistics.Modules.TMS.Application.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.OrderNumber)
                .NotEmpty().WithMessage("Номер заказа обязателен.")
                .MaximumLength(32);

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Идентификатор заказчика обязателен.");

            RuleFor(x => x.CargoItems)
                .NotEmpty().WithMessage("Заказ должен содержать хотя бы одну позицию груза.");

            RuleFor(x => x.RouteStops)
                .Must(s => s is { Count: >= 2 })
                .WithMessage("Маршрут должен содержать минимум погрузку и выгрузку.")
                .Must(s => s is null || s.Select(x => x.SequenceOrder).Distinct().Count() == s.Count)
                .WithMessage("Порядковые номера точек маршрута не должны дублироваться.");

            RuleForEach(x => x.CargoItems).ChildRules(item =>
            {
                item.RuleFor(i => i.Name).NotEmpty().MaximumLength(200);
                item.RuleFor(i => i.WeightKg).GreaterThan(0);
                item.RuleFor(i => i.Quantity).GreaterThan(0);
            });

            RuleForEach(x => x.RouteStops).ChildRules(stop =>
            {
                stop.RuleFor(s => s.SequenceOrder).GreaterThan(0);
                stop.RuleFor(s => s.Address).NotEmpty().MaximumLength(500);
                stop.RuleFor(s => s.Latitude).InclusiveBetween(-90, 90);
                stop.RuleFor(s => s.Longitude).InclusiveBetween(-180, 180);
                stop.RuleFor(s => s.WindowEndUtc)
                    .GreaterThan(s => s.WindowStartUtc)
                    .WithMessage("Окончание окна доставки должно быть позже начала.");
                stop.RuleFor(s => s.Type).IsInEnum();
            });
        }
    }
}
