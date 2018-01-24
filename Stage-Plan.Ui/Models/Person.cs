using Stage_Plan.Ui.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stage_Plan.Bll;

namespace Stage_Plan.Ui.Models
{
    public class Person : AbstractContact
    {
        public override bool Send(string subject)
        {
            try
            {
                var email = new Email();
                email.SendEmail(base.Email, null, base.GetContent(), subject);
                base.DidSend = true;
                return base.DidSend;
            }
            catch (Exception ex)
            {
                base.DidSend = false;
                return base.DidSend;
            }
        }

    }
}