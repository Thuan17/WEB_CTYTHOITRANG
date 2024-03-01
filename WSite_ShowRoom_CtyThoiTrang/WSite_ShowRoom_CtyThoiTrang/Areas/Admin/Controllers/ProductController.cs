using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product



        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();



        public ActionResult Index(int? page)
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
                    IEnumerable<tb_Products> items = db.tb_Products.OrderByDescending(x => x.ProductId);
                    var pageSize = 10;
                    if (page == null)
                    {
                        page = 1;
                    }
                    var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
                    items = items.ToPagedList(pageIndex, pageSize);
                    ViewBag.PageSize = pageSize;
                    ViewBag.Page = page;
                    return View(items);
                }
            }
        }
        public ActionResult Detail(int id)
        {
            var item = db.tb_Products.Find(id);
            return View(item);
        }

        public ActionResult Partail_ProductDetailForDetail(int id) 
        {
            var item = db.tb_ProductDetai.Where(x => x.ProductId == id);
            return PartialView(item);
        }






        //////////////////////////////////////////////////////////////Da Xong
        public ActionResult Add()
        {
            //Load ProductCategory
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
                    ViewBag.ProductCategory = new SelectList(db.tb_ProductCategory.ToList(), "ProductCategoryId", "Title");
                    return View();
                }

            }



        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(tb_Products model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            db.tb_ProductImage.Add(new tb_ProductImage
                            {
                                ProductId = model.ProductId,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }
                        else
                        {
                            db.tb_ProductImage.Add(new tb_ProductImage {
                                ProductId = model.ProductId,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }
                    }
                }
                tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.IsHome = false;
                model.IsFeature = false;
                model.IsActive = false;
                model.IsSale = false;
                model.CreatedBy = nvSession.TenNhanVien; 
                if (string.IsNullOrEmpty(model.Title))
                {
                    model.SeoTitle = model.Title;
                }
                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = WSite_ShowRoom_CtyThoiTrang.Models.Common.Filter.FilterChar(model.Title);
                }
                db.tb_Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.ProductCategory = new SelectList(db.tb_ProductCategory.ToList(), "ProductCategoryId", "Title");
            return View();
        }

        //public ActionResult DetailProduct(int id)
        //{
        //    var item = db.tb_ProductDetai.Find(id);
        //    return View(item);
        //}

        public ActionResult Partail_ProductDetail(int id) 
        {
            ViewBag.Id = id;
            var item = db.tb_ProductDetai.Where(x => x.ProductId == id).ToList();
            if (item != null)
            {
                ViewBag.Title = "San Pham";
                return PartialView(item);
            }
            else 
            {
                return PartialView();
            }
           
           
        }




        public ActionResult AddProductDetail(int id)
        {


            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_Products.Find(id);
                return View(item);
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
        public ActionResult AddProductDetail(TokenAddProductsDetail req)
        {
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                if (req.Size > 0)
                {
                    var checkProduct = db.tb_Products.Find(req.ProductId);
                    if (checkProduct != null)
                    {
                        var checkProductDetail = db.tb_ProductDetai.FirstOrDefault(x=>x.ProductId==req.ProductId&&x.Size==req.Size);
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





        [HttpPost]
        public ActionResult Delete(int id)
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_Products.Find(id);

                if (item != null)
                {
                    //var checkImg = item.tb_ProductImage.Where(x => x.ProductId == item.ProductId);

                    //if (checkImg != null)
                    //{
                    //    foreach (var img in checkImg)
                    //    {
                    //        db.tb_ProductImage.Remove(img);
                    //        db.SaveChanges();
                    //    }
                    //}
                    db.tb_Products.Remove(item);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }

        }
        

        //////////////////////////////////////////////////////////////

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
                    ViewBag.ProductCategory = new SelectList(db.tb_ProductCategory.ToList(), "ProductCategoryId", "Title");
                    var SanPham = db.tb_Products.Find(id);
                    return View(SanPham);
                }
            }

           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_Products model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WSite_ShowRoom_CtyThoiTrang.Models.Common.Filter.FilterChar(model.Title);
                db.tb_Products.Add(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }


        //////////////////////////////////////////////////////////////Da Xong
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.tb_Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isAcive = item.IsActive });
            }

            return Json(new { success = false });
        }
        //////////////////////////////////////////////////////////////Da Xong
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = db.tb_Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsHome = item.IsHome });
            }

            return Json(new { success = false });
        }
        //////////////////////////////////////////////////////////////Da Xong

        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = db.tb_Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, IsSale = item.IsSale });
            }

            return Json(new { success = false });
        }



        [HttpPost]
        public ActionResult DeleteImg(int id) 
        {
            var item = db.tb_ProductImage.Find(id);
           
            if (item != null)
            {
                    db.tb_ProductImage.Remove(item);
                    db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}