using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.API.Configuration
{
    public class LoraConfiguration
    {
        [ConfigurationKeyName("LORA_PORT_BAUD_RATE")]
        public int BoudRate { get; set; } = 9600;
        [ConfigurationKeyName("LORA_PORT_NAME")]
        public string PortName { get; set; } = string.Empty;
        [ConfigurationKeyName("LORA_DEVICE_ADDRESS")]
        public int Address { get; set; } = 65534;
    }
}
