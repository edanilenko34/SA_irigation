using SA.Irrigation.Common.Models.QueueItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Services
{
    public interface IQueueProcessor
    {
        void Enqueue(SendItem item);
        void ProcessQueue();
    }
}
