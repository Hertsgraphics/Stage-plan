using Stage_plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Stage_plan.Ui.Models;
using System.Text;

namespace Stage_plan.Ui.Controllers
{
    public class StageplanController : Controller
    {
        // GET: Stageplan
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveNewStagePlan(Instruments model)
        {
            //todo save
            if (String.IsNullOrEmpty(model.BandName))
                return Json(new { id = -99 });

            var save = new Save();
            var id = save.SaveBand(model.BandName);

            foreach (var instrument in model.AllInstruments)
            {
                //make sure the src is relative, not aboluste due to testing from aboslute path
                var imgSrc = GetSrc(instrument.Src);

                Stage_plan.Bll.IInstrument inst = (Bll.IInstrument)instrument;

                bool didSave = save.SaveInstrument(inst, id);
            }

            var url = Bll.DataAccess.GetUrl(id);

            return Json(new { url = url });
        }

        private string GetSrc(string src)
        {
            var splitty = src.Split('/');
            var result = new StringBuilder();
            for (int i = 3; i < splitty.Length; i++)
            {
                result.Append(splitty[i]);

                if (i < splitty.Length - 1)
                    result.Append("\\");
            }

            return result.ToString();
        }

        //[HttpPost]
        //public JsonResult SaveInstrumentToStagePlan(Instruments model)
        //{
        //    // need to create C# object to match the incoming javascript object 
        //    //todo save
        //    System.Threading.Thread.Sleep(2000);
        //    return Json(new { currentInstrument = 1}, JsonRequestBehavior.AllowGet);
        //}
    }
}