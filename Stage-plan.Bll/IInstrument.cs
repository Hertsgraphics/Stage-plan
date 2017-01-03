using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_plan.Bll
{
    public interface IInstrument
    {
        string Text { get; set; }
        string Detail { get; set; }
        string Src { get; set; }
        decimal Left { get; set; }
        decimal Top { get; set; }
    }
}
