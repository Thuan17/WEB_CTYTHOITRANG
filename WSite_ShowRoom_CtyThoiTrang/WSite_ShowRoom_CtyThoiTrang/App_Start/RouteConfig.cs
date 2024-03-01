using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WSite_ShowRoom_CtyThoiTrang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WSite_ShowRoom_CtyThoiTrang.Controllers" }
            );

            routes.MapRoute(
               name: "Contact",
               url: "Lien-he",
               defaults: new { controller = "Contact", action = "Index" }
           );

            // routes.MapRoute(
            //    name: "detailProduct",
            //    url: "chi-tiet/{id}",
            //    defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
            //    namespaces: new[] { "WSite_ShowRoom_CtyThoiTrang.Controllers" }
            //);
            routes.MapRoute(
              name: "ProductDetail",
              url: "chi-tiet/{alias}-{id}",
              defaults: new { controller = "Product", action = "Detail" },
              constraints: new { id = @"\d+" } // Đảm bảo id chỉ chứa số
          );
            routes.MapRoute(
             name: "Products",
             url: "san-pham",
             defaults: new { controller = "Product", action = "Index", alias = UrlParameter.Optional },
             namespaces: new[] { "WSite_ShowRoom_CtyThoiTrang.Controllers" }
         );

        }
    }
}
