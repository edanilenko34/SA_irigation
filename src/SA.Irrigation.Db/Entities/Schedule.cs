using SA.Irrigation.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Db.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string StartCron { get; set; }
        public string? FinishCron { get; set; }
        public FinishByType FinishBy { get; set; }
        public Device ParentDevice { get; set; }
        public Sensor? FinishDevice { get; set; }
        public double? FinishValue { get; set; }
    }
}
