﻿using SA.Irrigation.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Models.Dto
{
    public class ScheduleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string StartCron { get; set; }
        public FinishByType FinishBy { get; set; }
        public TimeSpan? FinishDelta { get; set; } 
        public Guid? FinishDeviceId { get; set; }
        public double? FinishValue { get; set; }
    }
}