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
            return View();
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
        

        public ActionResult ThankYou(int type)
        {

            switch (type)
            {
                case 0: //From Contact page
ViewBag.Message = "<h1>Hurray...we've got your message!</h1><p> Thank you for you message, we'll review you message and come back to you if required!</p>";
                    break;

                case 1: //From the Sign up request page
                    ViewBag.Message = "<h1>Hurray...it's great you want to join us</h1> <p> We've sent you an email. You'll need to check it out and follow the instructions to complete the sign up.</p>";
                    break;

                case 2: //From Sign up confirmed page
                    ViewBag.Message = "<h1>Hurray...You are now subscribed!</h1><p>Thank you for you for your support!</p>";
                    break;

                default:
                    ViewBag.Message = "<h1>Thank you...</h1>";
                    break;
            }

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


        public ActionResult OptinRequest()
        {
            return RedirectToAction("ThankYou", new { type = 1 });
        }

        public ActionResult OptinConfirm()
        {
            return RedirectToAction("ThankYou", new { type = 2 });
        }



        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            var email = "support@stage-plan.com";
            var x = Utilies.Email.SendEmail(false, "mail.stage-plan.com", false, 25,email , GetPassword(), email, new List<string> { model.Email }, null, new List<string>{ email },"Email from Stage-Plan", true, GetBody(model));
            //todo verification
            return RedirectToAction("ThankYou", new { type = 0 });

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