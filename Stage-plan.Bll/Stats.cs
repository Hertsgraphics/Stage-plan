using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public class Stats
    {
        public Dictionary<string, int> OtherInstruments
        {
            get
            {
                var dt = DateTime.Now.AddDays(-60);
                var dc = new Dal.StageplanEntities();
                var query = (from d in dc.StageplanInstruments
                             where d.Src.ToLower().Contains("other.png")
                             where d.Stageplan.CreationDate > dt
                             group d by d.DataText into g
                             select new
                             {
                                 count = g.Count(),
                                 key = g.Select(a => a.DataText).FirstOrDefault()
                             }
                              );


                return query.ToDictionary(k => k.key, v => v.count);
            }
        }

        public Dictionary<string, int> Visits
        {
            get
            {
                var dc = new Dal.StageplanEntities();
                var query = (from d in dc.Stageplans
                             group d by DbFunctions.TruncateTime(d.CreationDate) into g
                             select new
                             {
                                 count = g.Count(),
                                 key = g.Select(a => a.CreationDate).FirstOrDefault()
                             }
                             );



                var result = query.ToDictionary(k => k.key.ToString(), v => v.count);
                return result;
            }
        }

        /// <summary>
        /// band name, date created, url, number of members
        /// </summary>
        public IEnumerable<Tuple<string, DateTime, string, int>> RecentPlans
        {
            get
            {
                var dc = new Dal.StageplanEntities();
                var query = (from d in dc.Stageplans
                             select d).OrderByDescending(a => a.CreationDate).Take(15);

                var result = new List<Tuple<string, DateTime, string, int>>();
                foreach (var item in query)
                {
                    result.Add(new Tuple<string, DateTime, string, int>(item.BandName, item.CreationDate, item.Token, item.StageplanInstruments.Select(a => a.BandMember).Distinct().Count()));
                }
                return result;
            }
        }

        public string Rating
        {
            get 
            {
                var dc = new Dal.StageplanEntities();
                var rating = new Bll.Rating();
                var score = rating.GetScore();
                return dc.Generics.Select(a => " Total score of " + a.TotalScore + " from " + a.NumberOfVoters + " votes. Average score of " + score).Single();
            }
        }

        public int TotalStagePlans
        {
            get
            {
                var dc = new Dal.StageplanEntities();
                return dc.Generics.Select(a => a.TotalStagePlans).SingleOrDefault();
            }
        }
    }
}
