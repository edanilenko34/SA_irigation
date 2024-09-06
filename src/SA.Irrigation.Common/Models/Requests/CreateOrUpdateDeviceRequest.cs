using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Models.Requests
{
    public class CreateOrUpdateDeviceRequest
    {
        public int Address { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(1024)]
        public string? Description { get; set; }
        public Guid ModelId  { get; set; }
    }
}
