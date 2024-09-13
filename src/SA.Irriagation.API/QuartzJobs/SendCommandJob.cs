using Quartz;
using SA.Irrigation.Common.Models.QuartzJobs;
using SA.Irrigation.Common.Models.QueueItems;
using SA.Irrigation.Common.Services;

namespace SA.Irrigation.API.QuartzJobs
{
    public class SendCommandJob : IJob
    {
        private readonly IQueueProcessor _queueProcessor;

        public SendCommandJob(IQueueProcessor queueProcessor)
        {
            ArgumentNullException.ThrowIfNull(queueProcessor, nameof(queueProcessor));

            _queueProcessor = queueProcessor;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var data = (SendJobData)context.JobDetail.JobDataMap["sendData"];
            _queueProcessor.Enqueue((SendItem)data);

            return Task.CompletedTask;
        }
    }
}
