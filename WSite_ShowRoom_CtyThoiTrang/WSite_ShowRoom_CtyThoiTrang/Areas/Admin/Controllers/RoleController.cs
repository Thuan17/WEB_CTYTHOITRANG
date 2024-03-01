using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        // GET: Admin/Role
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {

                tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                var check = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == nvSession.MSNV && (row.IdChucNang == 1 || row.IdChucNang == 2));
                if (check == null)
                {
                    return RedirectToAction("NonRole", "HomePage");
                }
                else 
                {
                    var item = db.tb_ChucNang.ToList();
                    return View(item);
                }
                
            }

        }
        public ActionResult Details(int id)
        {
            var checkIdPQuyen = db.tb_ChucNang.Find(id);
            return View(checkIdPQuyen);
        }


        public ActionResult Add()
        {
            if (Session["user"] == null)
            {


                return RedirectToAction("DangNhap", "Account");
            }
            else
            {

                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(tb_ChucNang model)
        {
            if (ModelState.IsValid)
            {
                tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                var item = db.tb_NhanVien.SingleOrDefault(row => row.MSNV == nvSession.MSNV);
                model.Createby = item.TenNhanVien;
                model.CreatedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.MaChucNang))
                {
                    model.MaChucNang = WSite_ShowRoom_CtyThoiTrang.Models.Common.Filter.FilterChar(model.TenChucNang);
                }
                db.tb_ChucNang.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_ChucNang.Find(id);
                return View(item);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_ChucNang model)
        {
            if (ModelState.IsValid)
            {
                tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                var item = db.tb_NhanVien.SingleOrDefault(row => row.MSNV == nvSession.MSNV);
                model.ModifiedDate = DateTime.Now;
                model.Modifeby = item.TenNhanVien;

                model.MaChucNang = WSite_ShowRoom_CtyThoiTrang.Models.Common.Filter.FilterChar(model.TenChucNang);

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        protected void SetAlert(string message, int type)
        {
            TempData["AlertMessage"] = message;
            if (type == 1)
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == 2)
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == 3)
            {
                TempData["AlertType"] = "alert-danger";
            }
            else
            {
                TempData["AlertType"] = "alert-info";
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_ChucNang.Find(id);
                if (item != null)
                {
                    var checkPhanQuyen = db.tb_NhanVien.FirstOrDefault(row => row.IdChucNang == id);
                    if (checkPhanQuyen != null)
                    {
                        db.tb_NhanVien.Attach(checkPhanQuyen);

                        db.tb_ChucNang.Remove(item);
                        db.SaveChanges();
                        return Json(new { success = true });
                    }
                    else
                    {

                    }

                }
                return Json(new { success = false });
            }

        }



        [HttpPost]
        public ActionResult DeleteAll(int? IdChucNang ,List<int> IdNhanVien)
        {
            var code = new { Success = false, msg = "", code = -1 };
            try
            {
              
                foreach (var item in IdNhanVien)
                {
                    var checkNhanVien = db.tb_PhanQuyen.FirstOrDefault(x => x.MSNV == Convert.ToString(item));
                    if (checkNhanVien != null) 
                    {
                        db.tb_PhanQuyen.Remove(checkNhanVien);
                        db.SaveChanges();
                        code = new { Success = true, msg = "Xóa nhân viên khỏi phân quyền", code = 1 };
                    }
                    
                } 

            }
            catch { }
            return Json(code);
        }
        public ActionResult StafIdRole(int  id)
        {
            var checkIdPQuyen = db.tb_PhanQuyen.Where(row => row.IdChucNang==id).ToList();
            return PartialView(checkIdPQuyen);
        }
    }
}