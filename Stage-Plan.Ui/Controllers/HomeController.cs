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
        
        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            var email = "support@stage-plan.co.uk";
            var x = Utilies.Email.SendEmail(false, "mail.stage-plan.com", false, 25,email , GetPassword(), email, new List<string> { model.Email }, null, new List<string>{ email },"Email from Stage-Plan", true, GetBody(model));
            //todo verification
            return View();
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