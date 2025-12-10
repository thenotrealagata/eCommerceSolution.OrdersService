using BusinessLogic.Mappers;
using BusinessLogic.ServiceContracts;
using BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogic
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddAutoMapper(config => config.LicenseKey = "", typeof(MappingProfile));

            services.AddScoped<IOrdersService, OrdersService>();

            return services;
        }
    }
}
