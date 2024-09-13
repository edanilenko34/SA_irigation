using Quartz;
using SA.Irrigation.Common.Services;

namespace SA.Irrigation.API.QuartzJobs
{
    public class QueueProcessingJob : IJob
    {
        private readonly IQueueProcessor _queueProcessor;

        public QueueProcessingJob(IQueueProcessor processor)
        {
            ArgumentNullException.ThrowIfNull(processor, nameof(processor));
            _queueProcessor = processor;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _queueProcessor.ProcessQueue();

            return Task.CompletedTask;
        }
    }
}
