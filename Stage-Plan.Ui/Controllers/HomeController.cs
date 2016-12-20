using Stage_plan.Bll;
using Stage_Plan.Ui.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
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
        

        public ActionResult ThankYou(string message)
        {

            ViewBag.Message = message;

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


        public ActionResult OptinRequest(SubscriptionPreferences model)
        {
            model.Update();
            return RedirectToAction("ThankYou", new { message = "<h1>Hurray...it's great you want to join us</h1> <p> We've sent you an email. You'll need to check it out and follow the instructions to complete the sign up.</p>" });
        }

        public ActionResult OptinConfirm()
        {
            return RedirectToAction("ThankYou", new { message = "<h1>Hurray...You are now subscribed!</h1><p>Thank you for you for your support!</p>" });
        }



        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            var email = "support@stage-plan.com";
            var x = Utilies.Email.SendEmail(false, "mail.stage-plan.com", false, 25,email , GetPassword(), email, new List<string> { model.Email }, null, new List<string>{ email },"Email from Stage-Plan", true, GetBody(model));
            //todo verification
            return RedirectToAction("ThankYou", new { message = "<h1>Hurray...we've got your message!</h1><p> Thank you for you message, we'll review you message and come back to you if required!</p>" });

        }

        private string GetBody(Contact model)
        {
            return "<p>Name: " + model.Name + "</p><p>Email: " + model.Email + "</p><p>Message: " + model.Message + "</p>";
        }

        private string GetPassword()
        {
            return "757ujHGRn3";
        }
    }
}