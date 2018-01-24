using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public class Url : DataAccess
    {
        public string GetUrl(int id)
        {
            return WebPaths.GetPlan(base.DataContext.Stageplans.Single(a => a.Id == id && !a.IsDeprecated).Token);
        }
    }
}
