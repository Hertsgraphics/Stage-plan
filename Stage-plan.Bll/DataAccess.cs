using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_plan.Bll
{
    public abstract class DataAccess
    {
        protected bool SaveToDatabase(Dal.StageplanEntities dc)
        {
            try
            {
                dc.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static string GetUrl(int id)
        {
            Dal.StageplanEntities dc = new Dal.StageplanEntities();
            return dc.Stageplans.Single(a => a.Id == id).URL;
        }
    }
}
