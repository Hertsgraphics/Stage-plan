using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public interface IInstrument
    {
        string Text { get; set; }
        string Detail { get; set; }
        string Src { get; set; }
        decimal Left { get; set; }
        decimal Top { get; set; }
        string BandMemberName { get; set; }
        string SelectedInstrument { get; set; }
        bool IsFixedPosition { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        int Zindex { get; set; }
        int RotateAngle { get; set; }
    }
}
