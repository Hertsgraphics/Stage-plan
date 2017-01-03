using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage_plan.Bll
{
    public class Instrument : IInstrument
    {
        public string Text { get; set; }
        public string Detail { get; set; }
        public string Src { get; set; }
        public decimal Left { get; set; }
        public decimal Top { get; set; }
    }
}