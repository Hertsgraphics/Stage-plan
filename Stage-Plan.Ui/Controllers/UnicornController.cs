using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Stage_Plan.Ui.Controllers
{
    public class UnicornController : Controller
    {


        //validPwrd = "Unic0rn5";
        //validUsr = "unicorn";

        // GET: Unicorn
        public ActionResult Index()
        {
            var login = new Models.Login.Login();
            return View(login);
        }

        [HttpPost]
        public ActionResult Index(Models.Login.Login login)
        {
            if (login.IsValid())
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
    1,                                     // ticket version
    login.Username,                              // authenticated username
    DateTime.Now,                          // issueDate
    DateTime.Now.AddMinutes(30),           // expiryDate
    true,                          // true to persist across browser sessions
    "",                              // can be used to store additional user data
    FormsAuthentication.FormsCookiePath);  // the path for the cookie

                // Encrypt the ticket using the machine key
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Add the cookie to the request to save it
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);

                return RedirectToAction("StatsForStagePlan");
            }
            return View();
        }

        // GET: Unicorn
        [Authorize]
        public ActionResult StatsForStagePlan()
        {
            var model = new Stage_Plan.Bll.Stats();
            return View(model);
        }
    }
}