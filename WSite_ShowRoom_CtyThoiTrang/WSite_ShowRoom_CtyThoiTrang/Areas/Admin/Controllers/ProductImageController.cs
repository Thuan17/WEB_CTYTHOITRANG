using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        // GET: Admin/ProductImage
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities(); 
        public ActionResult Index(int id )
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                View().ViewBag.ProductId = id;
                var item = db.tb_ProductImage.Where(x => x.ProductId == id).OrderByDescending(x => x.ProductId).ToList();
                return View(item);
            }
           
        }
        [HttpPost]
        public ActionResult AddImg(int proId, string url) 
        {
            db.tb_ProductImage.Add(new tb_ProductImage {
                ProductId = proId,  
                Image=url,
                IsDefault = false
            });
            db.SaveChanges();
            return Json(new { Success = true });
        }
        [HttpPost]
        public ActionResult DeleteImg(int id) 
        {
            var item = db.tb_ProductImage.Find(id);
            db.tb_ProductImage.Remove(item);    
            db.SaveChanges();
            return Json(new { Success = true });   
        }
    }
}