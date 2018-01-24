using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage_Plan.Bll
{
    public class Instruments : IInstruments, ICaptcha
    {
        public List<Instrument> AllInstruments { get; set; }
        public string BandName { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string CaptchaPath { get; set; }
        public string CaptchaCode { get; set; }
        public string BandWebAddress { get; set; }
        public string BandSocialMediaAddress { get; set; }
        public string BandGenre { get; set; }
        public string Country { get; set; }
        public bool WillShowInRecentBands { get; set; }

        /// <summary>
        ///     use for when customer finishes a stage plan, and when they enter band name, genre, they can also get an email with the link and opportunity to sign up
        /// </summary>
        public string TemporaryEmailAddress { get; set; }
        public bool WillSignUp { get; set; }

      
    }
}