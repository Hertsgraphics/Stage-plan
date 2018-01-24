using Stage_Plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class CreateTemplateController : Controller
    {
        // GET: Template
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(Bll.ViewModel.NewVenueAndAccountVm VenueAndAccount)
        {
            var captcha = new Captcha();
            if (!captcha.IsValid(VenueAndAccount.Captcha))
            {
                return Json(new { IsError = true, Message = "The captcha code is not valid" });
            }

            var errorMessage = VenueAndAccount.Save();

            if (String.IsNullOrEmpty(errorMessage))
                return Json(new { IsError = false });

            return Json(new { IsError = true, Message = errorMessage });

            //return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }
}