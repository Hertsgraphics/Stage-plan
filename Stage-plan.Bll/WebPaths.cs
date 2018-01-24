using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stage_Plan.Bll
{
    public static class WebPaths
    {
        public static string AddMissingHttp(string url)
        {
            if (String.IsNullOrEmpty(url))
                return url;

            if (url.Contains("http"))
                return url;


            return "http://" + url;
        }

        public static string GetFacebook()
        {
            return "https://www.facebook.com/people/Stageplan-Stageplan/100015404205242";
        }

        /// <summary>
        /// Returns https://stage-plan.com
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            return "https://stage-plan.com";
        }


        /// <summary>
        /// Returns https://stage-plan.com/stageplan/plan/uniqueToken
        /// </summary>
        /// <returns></returns>
        public static string GetPlan(string token)
        {
            return GetDomain() + "/stageplan/plan/" + token;
        }

        /// <summary>
        /// Returns <a href = https://stage-plan.com/stageplan/plan/uniqueToken>https://stage-plan.com/stageplan/plan/uniqueToken</a>
        /// </summary>
        /// <returns></returns>
        public static string GetPlanLink(string token)
        {
            return "<a href = \"" + GetPlan(token) + "\">" + GetPlan(token) + "</a>";
        }
    }
}
