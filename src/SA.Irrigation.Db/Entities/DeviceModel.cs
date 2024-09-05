using Microsoft.EntityFrameworkCore;
using SA.Irrigation.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Db.Entities
{ 

    [Index(nameof(Name), IsUnique = true)]
    public class DeviceModel
    {
        public Guid Id { get; set; }
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(1024)]
        public string? Description { get; set; }
        public DeviceType Type { get; set; }
        [StringLength(64)]
        public string? OpenCommand { get; set; }
        [StringLength(64)]
        public string? CloseCommand { get; set; }
        [StringLength(64)]
        public string? GetDataCommand { get; set; }
    }
}
