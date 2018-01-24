using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stage_Plan.Bll
{
    public static class TextHelper
    {
        public static string Format(string s)
        {
            //result = HttpUtility.HtmlEncode(result);
           // result = HttpUtility.HtmlDecode(result);
            //result = Uri.EscapeDataString(result);
           // result = HttpUtility.UrlEncode(result);
            //result = HttpUtility.UrlDecode(result);
            s = s.Replace("'", "");
            s = s.Replace("\"", "");
            s = s.Replace("<script", " error ");
            s = s.Replace("\n", "<br/>");// must be last as we remove < and >

            return s;
        }

        public static IHtmlString Format(IHtmlString hs)
        {
            var s = Convert.ToString(hs);
            return new HtmlString(Format(s));
        }
    }
}