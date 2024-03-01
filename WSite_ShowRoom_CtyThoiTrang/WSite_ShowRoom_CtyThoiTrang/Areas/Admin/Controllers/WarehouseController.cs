using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class WarehouseController : Controller
    {
        // GET: Admin/Warehouse

        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();
        public ActionResult Index()
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


      

      


        public ActionResult Partail_CheckCode(int id)
        {
            var checkcode = db.tb_Order.FirstOrDefault(row => row.OrderId == id);
            if (checkcode != null)
            {
                return PartialView(checkcode);
            }
            return PartialView();
        }

        public ActionResult Partail_WareHouseById(int id)
        {
            var checkWareHouse = db.tb_Kho.Find(id);
            if (checkWareHouse != null)
            {
                return PartialView(checkWareHouse);
            }
            return PartialView();
        }

        public ActionResult Partail_StaffById(string MSNV)
        {
            var Staff = db.tb_NhanVien.Find(MSNV);
            if (Staff != null)
            {
                return PartialView(Staff);
            }
            return PartialView();
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

        public ActionResult Add(tb_Kho model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                db.tb_Kho.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");

            }
            return View();
        }




        //Nhap kho
        public ActionResult ImportWareHouse() 
        {
            return View();
        }




        public ActionResult Partia_ImportWareHouse()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                ViewBag.Kho = new SelectList(db.tb_Kho.ToList(), "IdKho", "DiaChi");
                return PartialView();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Partia_ImportWareHouse(tb_KhoNhap model)
        {
            if (ModelState.IsValid)
            {
                tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                var item = db.tb_NhanVien.SingleOrDefault(row => row.MSNV == nvSession.MSNV);
                model.ImportBy = item.TenNhanVien;
                model.MSNV = nvSession.MSNV;
                model.ImportDate = DateTime.Now;
                db.tb_KhoNhap.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            ViewBag.Kho = new SelectList(db.tb_Kho.ToList(), "IdKho", "DiaChi");
            return PartialView();
        }


        public ActionResult Test() 
        {
            return PartialView();
        }





        ////////Xuat Kho


        public ActionResult Partail_ExportToday()
        {

            DateTime today = DateTime.Today;
            DateTime startOfDay = today.Date;
            DateTime endOfDay = today.Date.AddDays(1).AddTicks(-1);

            var exportToDay = db.tb_KhoXuat.Where(row => row.OutDate >= startOfDay && row.OutDate <= endOfDay).OrderByDescending(x => x.IdKhoXuat).ToList();
            if (exportToDay != null)
            {
                return PartialView(exportToDay);
            }
            return PartialView();
        }
        public ActionResult ExportWareHouse()
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


        public ActionResult Partial_ThongTinXuat() 
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {

                ViewBag.Kho = new SelectList(db.tb_Kho.ToList(), "IdKho", "DiaChi");
                return PartialView();
            }
        }


      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Partial_ExportWareHouse(Admin_WareHouse_Export_ToKen rep  )
        {
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid) 
            {

                string mDH = "DH" + rep.Code.Trim();
                var checkOrder = db.tb_Order.FirstOrDefault(x => x.Code == mDH.Trim());
                if (checkOrder != null) 
                {
                    var checkCancelOrder=db.tb_Order.FirstOrDefault(x=>x.OrderId== checkOrder.OrderId && x.typeOrder == false );
                    if (checkCancelOrder != null) 
                    {
                        var checkConfim = db.tb_Order.FirstOrDefault(x => x.OrderId == checkCancelOrder.OrderId && x.Confirm == true);
                        if (checkConfim != null)
                        {
                            var OrderReturn=db.tb_Order.FirstOrDefault(x => x.OrderId == checkConfim.OrderId && x.typeReturn==false);
                            if (OrderReturn != null)
                            {
                                var checkTBOut = db.tb_KhoXuat.FirstOrDefault(x => x.OrderId == OrderReturn.OrderId);
                                if (checkTBOut == null)
                                {
                                    tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                                    var item = db.tb_NhanVien.SingleOrDefault(row => row.MSNV == nvSession.MSNV);
                                    tb_KhoXuat model = new tb_KhoXuat();
                                    model.OutDate = DateTime.Now;
                                    model.OutBy = item.TenNhanVien;
                                    model.OrderId = checkOrder.OrderId;
                                    model.Idkho = rep.Idkho;
                                    model.MSNV = nvSession.MSNV;
                                    db.tb_KhoXuat.Add(model);
                                    db.SaveChanges();
                                    code = new { Success = true, Code = 1, Url = "" };
                                }
                                else
                                {
                                    code = new { Success = false, Code = -6, Url = "" };
                                }
                            }
                            else 
                            {
                                code = new { Success = false, Code = -5, Url = "" };
                            }

                            
                        }
                        else 
                        {
                            code = new { Success = false, Code = -4, Url = "" };
                        }
                        
                    }
                    else
                    {
                        //Don Hang da bi huy
                        code = new { Success = false, Code = -3, Url = "" };
                    }
                }
                else 
                {
                    //Khong thay ma Order
                   code = new { Success = false, Code = -2, Url = "" };
                }
            }
            return Json(code);
            
        }



        //Hàng Return 
        public ActionResult Return()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var OrderReturn = db.tb_Order.Where(x => x.typeReturn == true);
                return View(OrderReturn);
            }
        }



        


        [HttpPost]
        public ActionResult FindReturn(string Search = "") 
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                if (!string.IsNullOrEmpty(Search))
                {
                    var FindProduc = db.tb_Return.FirstOrDefault(x => x.Code.ToUpper().Contains(Search.ToUpper()));
                    if (FindProduc != null) 
                    {
                        ViewBag.Search = Search;
                        return View(FindProduc);
                    }
                    else 
                    {
                        return View();
                    }
                }
                return View();
                
            }
               
        }



        public ActionResult OrderById(int id)
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



        //public ActionResult Partial_ThongTinKhoReturn()
        //{
        //    if (Session["user"] == null)
        //    {
        //        return RedirectToAction("DangNhap", "Account");
        //    }
        //    else
        //    {

        //        ViewBag.Kho = new SelectList(db.tb_Kho.ToList(), "IdKho", "DiaChi");
        //        return PartialView();
        //    }
        //}





        [HttpPost]
       
        public ActionResult ImportReturn(List<int> ListId ,int OrderId)
        {
            var code = new { Success = false, msg = "", code = -1 };
            if (ListId != null && ListId.Any())
            {
              var checkReturn =db.tb_Return.FirstOrDefault(x=>x.OrderId==OrderId);
                if (checkReturn != null) 
                {
                    var checkKhoReturn = db.tb_KhoReturn.FirstOrDefault(x => x.ReturnId == checkReturn.ReturnId);
                    if (checkKhoReturn == null) 
                    {
                        foreach (var item in ListId)
                        {
                            var checkOrderDetaill = db.tb_OrderDetail.Find(item);
                            if (checkOrderDetaill != null) 
                            {
                                var FindProduct = db.tb_ProductDetai.Find(checkOrderDetaill.ProductDetai);
                                if (FindProduct != null) 
                                {
                                    checkReturn.Confirm = true;
                                    db.Entry(checkReturn).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();



                                    FindProduct.Quantity += checkOrderDetaill.Quantity;
                                    db.Entry(FindProduct).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();

                                    checkOrderDetaill.damagedProduct = true;

                                    db.Entry(checkOrderDetaill).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();

                                }
                                else
                                {
                                    code = new { Success = false, msg = "", code = -6 }; //Không tồn tài sản phẩm này 
                                }
                            }
                            else 
                            {
                                code = new { Success = false, msg = "", code = -5 }; //Không tồn tài sản phẩm này trong đơn hàng
                            }
                        }



                        var checkKhoXuat = db.tb_KhoXuat.FirstOrDefault(x => x.OrderId == OrderId);
                        if (checkKhoXuat != null) 
                        {
                            tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                            var item = db.tb_NhanVien.SingleOrDefault(row => row.MSNV == nvSession.MSNV);
                            tb_KhoReturn KhoReturn = new tb_KhoReturn();
                            KhoReturn.ReturnDate = DateTime.Now;
                            KhoReturn.IdKho = (int)checkKhoXuat.Idkho;
                            KhoReturn.ReturnId = checkReturn.ReturnId;
                            KhoReturn.MSNV = nvSession.MSNV;
                            KhoReturn.ReturnBy = item.TenNhanVien;
                            db.tb_KhoReturn.Add(KhoReturn);
                            db.SaveChanges();

                            code = new { Success = true, msg = "", code = 1 };
                        }
                        else 
                        {
                            code = new { Success = false, msg = "", code = -4 }; //Không tìm thấy kho xuất hãy kiểm tra lại
                        }
                       
                    }
                    else
                    {
                        code = new { Success = false, msg = "", code = -3 }; //Đơn hàng trả lập lại 2 lần 
                    }
                   
                }
                else 
                {
                    code = new { Success = false, msg = "", code = -2 }; //Không tìm thấy trong bảng yêu cầu Return
                }
            }
            else 
            {
                code = new { Success = false, msg = "", code = -7}; //Khong tim thay danh sach san pham
            }
            return Json(code);
        }






    }
}