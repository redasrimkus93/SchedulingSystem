using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Company
    {
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int CompanyNumber { get; set; }
        public string CompanyType { get; set; }
        public string Market { get; set; }
        public int? ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }

    }

}
