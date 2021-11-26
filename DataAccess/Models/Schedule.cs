using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Schedule
    {
        public Schedule()
        {
            Notifications = new List<Notification>();
        }
        public int ScheduleId { get; set; }
        public virtual List<Notification> Notifications { get; set; }

    }

}
