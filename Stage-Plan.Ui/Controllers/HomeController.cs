using Stage_Plan.Bll;
using Stage_Plan.Ui.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Stage_Plan.Ui.Models;

namespace Stage_Plan.Ui.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomePageViewModel(Request.UserHostAddress, 6);
            return View(model);
        }

        public ActionResult Blog()
        {

            return View();
        }

        public ActionResult About()
        {

            return View();
        }


        public ActionResult Disclaimer()
        {
            return View();
        }

        public ActionResult Privacy_Policy()
        {
            return View();
        }

        public ActionResult SubscriptionPreferences()
        {
            return View();
        }


        public ActionResult ThankYou(string header, string msg1, string msg2, string msg3)
        {
            var sb = new StringBuilder();
            sb.Append("<h1>" + header + "</h1>");

            sb.Append("<p>" + msg1 + "</p>");
            sb.Append("<p>" + msg2 + "</p>");
            sb.Append("<p>" + msg3 + "</p>");


            ViewBag.Message = sb.ToString();

            return View();
        }

        [HttpPost]
        public ActionResult SubscriptionPreferences(Stage_Plan.Bll.SubscriptionPreferences model)
        {

            model.IpAddress = Request.UserHostAddress;

            var errorMsg = model.Update();
            if (String.IsNullOrEmpty(errorMsg))
            {

                var email = new Email();

                if (model.IsOptIn)
                {

                    var host = Request.Url.Host;
                    var msg = "<p>Hi " + model.Name + "</p><p>Thanks for joining us! There is one thing we need, and that is for you to verify you want our occasional emails. To do so, simply visit this page: <a href=https://" + host + "/OptinConfirm/" + model.Token + " > " + host + "/OptinConfirm/" + model.Token + "</a><p>If you don't see an email from us please check your spam folder and add us to your white list.</p>";
                    email.SendEmail(model.EmailAddress,null, msg, "Confirm your subscription to Stage Plan");
                }
                else
                {
                    Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "Unsubscribe from Stage Plan", true, "<p>Unsubscribe: " + model.EmailAddress + " <br/>" + model.Name + "<br />IsOptIn: " + model.IsOptIn);

                    return RedirectToAction("ThankYou", new { header = "Sorry to see you go", msg1 = "You have been removed from our list" });
                }
            }
            else
            {
                Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "Fault subscribing in to Stage Plan", true, "<p>stageplansp15f35 Could not subscribe: " + model.EmailAddress + " <br/>" + model.Name + "<br />IsOptIn: " + model.IsOptIn + "<br />" + errorMsg);

                return RedirectToAction("ThankYou", new { header = "Oh no...it's great you want to join us but something is wrong...", msg1 = errorMsg });
            }

            return RedirectToAction("ThankYou", new { header = "Hurray...it's great you want to join us", msg1 = "We've sent you an email. You'll need to check it out and follow the instructions to complete the sign up." });

        }
    }
}