using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SA.Irrigation.Common.Configuration;
using SA.Irrigation.Common.Helpers;
using SA.Irrigation.Common.Services;
using System.IO.Ports;

namespace SA.Irrigation.Services.Implementation
{
    public class LoraTransportLayer : SerialPort, ITransportLayer
    {

        private readonly ILogger<LoraTransportLayer> _logger;
        private readonly LoraConfiguration _configuration;
        public int Channel { get;}

        public LoraTransportLayer(ILogger<LoraTransportLayer> logger, IConfiguration configuration) : base()
        {
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            _logger = logger;
            _configuration = configuration.Get<LoraConfiguration>();
            ArgumentNullException.ThrowIfNull(_configuration, nameof(_configuration));

            Channel = _configuration.Channel;
            PortName = _configuration.PortName;
            BaudRate = _configuration.BaudRate;
            

            SetDeviceSettings();
        }

        public void SendData(byte[] data)
        {
            if (!IsOpen) Open();
                Write(data, 0, data.Length);
        }

        public bool SetDeviceSettings()
        {
            var address = ((byte)(Convert.ToUInt16(_configuration.Address) >> 8)).ToString("X2")+" "+ ((byte)(Convert.ToUInt16(_configuration.Address) & 0xff)).ToString("X2");
            

            byte brate = 0b01100010;
            if (_configuration.BaudRate == 1200) brate = 0b00000010;
            if (_configuration.BaudRate == 2400) brate = 0b00100010;
            if (_configuration.BaudRate == 4800) brate = 0b01000010;
            if (_configuration.BaudRate == 19200) brate = 0b10000010;
            if (_configuration.BaudRate == 38400) brate = 0b10100010;
            if (_configuration.BaudRate == 57600) brate = 0b11000010;
            if ( _configuration.BaudRate == 115200) brate = 0b11100010;

            var len = 8;

            var settingsString = $"C0 00 {len.ToString("X2")} {address} {brate} 00 {_configuration.Channel.ToString("X2")} {0b00000001.ToString("x2")} 00 00";
            var dataToSend = HexStrToBytes.ToBytes(settingsString);

            SendData(dataToSend);
            Thread.Sleep(100);
            if (BytesToRead > 0)
            {
                var strRes = ReadExisting();
                strRes = HexStrToBytes.ToHexString(strRes);

                if (strRes == "FF FF FF") return true;
            }
            Close();
            return true;
        }
    }
}
