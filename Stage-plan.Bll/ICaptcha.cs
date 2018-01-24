using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public interface ICaptcha
    {
        string CaptchaPath { get; }
        string CaptchaCode { get; }
    }
}
