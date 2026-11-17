using FluentValidation;
using Microsoft.Extensions.DependencyInjection;


namespace Logistics.Modules.TMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTmsApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);


            return services;
        }
    }
}
