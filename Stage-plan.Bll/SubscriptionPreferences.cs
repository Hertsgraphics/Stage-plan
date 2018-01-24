using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stage_Plan.Dal;

namespace Stage_Plan.Bll
{
    public class SubscriptionPreferences : ICaptcha
    {
        private Dal.StageplanEntities _dc;
        private string _faultSavingMessage = "There was a fault. We could not save the change. Please email us direct";

        public SubscriptionPreferences()
        {
            this._dc = new Dal.StageplanEntities();
            this.IsOptIn = true;
        }

        public SubscriptionPreferences(string ipAddress) : this()
        {
            this.IpAddress = ipAddress;

        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        public string IpAddress { get; set; }

        public string Token { get; private set; }

        [Required]
        [Display(Name = "Enter the letters/numbers")]
        public string CaptchaCode { get; set; }

        [Required]
        public bool IsOptIn { get; set; }

        public string CaptchaPath { get; set; }


        public string Update()
        {
            string result = String.Empty;
            var token = String.Empty;
            if (IsOptIn)
                result = OptIn(out token);
            else
                result = OptOut();

            this.Token = token;
            return result;
        }

        public SubscriptionPreferences GetEmailByAddress(string emailAddress)
        {
            var email = this._dc.MailingLists.SingleOrDefault(a => a.EmailAddress == emailAddress);
            return Map(email);
        }



        public Dal.MailingList GetEmail()
        {
            var email = ((from d in this._dc.MailingLists
                          where d.EmailAddress == this.EmailAddress
                          select d).SingleOrDefault());
            return email;
        }

        public void Send(Email email)
        {
            var webPage = WebPaths.GetDomain() + "/OptinConfirm/" + this.Token;
            var msg = "<p>Hi " + this.Name + "</p><p>Thanks for joining us! There is one thing we need, and that is for you to verify you want our occasional emails. To do so, simply visit this page: <a href=\""+webPage+"\" > " + webPage + "</a><p>Please don't forget to add us to your safe list.</p>";

            email.SendEmail(this.EmailAddress, null, msg, "Confirm your subscription to Stage Plan");
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

        #region Private

        private string OptIn(out string token)
        {
            token = String.Empty;
            var email = GetEmail();
            if (email != null && email.IsOptin)
                return "The email address provided is already opt in.";

            if (email == null)
            {
                email = New();
                this._dc.MailingLists.Add(email);
            }
            else
            {
                email.Name = this.Name;
                email.IsOptin = this.IsOptIn;
            }
            email.IpAddress = this.IpAddress;
            token = email.ConfirmToken;

            var msg = Validate();
            if (!String.IsNullOrEmpty(msg))
                return msg;

            return Save() ? String.Empty : this._faultSavingMessage;
        }

        private SubscriptionPreferences Map(MailingList email)
        {
            return new SubscriptionPreferences(email.IpAddress)
            {
                EmailAddress = email.EmailAddress,
                IsOptIn = email.IsOptin,
                Name = email.Name,
                Token = email.ConfirmToken
            };
        }

        private Dal.MailingList New()
        {
            return new Dal.MailingList()
            {
                ConfirmToken = Guid.NewGuid().ToString(),
                DateOptInRequest = DateTime.Now,
                EmailAddress = this.EmailAddress,
                IsOptin = this.IsOptIn,
                Name = this.Name,
                IpAddress = this.IpAddress
            };
        }

        private string OptOut()
        {
            var email = GetEmail();

            if (email == null)
                return "Sorry, we don't have any match for the email address provided.";

            email.Name = this.Name;
            email.IsOptin = this.IsOptIn;
            email.IsConfirmed = false;//need to reset so user can re-opt in
            email.DateOptInConfirm = new DateTime();
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

        private string Validate()
        {
            var errorMsg = String.Empty;
            if (String.IsNullOrEmpty(this.EmailAddress) || !this.EmailAddress.Contains("@"))
                errorMsg += "Invalid email address. ";

            if (String.IsNullOrEmpty(this.Name))
                errorMsg += "Invalid name. ";


            Captcha captcha = new Bll.Captcha();
            if (!captcha.IsValid(this))
                errorMsg += "Invalid captcha. ";


            return errorMsg;
        }

        #endregion

    }
}
