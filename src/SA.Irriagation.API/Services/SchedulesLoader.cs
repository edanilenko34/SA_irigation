
using SA.Irrigation.API.Configuration;
using SA.Irrigation.Common.Models.QuartzJobs;
using SA.Irrigation.Common.QuartzJobs;
using SA.Irrigation.Common.Services;
using SA.Irrigation.Db;
using System.Data.Entity;

namespace SA.Irrigation.API.Services
{
    public class SchedulesLoader : ISchedulesLoader
    {

        private readonly ILogger<SchedulesLoader> _logger;
        private readonly IrrigationDbContext _context;
        private readonly ISchedulerManager _schedulerManager;
        private readonly int _waitInSeconds;

        public SchedulesLoader(ILogger<SchedulesLoader> logger, IrrigationDbContext context, ISchedulerManager schedulerManager, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(schedulerManager, nameof(schedulerManager));
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            _logger = logger;
            _context = context;
            _schedulerManager = schedulerManager;

            var _waitInSeconds = configuration.Get<QueueProcessingConfiguration>().WaitTime;
        }

        public async Task Load()
        {
            await AddQueueProcessJob();

            //var commandSchedules = _context.Schedules.Where(w => w.FinishBy == Common.Enums.FinishByType.ByTime).ToList();
            //foreach (var schedule in commandSchedules) 
            //{
            //    var keyStart = $"send_{schedule.Id}_open";
            //    var keyFinish = $"send_{schedule.Id}_close";
            //    if (schedule.IsDisabled)
            //    {
            //        await _schedulerManager.UnscheduleJob(keyStart);
            //        await _schedulerManager.UnscheduleJob(keyFinish);
            //    }
            //    else
            //    {
            //        var parentDevice = _context.Devices.Include(d => d.Model).First(p => p.Id == schedule.ParentDeviceId);
            //        _context.Entry(parentDevice).Reference(d => d.Model).Load();
            //        if (parentDevice != null)
            //        {
            //            var parameters = new JobParameters(keyStart, schedule.StartCron, schedule.StartDate, schedule.FinishDate, new SendJobData { Address = schedule.ParentDevice.Address, Message = parentDevice.Model.OpenCommand! }, null);
            //            await _schedulerManager.ScheduleJob<SendCommandJob, JobParameters>(parameters);
            //            parameters = new JobParameters(keyFinish, schedule.FinishCron!, schedule.StartDate, schedule.FinishDate, new SendJobData { Address = schedule.ParentDevice.Address, Message = parentDevice.Model.CloseCommand! }, null);
            //            await _schedulerManager.ScheduleJob<SendCommandJob, JobParameters>(parameters);
            //        }
            //        else
            //        {
            //            _logger.LogError("Parent device with id={id} not found", schedule.ParentDevice.Id);
            //        }
            //    }
            //}
        }

        private async Task AddQueueProcessJob()
        {
            var parameters = new JobParameters("QueueProcessing", string.Empty, null, null, null, _waitInSeconds);


            await _schedulerManager.ScheduleJob<QueueProcessingJob, JobParameters>(parameters);
        }
    }
}
