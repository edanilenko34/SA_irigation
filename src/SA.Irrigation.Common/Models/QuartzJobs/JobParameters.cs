using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Models.QuartzJobs
{
    public class JobParameters
    {
        public string Name { get; private set; }
        public string CronExpression { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public SendJobData? SendJobData { get; private set; }

        public int? Interval { get; private set; }

        public JobParameters(string name, string cronExpression, DateTime? startDate, DateTime? endDate, SendJobData? sendJobData, int? interval ) 
        {
            Name = name;
            CronExpression = cronExpression;
            StartDate = startDate;
            EndDate = endDate;
            SendJobData = sendJobData;
            Interval = interval;
        }
    }
}
