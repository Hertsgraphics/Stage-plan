using Stage_Plan.Ui.Models.Contact;
using System;
using System.Collections.Generic;
using Stage_Plan.Bll;
using System.ComponentModel.DataAnnotations;

namespace Stage_Plan.Ui.Models
{
    public class SharePlan : AbstractContact, ICaptcha
    {
        public override bool Send(string subject)
        {
            try
            {
                var email = new Email();
                email.SendEmail(this.TheirEmail, new List<string>() { base.Email }, GetContent(), subject);
                base.DidSend = true;
                return base.DidSend;
            }
            catch (Exception ex)
            {
                base.DidSend = false;
                return base.DidSend;
            }
        }

        [Required]
        public string TheirName { get; set; }
        [Required]
        public string TheirEmail { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Band { get; set; }
        [Required]
        public string CaptchaCode { get; set; }
        public string CaptchaPath { get; set; }

        public override string GetContent()
        {
            return "<p>Hi " + this.TheirName + "</p><p>" + base.Name + " from " + this.Band + " has shared a stage plan with you. The unique URL is: </p><p>" + WebPaths.GetPlanLink(this.Token) + "</p><p>You have not been subscribed to our list nor have we stored your details (unless you have previously registrered with us). To find out more about us please visit <a href = \"" + WebPaths.GetDomain() + "\">" + WebPaths.GetDomain() + "</a></p>";
        }


    }
}