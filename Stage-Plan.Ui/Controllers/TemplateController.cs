using Stage_Plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stage_Plan.Ui.Controllers
{
    public class TemplateController : Controller
    {
        public ActionResult Venue(string id)
        {
            Instruments instruments = null;

            if (id == "test")
            {
                instruments = new Bll.TestUnitHelper.Mock.InstrumentsMock().GetDemoTemplate();
            }
            else
            {
                var instrument = new Bll.Instrument();
                instruments = instrument.GetInstruments(id);
            }

            if (instruments == null)
            {
                instruments = new Instruments();
                return RedirectToAction("MissingPlan");
            }

            var model = new Models.PlanViewModel(instruments, id);
            return View(model);
        }

        public ActionResult MissingPlan()
        {
            return View();
        }
    }
}