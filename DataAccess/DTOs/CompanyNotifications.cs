using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CompanyNotifications
    {
        public CompanyNotifications()
        {
            Notifications = new List<string>(); 
        }
        public Guid CompanyId { get; set; }
        public List<string> Notifications { get; set; }
    }
}
