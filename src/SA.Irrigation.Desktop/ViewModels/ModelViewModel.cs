using SA.Irrigation.Desktop.RestApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Desktop.ViewModels
{
    public class ModelViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DeviceType? ModelDeviceType { get; set; }
        public string? OpenCommandText { get; set; }
        public string? CloseCommandText { get; set; }
        public string? GetDataCommandText { get; set; }
    }
}
