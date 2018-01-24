using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Stage_Plan.Ui
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


//            routes.MapRoute(
//               name: "Stageplan",
//               url: "Stageplan/{action}/{id}",
//               defaults: new { controller = "Stageplan", action = "Index", id = UrlParameter.Optional }
//           );

//            routes.MapRoute(
//             name: "Template",
//             url: "Template/{action}/{id}",
//             defaults: new { controller = "Template", action = "Index", id = UrlParameter.Optional }
//         );



//            routes.MapRoute(
//   name: "Error",
//   url: "Error/{action}/{id}",
//   defaults: new { controller = "Error", action = "Index", id = UrlParameter.Optional }
//);

//            routes.MapRoute(
//              name: "Unicorn",
//              url: "Unicorn/{action}/{id}",
//              defaults: new { controller = "Unicorn", action = "Index", id = UrlParameter.Optional }

//          );

//            routes.MapRoute(
//   name: "Blog",
//   url: "Blog/{action}/{id}",
//   defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
//);



//            routes.MapRoute(
//                name: "Home",
//                url: "{action}/{id}",
//                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
//            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
