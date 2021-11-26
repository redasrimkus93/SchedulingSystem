using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public DateTime SendingDate { get; set; }
        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
