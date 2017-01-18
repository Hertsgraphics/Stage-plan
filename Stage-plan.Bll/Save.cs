using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_plan.Bll
{
    public class Save : DataAccess
    {
        public int SaveBand(string name)
        {

            var dc = new Dal.StageplanEntities();
            var guid = GetUniqueGuid();
            var stagePlan = new Dal.Stageplan()
            {
                BandName = name,
                URL = guid
            };

            dc.Stageplans.Add(stagePlan);

            if (base.SaveToDatabase(dc))
                return stagePlan.Id;

            return -99;
        }

        private string GetUniqueGuid()
        {
            var dc = new Dal.StageplanEntities();
            string guid = String.Empty;
            int i = 0;
            while (i < 100)
            {
                guid = Guid.NewGuid().ToString();
                if (!dc.Stageplans.Any(a => a.URL == guid))
                    return guid;
            }

            return guid;
        }

        public bool SaveInstrument(IInstrument inst, int id)
        {
            var dc = new Dal.StageplanEntities();
            var stagePlan = dc.Stageplans.Single(a => a.Id == id);
            var instrument = new Dal.StageplanInstrument()
            {
                DataText = inst.Text,
                DataDetail = inst.Detail,
                X = inst.Left, 
                Y = inst.Top,
                Src=inst.Src,
                Stageplan = stagePlan
            };

            dc.StageplanInstruments.Add(instrument);

            return base.SaveToDatabase(dc);
        }
        
    }
}
