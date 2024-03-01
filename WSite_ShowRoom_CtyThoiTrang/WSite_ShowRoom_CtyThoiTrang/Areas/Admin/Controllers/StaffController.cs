using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        // GET: Admin/Staff
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
                var item = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == nvSession.MSNV && (row.IdChucNang == 1 || row.IdChucNang == 2));
                if (item == null)
                {
                    return RedirectToAction("NonRole", "HomePage");
                }
                else
                {
                    var items = db.tb_NhanVien.OrderByDescending(x => x.MSNV).ToList();
                    return View(items);
                }
            }

        }

        [HttpPost]
        public ActionResult IsLock(string Msnv)
        {

            var item = db.tb_NhanVien.SingleOrDefault(x=>x.MSNV==Msnv);
            if (item != null)
            {
                item.Clock = !item.Clock;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.Clock });
            }

            return Json(new { success = false });
        }

        public ActionResult Details(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_NhanVien.Find(id);
                return View(item);
            }

        }
        public ActionResult Partial_AddStaff()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {

                tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                var item = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == nvSession.MSNV && (row.IdChucNang == 1 || row.IdChucNang == 2));
                if (item == null)
                {
                    return RedirectToAction("NonRole", "HomePage");
                }
                else
                {
                    Random ran = new Random();
                    ViewBag.MSNV = "2" + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9);


                    ViewBag.ChucNang = new SelectList(db.tb_ChucNang.ToList(), "IdChucNang", "TenChucNang");
                    return PartialView();
                }
            }

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
        public ActionResult Add(tb_NhanVien model, tb_PhanQuyen modelPhanQuyen, Admin_Add_Staff_ToKen req)
        {
            var code = new { Success = false, Code = -1, Url = "" };
            var checkMail = db.tb_NhanVien.FirstOrDefault(row => row.Email == model.Email);
            var checkPhone = db.tb_NhanVien.FirstOrDefault(row => row.SDT == model.SDT);
            if (checkMail == null)
            {
                if (checkPhone == null)
                {
                    tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                    var item = db.tb_NhanVien.SingleOrDefault(x => x.MSNV == nvSession.MSNV);
                    if (item != null)
                    {
                        if (req.IdChucNang !=null)
                        {
                            string pass = "123";


                            Random ran = new Random();

                            string msnv = "2" + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9);
                            model.MSNV = msnv.Trim();
                            model.TenNhanVien = req.Name.Trim();
                            model.SDT = req.SDT;
                            model.CCCD = req.CCCD;  
                            model.Email = req.Email;
                            model.Birthday = req.Birthday;  
                            model.DiaChi = req.DiaChi;  
                            model.Luong = req.Luong;    



                            model.Password = MaHoaPass(pass);
                            model.CreatedDate = DateTime.Now;
                            model.NgayVaoLam = DateTime.Now;


                            modelPhanQuyen.MSNV = msnv;
                            modelPhanQuyen.IdChucNang = (int)req.IdChucNang;

                            db.tb_PhanQuyen.Add(modelPhanQuyen);
                            db.tb_NhanVien.Add(model);
                            db.SaveChanges();
                            code = new { Success = true, Code = 1, Url = "" };
                        }
                        else
                        {
                           //"Không thấy chức năng cho nhân viên mới";
                            code = new { Success = false, Code = -4, Url = "" };
                        }
                    }
                    else
                    { ViewBag.error = "Không thấy Session nhân viên hiện tại"; }
                }
                else
                {
                    // "Số điện thoại đã tồn tại";
                    code = new { Success = false, Code = -3, Url = "" };
                }

            }
            else
            {
                //"Email đã tồn tại";
                code = new { Success = false, Code = -2, Url = "" };
            }

            ViewBag.ChucNang = new SelectList(db.tb_ChucNang.ToList(), "IdChucNang", "TenChucNang");
            return Json(code);
        }

        public ActionResult Edit(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                ViewBag.ChucNang = new SelectList(db.tb_ChucNang.ToList(), "IdChucNang", "TenChucNang");
                var item = db.tb_NhanVien.Find(id);
                return View(item);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_NhanVien model, tb_PhanQuyen modelPhanQuyen, int? IdChucNang, int? MSNV)
        {
            if (ModelState.IsValid)
            {
                if (IdChucNang.HasValue)
                {
                    string msnv = Convert.ToString(MSNV);
                    var checkNhanVien = db.tb_PhanQuyen.SingleOrDefault(x => x.MSNV == msnv);
                    if (checkNhanVien != null)
                    {
                        model.ModifiedDate = DateTime.Now;
                        db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        modelPhanQuyen.IdChucNang = (int)IdChucNang;

                        db.SaveChanges();
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ViewBag.error = "lỗi thay đổi nhân viên";
                    }


                }
                else { ViewBag.error = "Không thấy chức năng cho nhân viên mới"; }

            }
            return View(model);
        }




        public ActionResult Delete(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_NhanVien.Find(id);
                if (item != null)
                {
                    var checkPhanQuyen = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == id);
                    db.tb_PhanQuyen.Remove(checkPhanQuyen);
                    db.tb_NhanVien.Remove(item);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
        }

        public ActionResult Parti_RoleTheMSNV(string id)
        {
            var checkNhanVien = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == id);
            if (checkNhanVien != null)
            {
                var item = db.tb_ChucNang.Find(checkNhanVien.IdChucNang);
                return PartialView(item);
            }
            return PartialView();
        }





        public static string MaHoaPass(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;

        }




        public ActionResult Partial_RoleInId(string id) 
        {
            var checkStaff = db.tb_PhanQuyen.FirstOrDefault(x => x.MSNV == id);
            if (checkStaff != null) 
            {
                var checkChucNang=db.tb_ChucNang.FirstOrDefault(x=>x.IdChucNang==checkStaff.IdChucNang);
                if (checkChucNang != null) 
                {
                    return PartialView(checkChucNang);
                }

            }
            return PartialView();
        }



    }
}