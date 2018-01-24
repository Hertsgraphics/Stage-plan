using Stage_Plan.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using Stage_Plan.Ui.Models;
using System.Text;
using System.IO;

namespace Stage_Plan.Ui.Controllers
{
    public class StageplanController : Controller
    {
        // GET: Stageplan
        public ActionResult Index(string id)
        {
            return View();
        }

        public ActionResult Plan(string id)
        {
            Instruments instruments = null;

            if (id == "test")
            {
                instruments = new Bll.TestUnitHelper.Mock.InstrumentsMock().GetDemoInstruments();
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

        public FileStreamResult Pdf(string id)
        {
            if (id == null)
                RedirectToAction("Index", "Error");

            var pdf = new Bll.PdfWrapper.Pdf();
            Bll.Instruments instruments = null;
            if (id.ToLower() == "test")
            {
                instruments = new Bll.TestUnitHelper.Mock.InstrumentsMock().GetDemoInstruments();
            }
            else
            {
                var instrument = new Bll.Instrument();
                instruments = instrument.GetInstruments(id);
            }

            if (instruments == null)
                RedirectToAction("Index", "Error");

            byte[] byteInfo = pdf.Get(instruments, true);
            var workStream = new MemoryStream();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }

        public JsonResult SaveRating(string Message, int Rating)
        {
            var ratingBll = new Bll.Rating(); //ratingBll because the int Rating parameter
            if (Rating > 0 && Rating <= 5)
            {
                if (!ratingBll.SaveRating(Rating))
                    System.Diagnostics.Debug.Assert(false, "Failed to save");
            }

            var ratingScore = ratingBll.GetScore();

            if (Rating <= 3 || !String.IsNullOrEmpty(Message))
            {
                Email email = new Email();
                email.SendEmailToDaveAndTelis("New rating: " + Rating + "/5. Current average is : " + ratingScore + ". <p>" + Message + "</p>", "Rating from Stage Plan");
            }
            return Json(new { });

        }

        [HttpPost]
        public JsonResult SaveNewStagePlan(Instruments model)
        {
            if (String.IsNullOrEmpty(model.BandName))
                return Json(new { error = "Please enter a band name" });

            var captcha = new Captcha();
            if (!captcha.IsValid(model))
                return Json(new { error = "The captcha code was not correct" });


            var save = new Save();
            var id = save.SaveBand(model.BandName, model.Width, model.Height, -99, model.BandWebAddress, model.BandSocialMediaAddress, model.BandGenre, model.Country, model.WillShowInRecentBands);

            foreach (var instrument in model.AllInstruments)
            {
                //make sure the src is relative, not aboluste due to testing from aboslute path
                var imgSrc = GetSrc(instrument.Src);

                IInstrument inst = (Bll.IInstrument)instrument;

                bool didSave = save.SaveInstrument(inst, id);
            }

            var url = new Url().GetUrl(id);

            //Send notification to Dave and Telis
            var email = new Email();
            //email.SendEmailToDaveAndTelis(GetNewStagePlanAddedNotificationForDaveAndTelis(url, model.BandName), "New stage plan");

            if (!String.IsNullOrEmpty(model.TemporaryEmailAddress))
            {
                var content = "<p>Hi " + model.BandName + ",</p><p>Thank you for using stage plan! </p><p>Please take a minute to join us on any of the social media and please do let your friends, band mates and other musicians know about stage plan.</p><p>We also would love to hear direct from you - please let us know how we can make it better for you.</p><p>The unique URL is <a href=\"" + url + "\">" + url + "</a></p><p>Your URL will last for only " + System.Configuration.ConfigurationManager.AppSettings["DaysStagePlanRemains"] + " days. </p><p>Unless you asked us to, we've not signed you up nor have we even stored your email address on our database.</p>";

                email.SendEmail(model.TemporaryEmailAddress, null, content, model.BandName + ", your stage plan is ready!");
            }

            if (model.WillSignUp && !String.IsNullOrEmpty(model.TemporaryEmailAddress))
            {
                var controller = new Controllers.HomeController();
                var subscription = new SubscriptionPreferences(Request.UserHostAddress);
                subscription.EmailAddress = model.TemporaryEmailAddress;
                subscription.Name = model.BandName;
                subscription.Send(email);
            }

            var newPlan = new Bll.NewPlan();
            newPlan.New();

            return Json(new { url = url, error = "" });
        }

        [HttpPost]
        public ActionResult Plan(Models.PlanViewModel model)
        {

            //we can't return View(model) because we'd have to send all the objects again, such as Instrument.AllInstruments, whcih is hassle

            var captch = new Bll.Captcha();
            if (!captch.IsValid(model.Contact))
            {
                return RedirectToAction("ThankYou", "Home", new { header = "Oh dear...we didn't send your stage plan!", msg1 = "Sadly the captcha wasn't correct, please click back in your browser and try again" });

            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ThankYou", "Home", new { header = "Oh dear...we didn't send your stage plan!", msg1 = "Sadly the details you provided were either missing or incorrect. Please click back in your browser and try again" });

            }

            if (!String.IsNullOrEmpty(model.Contact.Band))
                model.Contact.Send(model.Contact.Band + " has shared their stage plot");
            else
                model.Contact.Send(model.Contact.Name + " has shared their stage plot");


            if (!model.Contact.DidSend)
            {
                Utilies.Email.SendFaultEmail("dave@lmsites.co.uk", "fault with Stage plan", false, "Failed to send to venue/engineer. sp23U39");

                return RedirectToAction("ThankYou", "Home", new { header = "Oh dear...we didn't send your stage plan!", msg1 = "Sadly we were not able to send the email - please confirm the email you've provided is correct - if it is, then there is a fault with our website, so we've already let the web team know and they are busy trying to fix it! If you can email us via our contact page to tell us what made it go wrong it may help us to fix it for you." });

            }


            //    //var email = "support@stage-plan.com";
            //var x = Utilies.Email.SendEmail(false, "mail.stage-plan.com", false, 25, email, GetPassword(), email, new List<string> { model.Email }, null, new List<string> { email }, "Email from Stage-Plan", true, GetBody(model));
            ////todo verification
            return RedirectToAction("ThankYou", "Home", new { header = "Hurray...we've sent your stage plan!", msg1 = "Your plan has been sent but we do suggest you check (some times, emails don't go through or can be blocked as spam)." });

        }



        private string GetNewStagePlanAddedNotificationForDaveAndTelis(string url, string bandName)
        {
            var sb = new StringBuilder();
            sb.Append("<p>Some one saved a new stage plan</p>");
            sb.Append("<p>Band name: " + bandName + "</p>");
            sb.Append("<p>"+url+"</p>");
            sb.Append("<p>EOM</p>");

            return sb.ToString();
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

    }
}