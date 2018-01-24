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

        public ActionResult Subscribe()
        {
            var model = new SubscriptionPreferences(Request.UserHostAddress);
            return View(model);
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

        [HttpPost]
        public ActionResult OptinRequest(SubscriptionPreferences model)
        {
            model.IpAddress = Request.UserHostAddress;

            var errorMsg = model.Update();
            if (String.IsNullOrEmpty(errorMsg))
            {
                var email = new Email();
                
                model.Send(email);

                return RedirectToAction("ThankYou", new { header = "Hurray...it's great you want to join us", msg1 = "Thanks for joining! We need you to do one more thing - Please check your email and you'll see a link to confirm your subscription. If you don't see an email from us please check your spam folder and add us to your white list." });
            }
            else
            {
                Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "Fault subscribing in to Stage Plan", true, "<p>stageplansp15f35 Could not subscribe: " + model.EmailAddress + " <br/>" + model.Name + "<br />IsOptIn: " + model.IsOptIn + "<br />" + errorMsg);

                return RedirectToAction("ThankYou", new { header = "Oh no...it's great you want to join us but something is wrong...", msg1 = errorMsg });
            }


        }

        public ActionResult OptinConfirm(string id)
        {
            //id is the token
            var sp = new Stage_Plan.Bll.SubscriptionPreferences(Request.UserHostAddress);
            var didConfirm = sp.ConfirmSubscription(id);

            return RedirectToAction("ThankYou", new { header = "Hurray...You are now subscribed!", msg = "Thank you for you for your support!" });
        }



        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            var email = "support@stage-plan.com";
            var x = Utilies.Email.SendEmail(false, "mail.stage-plan.com", false, 25, email, GetPassword(), email, new List<string> { model.Email }, null, new List<string> { email }, "Email from Stage-Plan", true, GetBody(model));

            if (String.IsNullOrEmpty(x))
                Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "Fault with stage plan", true, "<p>Failed to send contact... MSG: " + x + "</p>Name: " + model.Name + "... Email: " + model.Email + "... Message: " + model.Message);
            //todo verification
            return RedirectToAction("ThankYou", new { header = "Hurray...we've got your message!", msg1 = "Thank you for you message, we'll review you message and come back to you if required!" });

        }

        private string GetBody(Contact model)
        {
            return "<p>Name: " + model.Name + "</p><p>Email: " + model.Email + "</p><p>Message: " + model.Message + "</p>";
        }

        private string GetPassword()
        {
            Email email = new Email();
            return email.GetPassword();
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