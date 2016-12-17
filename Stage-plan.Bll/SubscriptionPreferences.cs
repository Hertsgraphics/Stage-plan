using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_plan.Bll
{
    public class SubscriptionPreferences
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string OptInPreference { get; set; }
        public bool IsOptIn { get; set; }
    }
}
