using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Dal.Interfaces
{
    public interface IAccount
    {
        int Id { get; set; }
        string EmailAddress { get; set; }
        string TempPassword { get; set; }
        bool IsEnabled { get; set; }
        int VenueTemplateLimit { get; set; }
    }
}
