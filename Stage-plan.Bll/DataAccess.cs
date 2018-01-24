using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public abstract class DataAccess 
    {
        protected Dal.StageplanEntities DataContext;

        public DataAccess()
        {
            this.DataContext = new Dal.StageplanEntities();
        }

        protected bool SaveToDatabase()
        {
            try
            {
                this.DataContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// DO NOT USE ANY MORE
        /// </summary>
        /// <param name="dc"></param>
        /// <returns></returns>
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

        
    }
}
