using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/DonHang
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();

        public ActionResult Index(int? page)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                //var item = db.tb_Order.ToList();
                var item = db.tb_Order.OrderByDescending(x => x.OrderId).ToList();
                if (page == null)
                {
                    page = 1;
                }
                if (page == null)
                {
                    page = 1;
                }
                var pageNumber = page ?? 1;
                var pageSize = 10;
                ViewBag.PageSize = pageSize;
                ViewBag.Page = pageNumber;
                return View(item);
            }

        }


        public ActionResult OrderNew(int? page)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                //var item = db.tb_Order.ToList();
                var item = db.tb_Order.Where(row => row.Confirm == false).OrderByDescending(x => x.OrderId).ToList();
                if (page == null)
                {
                    page = 1;
                }
                if (page == null)
                {
                    page = 1;
                }
                var pageNumber = page ?? 1;
                var pageSize = 10;
                ViewBag.PageSize = pageSize;
                ViewBag.Page = pageNumber;
                return View(item);
            }

        }

        [HttpPost]
        public ActionResult IsComfrim(int id)
        {
            var item = db.tb_Order.Find(id);
            if (item != null)
            {
                item.Confirm = !item.Confirm;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, Confirm = item.Confirm });
            }

            return Json(new { success = false });
        }



        public ActionResult CountUnConfimred()
        {
            var item = db.tb_Order.Count(row => row.Confirm.HasValue);
            return PartialView(item);
        }


        public ActionResult Detail(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_Order.Find(id);
                return View(item);
            }

        }


        public ActionResult Detail_SanPham(int id)
        {
            var item = db.tb_OrderDetail.Where(row => row.OrderId == id).ToList();
            return PartialView(item);
        }



        public ActionResult ordertoday() 
        {
            DateTime today = DateTime.Today;
            DateTime startOfDay = today.Date;
            DateTime endOfDay = today.Date.AddDays(1).AddTicks(-1);

            var exportToDay = db.tb_KhoXuat.Where(row => row.OutDate >= startOfDay && row.OutDate <= endOfDay).OrderByDescending(x => x.IdKhoXuat).ToList();
            if (exportToDay != null)
            {
                return View(exportToDay);
            }
            return View();

        }
    }
}