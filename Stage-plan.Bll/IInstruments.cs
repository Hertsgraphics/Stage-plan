using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public interface IInstruments
    {
        List<Instrument> AllInstruments { get; }
        string BandName { get; }
        decimal Width { get; }
        decimal Height { get; }
    }
}
