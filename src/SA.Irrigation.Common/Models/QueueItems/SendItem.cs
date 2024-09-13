using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Models.QueueItems
{
    public class SendItem
    {
        public int Address { get; set; }
        public string Message { get; set; }
    }
}
