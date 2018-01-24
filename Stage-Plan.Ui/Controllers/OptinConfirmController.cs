using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class OptinConfirmController : Controller
    {
        // GET: OptinConfirm
        public ActionResult Join(string id)
        {
            //id is the token
            var sp = new Stage_Plan.Bll.SubscriptionPreferences(Request.UserHostAddress);
            var didConfirm = sp.ConfirmSubscription(id);

            var socialMeda = "<a href='Bll.WebPaths.GetFacebook()'>Facebook</a>";
            return RedirectToAction("ThankYou", "Home", new { header = $"Hurray...You are now subscribed!", msg = "Thank you for you for your support!" });
        }
    }
}