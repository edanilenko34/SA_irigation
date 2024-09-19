﻿using SA.Irrigation.Desktop.RestApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Desktop.ViewModels
{
    public class ScheduleViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string StartCron { get; set; }
        public string? FinishCron { get; set; }
        public FinishByType FinishBy { get; set; }
        public Guid? FinishDeviceId { get; set; }
        public double? FinishValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsDisabled { get; set; } = false;
    }
}
