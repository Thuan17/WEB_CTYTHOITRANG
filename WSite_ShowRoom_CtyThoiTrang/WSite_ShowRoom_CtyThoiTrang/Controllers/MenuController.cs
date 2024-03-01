using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Controllers
{
    public class MenuController : Controller
    {
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuProductCategory()
        {
            var item = db.tb_ProductCategory.ToList();
            if (item != null)
            {
                return PartialView("_MenuProductCategory", item);

            }

            return PartialView();
        }

        public ActionResult MenuLeft(int? id)
        {
            if (id != null)
            {
                ViewBag.id = id;
            }
            var item = db.tb_ProductCategory.ToList();
            return PartialView("_MenuLeft", item);
        }

        public ActionResult MenuArrivals() 
        {
            var items = db.tb_ProductCategory.ToList();
            return PartialView("_MenuArrivals", items);
        }



        public ActionResult MenuNavProduct(int? id)
        {
            if (id != null)
            {
                ViewBag.id = id;
            }
            var item = db.tb_ProductCategory.ToList();
            return PartialView("_MenuNavProduct", item);
        }


    }
}