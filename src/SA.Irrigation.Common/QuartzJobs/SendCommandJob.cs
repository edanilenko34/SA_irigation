using Quartz;
using SA.Irrigation.Common.Models.QuartzJobs;
using SA.Irrigation.Common.Models.QueueItems;
using SA.Irrigation.Common.Services;
using System.Text.Json;

namespace SA.Irrigation.Common.QuartzJobs
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
            var dataStr = context.MergedJobDataMap.GetString("sendData");
            if (!string.IsNullOrEmpty(dataStr))
            {
                var data = JsonSerializer.Deserialize<SendJobData>(dataStr);
                _queueProcessor.Enqueue(data);
            }

            return Task.CompletedTask;
        }
    }
}
