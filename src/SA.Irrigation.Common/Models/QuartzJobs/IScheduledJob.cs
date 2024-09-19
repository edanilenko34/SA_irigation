using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Models.QuartzJobs
{
    public interface IScheduledJob<TParameters> : IJob where TParameters : JobParameters
    {
        public TParameters? Parameters { get; set; }
    }
}
