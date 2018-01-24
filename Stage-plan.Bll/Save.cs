using Stage_Plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public class Save : DataAccess
    {
        public int SaveBand(string name, decimal width, decimal height, int userAccountId, string bandWebUrl, string socialMediaUrl, string genre, string country, bool willSaveForRecent)
        {

            var dc = new Dal.StageplanEntities();
            var guid = GetUniqueGuid();
            var stagePlan = new Dal.Stageplan()
            {
                BandName = name,
                Token = guid,
                Height = height,
                Width = width,
                SavedByAccountId = userAccountId,
                CreationDate = DateTime.Now,
                Genre = genre,
                SocialMedia = WebPaths.AddMissingHttp(socialMediaUrl),
                Website = WebPaths.AddMissingHttp(bandWebUrl),
                Country = country,
                WillShowInRecentBands = willSaveForRecent
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
                if (!dc.Stageplans.Any(a => a.Token == guid))
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
                Src = inst.Src,
                Stageplan = stagePlan,
                BandMember = inst.BandMemberName,
                IsFixedPosition = inst.IsFixedPosition,
                SelectedInstrument = inst.SelectedInstrument,
                Width = inst.Width,
                Height = inst.Height,
                Zindex = inst.Zindex,
                RotateAngle = inst.RotateAngle
            };

            dc.StageplanInstruments.Add(instrument);

            return base.SaveToDatabase(dc);
        }

    }
}
