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
            this.IsOptIn = true;
        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }

        public string Token { get; private set; }

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

        public SubscriptionPreferences GetEmailByAddress(string emailAddress)
        {
            var email = this._dc.MailingLists.SingleOrDefault(a => a.EmailAddress == emailAddress);
            return Map(email);
        }

        private SubscriptionPreferences Map(MailingList email)
        {
            return new SubscriptionPreferences()
            {
                EmailAddress = email.EmailAddress,
                IsOptIn = email.IsOptin,
                Name = email.Name,
                Token = email.ConfirmToken
            };
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

            this._dc.MailingLists.Remove(email);
            return Save() ? String.Empty : this._faultSavingMessage;
        }

        public bool ConfirmSubscription(string token)
        {
            var result = this._dc.MailingLists.SingleOrDefault(a => a.ConfirmToken == token);
            if (result == null)
                return false;

            result.IsConfirmed = true;
            result.DateOptInConfirm = DateTime.Now;
            return Save();
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
