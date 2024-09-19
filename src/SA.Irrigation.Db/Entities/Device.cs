using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Db.Entities
{
    [Index(nameof(Address), IsUnique = true)]
    public class Device
    {
        public Guid Id { get; set; }
        public  int Address { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(1024)]
        public string? Description { get; set; }
        public DeviceModel? Model { get; set; }
        public Guid? ModelId { get; set; }
        public IEnumerable<Schedule>? Schedules { get; set; }
    }
}
