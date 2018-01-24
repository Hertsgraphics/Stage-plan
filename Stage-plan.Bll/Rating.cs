using Stage_Plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public class Rating : DataAccess
    {

        public int TotalVotes { get; private set; }
        public decimal TotalScore { get; private set; }
        public int MaximumScore { get { return this._totalScore; } }
        public int StagePlansCreated { get; private set; }

        private int _totalScore = 5;

        public void SetLatestResults()
        {
            var dc = new Dal.StageplanEntities();
            var result = (from d in dc.Generics
                          select new {
                              Score = d.TotalScore,
                              Votes = d.NumberOfVoters,
                              TotalPlans = d.TotalStagePlans
                          }).SingleOrDefault();

            this.TotalScore = result.Score;
            this.TotalVotes = result.Votes;
            this.StagePlansCreated = result.TotalPlans;
        }

        /// <summary>
        /// Gets the average score 
        /// </summary>
        /// <returns></returns>
        public decimal GetScore()
        {
            var dc = new Dal.StageplanEntities();
            var generic = dc.Generics.SingleOrDefault();
            
            var newScore = GetScore(generic.NumberOfVoters, generic.TotalScore);
            return Math.Round(newScore,2);
        }


        ///// <summary>
        ///// Returns the current score
        ///// </summary>
        ///// <returns></returns>
        //public decimal GetScore()
        //{
        //    var dc = new Dal.StageplanEntities();
        //    decimal dbScore = dc.Generics.Select(a => a.TotalScore).SingleOrDefault();
        //    return dbScore;
        //}


        public bool SaveRating(int newScore)
        {
            var dc = new Dal.StageplanEntities();
            var rating = dc.Generics.SingleOrDefault();
            if (rating == null)
            {
                rating = new Dal.Generic();
                dc.Generics.Add(rating);
            }
            rating.TotalScore += newScore;
            rating.NumberOfVoters++;

            return base.SaveToDatabase(dc);

        }

        /// <summary>
        /// Should be treated as privtate, exposed only for testing
        /// </summary>
        /// <param name="voters"></param>
        /// <param name="databaseScore"></param>
        /// <returns></returns>
        public decimal GetScore(decimal voters, int databaseScore)
        {
            if (databaseScore <= 0 || voters <= 0)
                return 0;

            return databaseScore / voters;
        }
        
    }
}
