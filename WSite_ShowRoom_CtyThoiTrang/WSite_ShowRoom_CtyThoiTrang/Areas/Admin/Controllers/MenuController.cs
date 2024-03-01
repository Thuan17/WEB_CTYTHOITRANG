using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuSideBar() 
        {
            return PartialView("_MenuSideBar");
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
    }
}