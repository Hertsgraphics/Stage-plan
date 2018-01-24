using Stage_Plan.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll.Template
{
    public class Venue : DataAccess
    {
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string SocialMedia { get; set; }
        public string VenueTemplateUrl { get; set; }
        public string VenueName { get; set; }
        public string Address { get; set; }
        public string StageWidth { get; set; }
        public string StageDepth { get; set; }
        

        public Dal.AccountVenue Save(int accountId, out string message)
        {
            message = string.Empty;

            message = ValidateForSave();

            if (!String.IsNullOrEmpty(message))
                return null;

            var venue = new Dal.AccountVenue()
            {
                VenueName = this.VenueName,
                VenueUrl = this.VenueTemplateUrl,
                ContactName = ContactName,
                AccountId = accountId
            };

            base.DataContext.AccountVenues.Add(venue);

            if (base.SaveToDatabase())
                return venue;


            if (base.DataContext.AccountVenues.Any(a => a.VenueUrl == this.VenueTemplateUrl))
                message = "The venue URL is already in use. ";

            return null;
        }

        private string ValidateForSave()
        {
            var message = String.Empty;
            if (String.IsNullOrEmpty(this.VenueName))
                message += "Please enter a venue name. ";

            if (String.IsNullOrEmpty(this.VenueTemplateUrl))
                message += "Please enter a venue URL. ";

            return message;
        }

        public string CaptchaCode { get; set; }

    }
}
