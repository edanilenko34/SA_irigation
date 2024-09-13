namespace SA.Irrigation.API.Configuration
{
    public class QueueProcessingConfiguration
    {
        [ConfigurationKeyName("QUEUE_PROCESSING_DELAY")]
        public int WaitTime { get; set; } = 10;
    }
}
