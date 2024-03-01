using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();
        public ActionResult Index()
        {
           
            return View(); 
        }
        public ActionResult dieukien()
        {

            return View();
        }
        public ActionResult doitra()
        {

            return View();
        }
        public ActionResult ThanhToan()
        {

            return View();
        }
        public ActionResult Khuyenmai()
        {

            return View();
        }
       


    }
}