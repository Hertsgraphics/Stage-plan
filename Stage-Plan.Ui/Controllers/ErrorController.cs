using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            var couldNotGetToPage = Request.QueryString["aspxerrorpath"];
            var previousPage = Request.ServerVariables["HTTP_REFERER"];

            //if (String.IsNullOrEmpty(couldNotGetToPage) && String.IsNullOrEmpty(previousPage))
                Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "Missing page on Stage Plan", true, "<p>Fault on stage plan... User couldn't get to " + couldNotGetToPage + " from " + previousPage);

            return View();
        }

        public ActionResult StageplanNotRecognised()
        {
            return View();
        }
    }
}