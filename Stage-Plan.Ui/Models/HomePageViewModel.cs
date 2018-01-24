using Stage_Plan.Bll;
using System.Collections.Generic;

namespace Stage_Plan.Ui.Models
{
    public class HomePageViewModel
    {
        public SubscriptionPreferences SubscriptionPreferences { get; set; }
        public IEnumerable<RecentBands> RecentBands { get; set; }
        public Rating Rating { get; set; }

        public HomePageViewModel()
        {
            //shut up mvc
        }

        public HomePageViewModel(string ipAddress, int limit)
        {
            this.SubscriptionPreferences = new SubscriptionPreferences(ipAddress);
            var recentBands = new RecentBands();
            this.RecentBands = recentBands.GetRecentBands(limit);
            this.Rating = new Rating();
            this.Rating.SetLatestResults();
        }
    }
}