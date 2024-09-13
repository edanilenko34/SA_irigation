using Microsoft.Extensions.Configuration;

namespace SA.Irrigation.Common.Configuration
{
    public class LoraConfiguration
    {
        [ConfigurationKeyName("LORA_PORT_BAUD_RATE")]
        public int BaudRate { get; set; } = 9600;
        [ConfigurationKeyName("LORA_PORT_NAME")]
        public string PortName { get; set; } = string.Empty;
        [ConfigurationKeyName("LORA_DEVICE_ADDRESS")]
        public int Address { get; set; } = 65534;
        [ConfigurationKeyName("LORA_DEVICE_CHANNEL")]
        public int Channel { get; set; } = 1;
    }
}
