using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class SellerController : Controller
    {
        // GET: Admin/Seller
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
                var item = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == nvSession.MSNV && row.IdChucNang == 4);
                if (item == null)
                {
                    return RedirectToAction("NonRole", "HomePage");
                }
                else
                {

                    return View();
                }
            }
        }


        public ActionResult Partail_Seller(int? page)
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
            return PartialView(items);
        }

        public ActionResult Partail_ProducDetail(int id)
        {
            var item = db.tb_ProductDetai.Where(x => x.ProductId == id).ToList();
            return PartialView(item);
        }





        public ActionResult Partail_ProductSeller()
        {
            SellerCart cart = (SellerCart)Session["Seller"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }




        [HttpPost]
        public ActionResult Find(int Search)
        {
            if (Search != null)
            {


                var FindProducbyId = db.tb_Products.Where(x => x.ProductId == Search).ToList();

                if (FindProducbyId != null)
                {

                    ViewBag.Find = Search;
                    return View(FindProducbyId);
                }
                else
                { ViewBag.Find = +Search;
                    return View();
                }
            }
            return View();
        }


        [HttpPost]
        public ActionResult AddListProduct(int id, int soluong)
        {
            var code = new { Success = false, Code = -1, Count = 0 };
            var checkSanPham = db.tb_ProductDetai.FirstOrDefault(row => row.ProductDetai == id);
            if (checkSanPham != null)
            {

                if (checkSanPham.Quantity >= soluong)
                {
                    SellerCart cart = (SellerCart)Session["Seller"];
                    if (cart == null)
                    {
                        cart = new SellerCart();
                    }
                    SellerCartItem item = new SellerCartItem
                    {
                        ProductId = checkSanPham.ProductDetai,
                        ProductName = checkSanPham.tb_Products.Title,
                        CategoryName = checkSanPham.tb_Products.tb_ProductCategory.Title,
                        Alias = checkSanPham.Alias,
                        SoLuong = soluong,
                        Size= (int)checkSanPham.Size,
                    };
                    if (checkSanPham.tb_Products.tb_ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                    {
                        item.ProductImg = checkSanPham.tb_Products.tb_ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                    }
                    item.Price = (decimal)checkSanPham.tb_Products.Price;
                    if (checkSanPham.tb_Products.PriceSale > 0)
                    {
                        item.Price = (decimal)checkSanPham.tb_Products.PriceSale;
                    }
                    item.PriceTotal = item.SoLuong * item.Price;

                    //checkSanPham.Quantity = -soluong;
                    cart.AddToCart(item, soluong);
                    Session["Seller"] = cart;
                    code = new { Success = true, Code = 1, Count = cart.Items.Count };

                }
                else
                {
                    code = new { Success = false, Code = -1, Count = 0 };//Số Lượng Không Đủ
                }

            }
            return Json(code);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, Code = -1, Count = 0 };

            SellerCart cart = (SellerCart)Session["Seller"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, Code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }


        [HttpPost]
        public ActionResult FindClient(string Search = "")
        {
            if (!string.IsNullOrEmpty(Search))
            {
                var FindSDTClient = db.tb_KhachHang.FirstOrDefault(x => x.SDT.ToUpper().Contains(Search.ToUpper()));
                if (FindSDTClient != null)
                {
                    var FindClient = db.tb_KhachHang.Find(FindSDTClient.IdKhachHang);
                    if (FindClient != null)
                    {
                        ViewBag.Find = FindClient;
                        return View(FindClient);
                    }
                }
                else
                {
                    return View();
                }
            }
            return View();
        }


        public ActionResult Partail_ThongTinKhachHang()
        {
            return PartialView();
        }

        public ActionResult Partial_Item_ThanhToan()
        {
            SellerCart cart = (SellerCart)Session["Seller"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Partial_ThanhToanDaCoTaiKhoan(int id)
        {

            ViewBag.id = id;

            return PartialView();




        }


        [HttpPost]
        public ActionResult UpdateQuanTity(int id, int quantity)
        {
            SellerCart cart = (SellerCart)Session["Seller"];
            if (cart != null && cart.Items.Any())
            {
                cart.UpSoLuong(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }



        public ActionResult CheckOut()
        {
            SellerCart cart = (SellerCart)Session["Seller"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(ThongTinKhachHangBanHangKhongTaiKHoan req)
        {
            tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                SellerCart cart = (SellerCart)Session["Seller"];
                if (cart != null)
                {

                    bool isTransactionSuccessful = false;

                    if (req.Phone != null)
                    {

                        foreach (var item in cart.Items)
                        {
                            var checkQuantityPro = db.tb_ProductDetai.Find(item.ProductId);
                            if (checkQuantityPro != null)
                            {
                                if (checkQuantityPro.Quantity >= item.SoLuong)
                                {
                                    checkQuantityPro.Quantity -= item.SoLuong;



                                    db.Entry(checkQuantityPro).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                    isTransactionSuccessful = true;
                                }
                                else
                                {
                                    code = new { Success = false, Code = -7, Url = "" };//Số lượng sản phẩm hiện không đủ 

                                }
                            }
                        }

                        if (isTransactionSuccessful)
                        {
                            tb_KhachHang khachHang = new tb_KhachHang();
                            khachHang.SDT = req.Phone.Trim();
                            khachHang.TenKhachHang = req.CustomerName.Trim();
                            khachHang.SoLanMua = 1;
                            db.tb_KhachHang.Add(khachHang);
                            db.SaveChanges();



                            tb_Seller seller = new tb_Seller();
                            seller.CustomerName = req.CustomerName;
                            seller.Phone = req.Phone;
                            //seller.Address = req.Address;
                            //seller.Email = req.Email;
                            /*  seller.Status = 1;*///chưa thanh toán / 2/đã thanh toán, 3/Hoàn thành, 4/hủy
                            cart.Items.ForEach(x => seller.tb_SellerDetail.Add(new tb_SellerDetail
                            {
                                ProductDetai = x.ProductId,
                                Quantity = x.SoLuong,
                                Price = x.Price,

                            }));
                            seller.TotalAmount = cart.Items.Sum(x => (x.Price * x.SoLuong));
                            seller.TypePayment = req.TypePayment;
                            seller.CreatedDate = DateTime.Now;
                            seller.ModifiedDate = null;
                            seller.CreatedBy = nvSession.TenNhanVien;
                            Random rd = new Random();
                            seller.Code = "HD" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);

                            db.tb_Seller.Add(seller);
                            db.SaveChanges();


                            cart.ClearCart();
                            code = new { Success = true, Code = 1, Url = "" };//Xuất hóa đơn thành công
                        }
                        else
                        {
                            code = new { Success = false, Code = -7, Url = "" };//Số lượng sản phẩm hiện không đủ 
                        }

                    }

                }
                else 
                {
                    code = new { Success = false, Code = -3, Url = "" }; // Không có sản phẩm
                }
            }

            else
            {
                code = new { Success = false, Code = -2, Url = "" };//Không được để trống
            }
            return Json(code);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOutClient(SellerPay req, tb_Products model)
        {
            tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
            var code = new { Success = false, Code = -1, Url = "" };
            var checkClient = db.tb_KhachHang.FirstOrDefault(row => row.IdKhachHang == req.idKhachHang);
            if (checkClient != null)
            {
                SellerCart cart = (SellerCart)Session["Seller"];
                if (cart != null)
                {


                    bool isTransactionSuccessful = false;



                    foreach (var item in cart.Items)
                    {
                        var checkQuantityPro = db.tb_ProductDetai.Find(item.ProductId);
                        if (checkQuantityPro != null)
                        {
                            if (checkQuantityPro.Quantity >= item.SoLuong)
                            {
                                checkQuantityPro.Quantity -= item.SoLuong;



                                db.Entry(checkQuantityPro).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                isTransactionSuccessful = true;
                            }
                            else
                            {
                                code = new { Success = false, Code = -7, Url = "" };//Số lượng sản phẩm hiện không đủ 

                            }
                        }
                    }


                    if (isTransactionSuccessful)
                    {
                        tb_Seller seller = new tb_Seller();
                        seller.CustomerName = checkClient.TenKhachHang;
                        seller.Phone = checkClient.SDT;

                        cart.Items.ForEach(x => seller.tb_SellerDetail.Add(new tb_SellerDetail
                        {
                            ProductDetai = x.ProductId,
                            Quantity = x.SoLuong,
                            Price = x.Price,

                        }));
                        seller.TotalAmount = cart.Items.Sum(x => (x.Price * x.SoLuong));
                        seller.TypePayment = req.TypePayment;
                        seller.CreatedDate = DateTime.Now;
                        seller.ModifiedDate = null;
                        seller.CreatedBy = nvSession.TenNhanVien;
                        Random rd = new Random();
                        seller.Code = "HD" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);

                        db.tb_Seller.Add(seller);
                        db.SaveChanges();
                        checkClient.SoLanMua += 1;

                        db.Entry(checkClient).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        cart.ClearCart();
                        code = new { Success = true, Code = 1, Url = "" };//Xuất hóa đơn thành công


                    }
                    else 
                    {
                        code = new { Success = false, Code = -7, Url = "" };//Số lượng sản phẩm hiện không đủ 
                    }

                   

                }
                else
                {
                    code = new { Success = false, Code = -3, Url = "" }; // Không có sản phẩm
                }
            }
            else
            {
                code = new { Success = false, Code = -2, Url = "" }; // Lỗi không tìm thấy khách hàng
            }
            return Json(code);

        }

        ////////Huy hoa don
        public ActionResult Bill()
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {

                tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                var item = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == nvSession.MSNV && row.IdChucNang == 4);
                if (item == null)
                {
                    return RedirectToAction("NonRole", "HomePage");
                }
                else
                {

                    Seller model = new Seller();

                    return View(model);
                }
            }

        }

        [HttpGet]
        public ActionResult Partial_GetBill(string search, int? page)
        {
            IEnumerable<tb_Seller> items = db.tb_Seller.OrderByDescending(x => x.SellerId);
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return PartialView(items);
        }

        [HttpGet]
        public ActionResult Partial_GetBillToDay(string search, int? page)
        {
            DateTime today = DateTime.Today;
            DateTime startOfDay = today.Date;
            DateTime endOfDay = today.Date.AddDays(1).AddTicks(-1);
            IEnumerable<tb_Seller> items = db.tb_Seller.Where(row => row.CreatedDate >= startOfDay && row.CreatedDate <= endOfDay).OrderByDescending(x => x.SellerId);
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return PartialView(items);

        }




        [HttpPost]
        public ActionResult FindBill(string Search = "")
        {
            if (!string.IsNullOrEmpty(Search))
            {
                var FindBill = db.tb_Seller.Where(x => x.Code.ToUpper().Trim().Contains(Search.ToUpper().Trim()));
                ViewBag.Find = Search;
                return View(FindBill.ToList());
            }
            return View("Partial_CheckBill");
        }

        [HttpGet]
        public ActionResult Partial_CheckBill()
        {

            return PartialView();
        }





        public ActionResult DetailSeller(int id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {
                var item = db.tb_Seller.Find(id);
                return View(item);
            }
        }

        public ActionResult Partail_DetailSeller(int id)
        {
            var item = db.tb_SellerDetail.Where(row => row.SellerId == id).ToList();
            return PartialView(item);
        }



        [HttpPost]
        public ActionResult SanPhamTra(List<int> productIds, int id, tb_SellerDetail model)
        {
            var code = new { Success = false, msg = "", code = -1 };
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {

                if (productIds != null && productIds.Any())
                {
                    foreach (var IdSellerDetail in productIds)
                    {
                        var checkSellerItem = db.tb_SellerDetail.SingleOrDefault(x => x.Id == IdSellerDetail && x.SellerId == id);
                        if (checkSellerItem != null)
                        {

                            var updateProducts = db.tb_ProductDetai.FirstOrDefault(x => x.ProductDetai == checkSellerItem.ProductDetai);
                            if (updateProducts != null)
                            {
                                db.tb_SellerDetail.Remove(checkSellerItem);
                                db.SaveChanges();

                                updateProducts.Quantity += checkSellerItem.Quantity;
                                db.Entry(updateProducts).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                code = new { Success = true, msg = "", code = 1 };//Chỉnh sửa thành công

                            }
                            else
                            {
                                code = new { Success = false, msg = "", code = -4 };//Sản phẩm không tồn tại 
                            }


                        }
                        else
                        {
                            code = new { Success = false, msg = "", code = -3 };// Không tồn tại trong hóa đơn
                        }


                    }

                }
                else
                {
                    code = new { Success = false, msg = "", code = -2 };// Vui lòng chọn sản phẩm trả
                }
            }

            return Json(code);
        }




        [HttpPost]
        public ActionResult DeleteSeller(int id)
        {
            var code = new { Success = false, msg = "", code = -1 };
            if (id != null)
            {
                var checkSeller = db.tb_Seller.Find(id);
                if (checkSeller != null)
                {

                }
                else
                {
                    code = new { Success = false, msg = "", code = -3 };// Không tìm thấy đơn hàng trong hệ thống
                }
            }
            else
            {
                code = new { Success = false, msg = "", code = -2 };// Không tìm thấy đơn hàng trong hệ thống
            }


            return Json(code);

        }



        public ActionResult CheckOutSuccess()
        {
            return View();
        }







    }
}