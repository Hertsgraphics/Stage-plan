using Stage_plan.Bll;
using Stage_Plan.Ui.Models.Contact;
using Stage_Plan.Ui.Models.Emails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new SubscriptionPreferences();
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

        public ActionResult Contact()
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


        public ActionResult OptinRequest(SubscriptionPreferences model)
        {
            if (String.IsNullOrEmpty(model.Update()))
            {
                Email email = new Email();

                var host = Request.Url.Host;

                var msg = "<p>Hi " + model.Name + "</p><p>Thanks for joining us! There is one thing we need, and that is for you to verify you want our occasional emails. To do, simply visit this page: <a href=" + host + model.Token + ">" + host + model.Token + "</a>";
                email.SendEmail(model.EmailAddress, msg, "Confirm your subscription to Stage Plan");
            }
            else
            {
                Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "Fault subscribing in to Stage Plan", true, "<p>Coudln't subscribe: " + model.EmailAddress + " <br/>" + model.Name + "<br />" + model.IsOptIn);

                return RedirectToAction("ThankYou", new { header = "Hurray...it's great you want to join us", msg1 = "However, we've already detected you're already on our list! Good news!" });
            }


            return RedirectToAction("ThankYou", new { header = "Hurray...it's great you want to join us", msg1 = "We've sent you an email. You'll need to check it out and follow the instructions to complete the sign up." });
        }

        public ActionResult OptinConfirm(string token)
        {
            SubscriptionPreferences sp = new Stage_plan.Bll.SubscriptionPreferences();
            var didConfirm = sp.ConfirmSubscription(token);

            return RedirectToAction("ThankYou", new { header = "<h1>Hurray...You are now subscribed!", msg = "Thank you for you for your support!" });
        }



        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            var email = "support@stage-plan.com";
            var x = Utilies.Email.SendEmail(false, "mail.stage-plan.com", false, 25, email, GetPassword(), email, new List<string> { model.Email }, null, new List<string> { email }, "Email from Stage-Plan", true, GetBody(model));
            //todo verification
            return RedirectToAction("ThankYou", new { header = "Hurray...we've got your message!", msg1 = "Thank you for you message, we'll review you message and come back to you if required!" });

        }

        private string GetBody(Contact model)
        {
            return "<p>Name: " + model.Name + "</p><p>Email: " + model.Email + "</p><p>Message: " + model.Message + "</p>";
        }

        private string GetPassword()
        {
            return "757ujHGRn3";
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
    }
}