using Microsoft.EntityFrameworkCore;
using SA.Irrigation.API.Configuration;
using SA.Irrigation.Common.Configuration;
using SA.Irrigation.Common.Services;
using SA.Irrigation.Db;
using SA.Irrigation.Services.Implementation;

namespace SA.Irrigation.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigiration(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            services.Configure<LoraConfiguration>(configuration);
            services.Configure<DatabaseConfiguration>(configuration);

            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            services.AddDbContext<IrrigationDbContext>(options =>
            {
                options.UseSqlServer(configuration.Get<DatabaseConfiguration>().ConnectionString);
            });

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            services.AddScoped<IDeviceModelService, DeviceModelService>();

            return services;
        }
    }
}
