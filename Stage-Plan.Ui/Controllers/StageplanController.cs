﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class StageplanController : Controller
    {
        // GET: Stageplan
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveNewStagePlan(string name)
        {
            //todo save
            return Json(new { id = 1 });
        }

        [HttpPost]
        public JsonResult SaveInstrumentToStagePlan(string name)
        {
            //todo save
            return Json(new { didSave = false});
        }
    }
}