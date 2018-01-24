using Stage_Plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage_Plan.Ui.Models
{
    public class PlanViewModel
    {
        public string Token { get; set; }
        public SubscriptionPreferences SubscriptionPreferences { get; set; }
        public Models.SharePlan Contact { get; set; }
        public Stage_Plan.Bll.Instruments Instruments { get; set; }
        public PlanViewModel()
        {
            this.SubscriptionPreferences = new SubscriptionPreferences();
        }

        public PlanViewModel(Bll.Instruments inst, string token): this()
        {
            this.Token = token;
            this.Instruments = inst;
            this.Contact = new SharePlan();
            this.Contact.Band = inst.BandName;
            this.Contact.Token = token;
        }
    }
}