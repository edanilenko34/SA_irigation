using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Desktop.ViewModels
{
    public class DeviceViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Address { get; set; }
        public Guid ModelId { get; set; }
    }
}
