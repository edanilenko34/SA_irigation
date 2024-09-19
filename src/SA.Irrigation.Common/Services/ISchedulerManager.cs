using Quartz;
using SA.Irrigation.Common.Models.QuartzJobs;

namespace SA.Irrigation.Common.Services
{
    public interface ISchedulerManager
    {
        public Task ScheduleJob<TJob, TJobParameters>(TJobParameters jobParameters)
            where TJob : class, IJob
            where TJobParameters : JobParameters;

        public Task UnscheduleJob(string key);
    }
}
 