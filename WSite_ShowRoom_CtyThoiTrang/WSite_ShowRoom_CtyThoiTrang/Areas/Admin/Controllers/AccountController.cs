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
    public class AccountController : Controller
    {
        // GET: Admin/Login
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
                    var nhanVien = db.tb_NhanVien.ToList();
                    return View(nhanVien);
                }
            }


        }

        public ActionResult Error()
        {

            return View();
        }


        public ActionResult DangNhap()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string msnv, string password, string user)
        {

            if (ModelState.IsValid)
            {
                var f_password = MaHoaPass(password);

                //var data = db.tb_NhanVien.Count(s => s.MSNV.Equals(msnv) && s.Password.Equals(f_password));
                var data = db.tb_NhanVien.SingleOrDefault(s => s.MSNV.Equals(msnv) && s.Password.Equals(f_password));

                if (data != null)
                {
                    var checklock = db.tb_NhanVien.FirstOrDefault(s => s.MSNV.Equals(msnv) && s.Clock == false);
                    if (checklock != null)
                    {
                        var checkRole = db.tb_PhanQuyen.SingleOrDefault(s => s.MSNV == msnv);
                        if (checkRole != null)
                        {
                            if (checkRole.IdChucNang == 1 || checkRole.IdChucNang == 2)
                            {
                                Session["user"] = data;
                                return RedirectToAction("Index", "HomePage");
                            }
                            else if (checkRole.IdChucNang == 3)
                            {
                                Session["user"] = data;
                                return RedirectToAction("Index", "Warehouse");
                            }
                            else if (checkRole.IdChucNang == 4)
                            {
                                Session["user"] = data;
                                return RedirectToAction("Index", "Seller");
                            }
                        }
                        else
                        {
                            ViewBag.error = "Không tồn tại tài khoản";
                        }
                    }
                    else 
                    {
                        ViewBag.error = "Tài khoản đã khóa";
                    }
                   
                }

                else
                {
                    ViewBag.error = "Không tồn tại tài khoản";
                }
            }

            return View();
        }

      


        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap");
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
                    var NhanVien = db.tb_NhanVien.ToList();
                    return View(NhanVien);
                }

              
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tb_NhanVien _nhanvien)
        {
            if (ModelState.IsValid) 
            {
                var checkMail = db.tb_NhanVien.FirstOrDefault(row => row.Email == _nhanvien.Email);
                var checkPhone = db.tb_NhanVien.FirstOrDefault(row => row.SDT == _nhanvien.SDT);
                string pass = "";
                if (checkMail == null) 
                {

                    if (checkPhone == null)
                    {

                        Random ran = new Random();
                        _nhanvien.MSNV ="2"+ ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9);

                        _nhanvien.Password = MaHoaPass(_nhanvien.Password);
                        _nhanvien.NgayVaoLam = DateTime.Now;
                        db.Configuration.ValidateOnSaveEnabled = false;

                        db.tb_NhanVien.Add(_nhanvien);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else 
                    {
                        ViewBag.error = "Số điện thoại đã tồn tại";
                    }
                }
            }


            return PartialView();
        }



        public ActionResult Logout() 
        { 
            Session.Clear();

            return RedirectToAction("DangNhap", "Account");
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




    }
}