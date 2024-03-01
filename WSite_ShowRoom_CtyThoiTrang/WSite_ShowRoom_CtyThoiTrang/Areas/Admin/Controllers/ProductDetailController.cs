using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: Admin/ProductDetail
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Add(int id) 
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_Products.Find(id);

                return View();
            }
        }
        public ActionResult Partial_AddProductDetail(int id)
        {


            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                ViewBag.Id = id;

                return PartialView();
            }



        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(TokenAddProductsDetail req)
        {
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                if (req.Size > 0)
                {
                    var checkProduct = db.tb_Products.Find(req.ProductId);
                    if (checkProduct != null)
                    {
                        var checkProductDetail = db.tb_ProductDetai.FirstOrDefault(x => x.ProductId == req.ProductId && x.Size == req.Size);
                        if (checkProductDetail == null)
                        {
                            tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                            tb_ProductDetai model = new tb_ProductDetai();
                            model.ProductId = req.ProductId;
                            model.Quantity = req.Quantity;
                            model.Size = req.Size;

                            model.CreatedBy = nvSession.TenNhanVien;
                            model.CreateDate = DateTime.Now;

                            db.tb_ProductDetai.Add(model);
                            db.SaveChanges();
                            code = new { Success = true, Code = 1, Url = "" };//Thêm thành công 
                        }
                        else
                        {
                            code = new { Success = false, Code = -3, Url = "" };//Không tìm thấy mã sản phẩm gốc
                        }


                    }
                    else
                    {
                        code = new { Success = false, Code = -2, Url = "" };//Không tìm thấy mã sản phẩm gốc
                    }
                }
                else
                {
                    code = new { Success = false, Code = -4, Url = "" };//Không tìm thấy mã sản phẩm gốc
                }




            }
            return Json(code);
        }





        public ActionResult Edit(int id)
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
                    ViewBag.ProductCategory = new SelectList(db.tb_Products.ToList(), "ProductId", "Title");
                    var SanPham = db.tb_ProductDetai.Find(id);
                    return View(SanPham);
                }
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_ProductDetai model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                
                db.tb_ProductDetai.Add(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Product");
            }
            return View(model);
        }
    }
}