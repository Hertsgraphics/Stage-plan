using Stage_Plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class OptinRequestController : Controller
    {
        [HttpPost]
        public ActionResult Index(SubscriptionPreferences model)
        {
            model.IpAddress = Request.UserHostAddress;

            var errorMsg = model.Update();
            if (String.IsNullOrEmpty(errorMsg))
            {
                var email = new Email();

                model.Send(email);

                return RedirectToAction("ThankYou", "Home", new { header = "Hurray...it's great you want to join us", msg1 = "Thanks for joining! We need you to do one more thing - Please check your email and you'll see a link to confirm your subscription. If you don't see an email from us please check your spam folder and add us to your white list." });
            }
            else
            {
                Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "Fault subscribing in to Stage Plan", true, "<p>stageplansp15f35 Could not subscribe: " + model.EmailAddress + " <br/>" + model.Name + "<br />IsOptIn: " + model.IsOptIn + "<br />" + errorMsg);

                return RedirectToAction("ThankYou","Home", new { header = "Oh no...it's great you want to join us but something is wrong...", msg1 = errorMsg });
            }


        }

       
    }
}