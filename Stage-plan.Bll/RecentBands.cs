using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public class RecentBands : DataAccess
    {
        public string BandName { get; set; }
        public string WebUrl { get; set; }
        public string SocialMediaUrl { get; set; }
        public string Genre { get; set; }
        public DateTime Created { get; set; }
        public string Country { get; set; }
        public string HtmlColor { get; set; }

        private Dal.StageplanEntities _dc;

        public RecentBands()
        {
            this._dc = new Dal.StageplanEntities();
        }

        public IEnumerable<RecentBands> GetRecentBands(int limit)
        {
            var query = (from d in this._dc.Stageplans
                         where d.WillShowInRecentBands == true
                         where d.IsDeprecated == false
                         group d by d.BandName into g
                         select g.FirstOrDefault()).OrderByDescending(a => a.Id).Take(limit);

            var result = new List<RecentBands>();

            int i =0;
            foreach (var item in query)
            {
                result.Add(new RecentBands()
                {
                    BandName = item.BandName,
                    Created = item.CreationDate,
                    Genre = item.Genre,
                    SocialMediaUrl = item.SocialMedia,
                    WebUrl = item.Website,
                    Country = item.Country,
                    HtmlColor = GetColour(i)
                });
                i++;
            }

            return result;
        }

        private string GetColour(int i)
        {
            switch (i)
            {
                case 0:
                    return "#7B4B92";
                case 5:
                    return "#2F579A";
                case 3:
                    return "#A52A2A";
                case 4:
                    return "#759B40";
                case 1:
                    return "#384C95";
                case 2:
                    return "#CCB094";
                case 7:
                    return "#58595B";
                case 6:
                    return "#926D5B";
                default:
                    return "#8AD2FA";
            }
        }
    }
}
