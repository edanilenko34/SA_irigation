using Xunit;
using SA.Irrigation.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SA.Irrigation.Common.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace SA.Irrigation.Services.Implementation.Tests
{
    public class LoraTransportLayerTests
    {
        private readonly LoraTransportLayer _layer;

        public LoraTransportLayerTests()
        {
            var loraSettings = new LoraConfiguration() { PortName = "COM3", Address = 65534 };
            var logger = new Mock<ILogger<LoraTransportLayer>>();

            _layer = new LoraTransportLayer(logger.Object, loraSettings);
        }

        [Fact()]
        public void SetDeviceSettingsTest()
        {
            var res = _layer.SetDeviceSettings();
            Xunit.Assert.True(res);
        }
    }
}