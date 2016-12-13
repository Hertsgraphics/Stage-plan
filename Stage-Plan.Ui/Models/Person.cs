﻿using Stage_Plan.Ui.Models.Emails;
using Stage_Plan.Ui.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage_Plan.Ui.Models
{
    public class Person : AbstractContact
    {
        public override bool Send(string subject)
        {
            try
            {
                Email email = new Email();
                email.SendEmail(base.Email, base.GetContent(), subject);
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