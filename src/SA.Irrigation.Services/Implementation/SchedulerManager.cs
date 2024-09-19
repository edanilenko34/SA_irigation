using Microsoft.Extensions.Logging;
using Quartz;
using SA.Irrigation.Common.Models.QuartzJobs;
using SA.Irrigation.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SA.Irrigation.Services.Implementation
{
    public class SchedulerManager : ISchedulerManager
    {

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<SchedulerManager> _logger;

        public SchedulerManager(ISchedulerFactory schedulerFactory, ILogger<SchedulerManager> logger)
        {
            ArgumentNullException.ThrowIfNull(schedulerFactory, nameof(schedulerFactory));
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));

            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task UnscheduleJob(string key)
        {
            var scheduler = await _schedulerFactory.GetScheduler("IrrigationScheduler");
            if (scheduler == null)
            {
                _logger.LogError("Scheduler with id = IrrigationScheduler not found");
                return;
            }
            await scheduler.UnscheduleJob(new TriggerKey(key));
        }

        public async Task ScheduleJob<TJob, TJobParameters>(TJobParameters jobParameters)
            where TJob : class, IJob
            where TJobParameters : JobParameters

        {
            var scheduler = await _schedulerFactory.GetScheduler("IrrigationScheduler");
            if (scheduler == null)
            {
                _logger.LogError("Scheduler with id = IrrigationScheduler not found");
                return;
            }

            var jobKey = new JobKey(jobParameters.Name);
            var job = await scheduler.GetJobDetail(jobKey);   

            if (job == null)
            {
                job = JobBuilder.Create<TJob>().WithIdentity(jobKey).Build();
                var trigger = BuildTrigger(jobParameters, job.Key);
                await scheduler.ScheduleJob(job, trigger);
            }
            else
            {
                var trigger = BuildTrigger(jobParameters, job.Key);
                await scheduler.ScheduleJob(job, new[] { trigger }, true);
            }
        }

        private ITrigger BuildTrigger<TJobParameters>(TJobParameters jobParameters, JobKey jobKey) where TJobParameters : JobParameters
        {
            var startDate = jobParameters.StartDate ?? DateTime.Now;
            var builder = TriggerBuilder.Create()
                .WithIdentity(new TriggerKey($"{jobKey.Name}_trigger"));
            if (jobParameters.SendJobData != null)
            {
                var dataString = JsonSerializer.Serialize<SendJobData>(jobParameters.SendJobData);

                var jobData = new JobDataMap
                {
                    { "sendData", dataString }
                };
                builder = builder.UsingJobData(jobData);
            }
            builder = builder.ForJob(jobKey).StartAt(startDate);
            if (jobParameters.Interval != null)
            {
                builder = builder.WithSimpleSchedule(x =>
                        x.WithIntervalInSeconds(jobParameters.Interval.Value));
            }
            else
                builder = builder.WithCronSchedule(jobParameters.CronExpression);

            if (jobParameters.EndDate != null)
                builder = builder.EndAt(jobParameters.EndDate);

            return builder.Build();
        }
    }
}
