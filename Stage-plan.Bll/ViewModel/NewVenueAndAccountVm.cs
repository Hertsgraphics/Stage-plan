//using Stage_Plan.Bll;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Stage_Plan.Bll.ViewModel
//{
//    public class NewVenueAndAccountVm 
//    {
//        public Bll.Account Account { get; set; }

//        public Bll.Captcha Captcha { get; set; }

//        public Bll.Template.Venue Venue { get; set; }

//        public string Save()
//        {
//            var message = String.Empty;
//            var account = this.Account.Save(out message);
//            if (account == null)
//            {
//                if (String.IsNullOrEmpty(message))
//                    return "Something went wrong. Sorry we can't complete this, please try again or email us direct via the contact page";

//                return message;
//            }
//            var venue = this.Venue.Save(account.Id, out message);
//            if (venue == null)
//            {
//                this.Account.Delete(account.Id);

//                if (String.IsNullOrEmpty(message))
//                    return "Something went wrong. Sorry we can't complete this, please try again or email us direct via the contact page";

//                return message;
//            }

//            System.Diagnostics.Debug.Assert(false, "Need to add an auth cookie");

//            return message;
//        }
//    }
//}