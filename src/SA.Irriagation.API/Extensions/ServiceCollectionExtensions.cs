using Microsoft.EntityFrameworkCore;
using Quartz;
using SA.Irrigation.API.Configuration;
using SA.Irrigation.API.QuartzJobs;
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
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IScheduleService, ScheduleService>();

            services.AddSingleton<ITransportLayer, LoraTransportLayer>();
            services.AddSingleton<IQueueProcessor, LoraQueueProcessor>();

            return services;
        }

        public static IServiceCollection AddQuartzCustomJobs(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            services.Configure<QueueProcessingConfiguration>(configuration);

            var duration = configuration.Get<QueueProcessingConfiguration>().WaitTime;

            services.AddQuartz(q =>
            {
                var jobKey = new JobKey("QueuePeocessing");
                q.AddJob<QueueProcessingJob>(opts => opts.WithIdentity(jobKey).DisallowConcurrentExecution());
                q.AddTrigger(opts =>
                    opts.ForJob(jobKey)
                    .WithIdentity("QueueProcessingJob-trigget")
                    .WithSimpleSchedule(x =>
                        x.WithIntervalInSeconds(duration)
                        .RepeatForever()));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
