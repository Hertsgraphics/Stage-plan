using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage_plan.Ui.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Stage_plan_Vs_Stage_Plot()
        {
            return View();
        }

        public ActionResult What_An_Engineer_Wants_To_Know()
        {
            return View();
        }
    }
}