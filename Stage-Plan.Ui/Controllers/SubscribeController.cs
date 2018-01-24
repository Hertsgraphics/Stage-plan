using Stage_Plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class SubscribeController : Controller
    {
        public ActionResult Index()
        {
            var model = new SubscriptionPreferences(Request.UserHostAddress);
            return View(model);
        }
    }
}