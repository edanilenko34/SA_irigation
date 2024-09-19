using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl.AdoJobStore;
using SA.Irrigation.API.Configuration;
using SA.Irrigation.API.Services;
using SA.Irrigation.Common.Configuration;
using SA.Irrigation.Common.QuartzJobs;
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

            var waitInSeconds = configuration.Get<QueueProcessingConfiguration>().WaitTime;

            services.AddQuartz(q =>
            {
                
                q.SchedulerId = "IrrigationScheduler";
                q.SchedulerName = "IrrigationScheduler";
                q.UseSimpleTypeLoader();
                q.UsePersistentStore(store =>
                {
                    store.UseSqlServer(options =>
                    {
                        options.UseDriverDelegate<SqlServerDelegate>();
                        options.ConnectionString = configuration.Get<DatabaseConfiguration>().ConnectionString;
                        options.TablePrefix = "quartz.QRTZ_";
                    });
                    store.UseSystemTextJsonSerializer();
                    store.PerformSchemaValidation = false;
                });
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });

                var jobKey = new JobKey("QueueProcessing");
                q.AddJob<QueueProcessingJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => 
                    opts.ForJob(jobKey).WithIdentity("QueueProcessing-trigger").WithSimpleSchedule(a=> a.WithIntervalInSeconds(waitInSeconds).RepeatForever())
                );

            });

        
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            services.AddScoped<ISchedulerManager, SchedulerManager>();
           

           

            return services;
        }
    }
}
