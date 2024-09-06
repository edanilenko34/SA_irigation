using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Models.Dto
{
    public class DeviceDto
    {
        public Guid Id { get; set; }
        public int Address { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(1024)]
        public string? Description { get; set; }
        public DeviceModelDto Model { get; set; }
    }
}
