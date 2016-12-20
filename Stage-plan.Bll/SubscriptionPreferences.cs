using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stage_plan.Dal;

namespace Stage_plan.Bll
{
    public class SubscriptionPreferences
    {
        private Dal.StageplanEntities _dc;
        private string _faultSavingMessage = "There was a fault. We could not save the change. Please email us direct";

        public SubscriptionPreferences()
        {
            this._dc = new Dal.StageplanEntities();
        }

        public SubscriptionPreferences GetEmailByAddress(string email)
        {
            var result = this._dc.MailingLists.Where(a => a.EmailAddress == email).SingleOrDefault();
            if (result == null)
                return null;

            return Map(result);
        }

        private SubscriptionPreferences Map(MailingList result)
        {
            return new SubscriptionPreferences()
            {
                EmailAddress=result.EmailAddress,
                IsOptIn = result.IsOptin,
                Name = result.Name
            };
        }

        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }


        [Required]
        public bool IsOptIn { get; set; }

        public string Update()
        {
            string result = String.Empty;
            if (IsOptIn)
                result = OptIn();
            else
                result = OptOut();

            return result;
        }

        public Dal.MailingList GetEmail()
        {
            var email = ((from d in this._dc.MailingLists
                          where d.EmailAddress == this.EmailAddress
                          select d).SingleOrDefault());
            return email;
        } 

        private string OptIn()
        {
            var email = GetEmail();
            if (email != null && email.IsOptin)
                return "The email address provided is already opt in.";

            email = new Dal.MailingList()
            {
                ConfirmToken = Guid.NewGuid().ToString(),
                DateOptInRequest = DateTime.Now,
                DateOptInConfirm = new DateTime(),
                EmailAddress = this.EmailAddress,
                IsOptin = this.IsOptIn,
                Name = this.Name
            };

            this._dc.MailingLists.Add(email);
            return Save() ? String.Empty : this._faultSavingMessage;
        }

        private string OptOut()
        {
            var email = GetEmail();

            if (email == null)
                return "Sorry, we don't have any match for the email address provided.";

            email.IsOptin = this.IsOptIn;
            return Save() ? String.Empty : this._faultSavingMessage;
        }

        private bool Save()
        {
            try
            {
                this._dc.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                //TODO send fail email
                return false;
            }
        }
    }
}
