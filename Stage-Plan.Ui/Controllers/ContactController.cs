using Stage_Plan.Bll;
using Stage_Plan.Ui.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class ContactController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(Contact model)
        {
            var email = "support@stage-plan.com";
            var x = Utilies.Email.SendEmail(false, "mail.stage-plan.com", false, 25, email, GetPassword(), email, new List<string> { model.Email }, null, new List<string> { email }, "Email from Stage-Plan", true, GetBody(model));

            if (String.IsNullOrEmpty(x))
                Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "Fault with stage plan", true, "<p>Failed to send contact... MSG: " + x + "</p>Name: " + model.Name + "... Email: " + model.Email + "... Message: " + model.Message);
            //todo verification
            return RedirectToAction("ThankYou","Home", new { header = "Hurray...we've got your message!", msg1 = "Thank you for you message, we'll review you message and come back to you if required!" });

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

    }
}