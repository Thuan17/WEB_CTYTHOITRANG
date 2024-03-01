using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSite_ShowRoom_CtyThoiTrang.Models;

namespace WSite_ShowRoom_CtyThoiTrang.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();
        public ActionResult Index()
        {
            if (Session["IdKhachHang"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }


        public ActionResult Partail_AllOrder()
        {
            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];
                var checkOrder = db.tb_Order.Where(x => x.IdKhachHang == idKhach).OrderByDescending(x => x.OrderId).ToList();
                if (checkOrder != null)
                {

                    return PartialView(checkOrder);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();

        }








        public ActionResult WaitPayOrder()
        {
            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];
                var cheCheckORder = db.tb_Order.Where(x => x.IdKhachHang == idKhach && x.TypePayment == 1).OrderByDescending(x => x.OrderId).ToList();
                if (cheCheckORder != null)
                {
                    return PartialView(cheCheckORder);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();

        }

        public ActionResult Partail_TrangThaiDonHang(int id)
        {
            int idKhach = (int)Session["IdKhachHang"];
            var cheCheckORderDetail = db.tb_Order.Find(id);
            if (cheCheckORderDetail != null)
            {
                var checkOutOrder = db.tb_KhoXuat.FirstOrDefault(x => x.OrderId == cheCheckORderDetail.OrderId);
                if (checkOutOrder != null)
                {
                    ViewBag.Out = "XuatKho";
                    return PartialView(cheCheckORderDetail);
                }
                else
                {
                    return PartialView(cheCheckORderDetail);

                }

            }
            return PartialView();
        }

       

        public ActionResult Partial_OrderCanceled()
        {

            int idKhach = (int)Session["IdKhachHang"];
            var checkORder = db.tb_Order.Where(x => x.IdKhachHang == idKhach && x.typeOrder == true).OrderByDescending(x => x.OrderId).ToList();
            if (checkORder != null)
            {
                return PartialView(checkORder);
            }
            return PartialView();
        }



        public ActionResult Partial_OrderReturn()
        {
            int idKhach = (int)Session["IdKhachHang"];
            var checkORder = db.tb_Order.Where(x => x.IdKhachHang == idKhach && x.typeReturn == true).OrderByDescending(x => x.OrderId).ToList();
            if (checkORder != null)
            {
                return PartialView(checkORder);
            }
            return PartialView();
        }










        public ActionResult OrderSuccess()
        {
            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];
                var cheCheckORder = db.tb_Order.Where(x => x.IdKhachHang == idKhach && x.Confirm == true).OrderByDescending(x => x.OrderId).ToList();
                if (cheCheckORder != null)
                {
                    return View(cheCheckORder);
                }
                else
                {

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult Par_OrderDetailSuccess(int id)
        {
            int idKhach = (int)Session["IdKhachHang"];
            var cheCheckORderDetail = db.tb_OrderDetail.Where(x => x.OrderId == id).OrderByDescending(x => x.OrderId).ToList();
            if (cheCheckORderDetail != null)
            {
                return PartialView(cheCheckORderDetail);
            }
            return PartialView();
        }

        public ActionResult Par_OrderDetailCancel(int id)
        {
            int idKhach = (int)Session["IdKhachHang"];
            var cheCheckORderDetail = db.tb_OrderDetail.Where(x => x.OrderId == id).OrderByDescending(x => x.OrderId).ToList();
            if (cheCheckORderDetail != null)
            {
                return PartialView(cheCheckORderDetail);
            }
            return PartialView();
        }







        public ActionResult purchaselist()
        {
            Cancel cart = (Cancel)Session["CancelOrder"];
            if (cart != null && cart.Items.Any())
            {

                return View(cart.Items);
            }
            return View();

        }





        public ActionResult Partial_ListCancelOrder()
        {
            Cancel cart = (Cancel)Session["CancelOrder"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }

        public ActionResult Partial_CancelOrder()
        {
            return PartialView();
        }

        ///Hủy Hàng/
        [HttpPost]
        public ActionResult AddListCancel(int id, List<int> productIds)
        {
            var code = new { Success = false, msg = "", code = -1 };
            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];
                var checkIdOrder = db.tb_Order.FirstOrDefault(row => row.IdKhachHang == idKhach);
                if (checkIdOrder != null)
                {

                    int checkId = checkIdOrder.OrderId;

                    Cancel can = (Cancel)Session[""];
                    if (can == null)
                    {
                        can = new Cancel();
                    }
                    foreach (var proId in productIds)
                    {
                        var OrderDetail = db.tb_OrderDetail.FirstOrDefault(row => row.OrderId == id && row.Id == proId);
                        if (OrderDetail != null)
                        {
                            //var check = db.tb_Order.FirstOrDefault(x => x.OrderId == checkId);
                            //int idOrderDetail = check.Id;
                            CanceltItem item = new CanceltItem
                            {
                                OrderId = id,
                                ProductId = (int)OrderDetail.ProductDetai,
                                ProductName = OrderDetail.tb_ProductDetai.tb_Products.Title,
                                SoLuong = OrderDetail.Quantity,
                                Price = OrderDetail.Price,
                                PriceTotal = checkIdOrder.TotalAmount,
                                Size=(int) OrderDetail.tb_ProductDetai.Size

                            };
                            if (OrderDetail.tb_ProductDetai.tb_Products.tb_ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                            {
                                item.ProductImg = OrderDetail.tb_ProductDetai.tb_Products.tb_ProductImage.FirstOrDefault(row => row.IsDefault).Image;
                            }
                            ViewBag.TotalPrice = checkIdOrder.TotalAmount;
                            can.AddToCart(item, OrderDetail.Quantity);

                        }
                    }

                    Session["CancelOrder"] = can;
                    code = new
                    {
                        Success = true,
                        msg = "",
                        code = 1
                    };
                }
            }
            return Json(code);
        }











        public ActionResult CancelOrder()
        {
            Cancel cart = (Cancel)Session["CancelOrder"];
            if (cart != null && cart.Items.Any())
            {
                return View(cart.Items);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelOrder(ReturnAndCancelOrder req, tb_Order model)
        {
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                if (Session["IdKhachHang"] != null)
                {
                    int idKhach = (int)Session["IdKhachHang"];
                    Cancel cart = (Cancel)Session["CancelOrder"];
                    if (cart != null)
                    {
                        foreach (var item in cart.Items)
                        {
                            var itemOrder = db.tb_Order.FirstOrDefault(x => x.IdKhachHang == idKhach && x.OrderId == item.OrderId);
                            if (itemOrder != null)
                            {
                                var checkQuantityPro = db.tb_ProductDetai.Find(item.ProductId);
                                if (checkQuantityPro != null)
                                {
                                    checkQuantityPro.Quantity += item.SoLuong;//capp nhap lai so luong cho ban porducts

                                    itemOrder.typeOrder = true;//capp nhap trang thai cho bang order
                                    if (req.Status == 1)
                                    {
                                        itemOrder.Status = "Thay đổi phương thức thanh toán";
                                    }
                                    else if (req.Status == 2)
                                    {
                                        itemOrder.Status = "Thay đổi địa chỉ";
                                    }
                                    else if (req.Status == 3)
                                    {
                                        itemOrder.Status = "Thay đổi sản phẩm khác";
                                    }
                                    itemOrder.typeReturn = false;
                                    //DeleteCartSucces(idKhach, item.ProductId);
                                    db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                                    db.Entry(checkQuantityPro).State = System.Data.Entity.EntityState.Modified;
                                    db.SaveChanges();
                                }
                            } }
                        cart.ClearCart();
                        code = new { Success = true, Code = 1, Url = "" };
                    }
                }
                else
                {
                    ViewBag.error = "Không tìm thấy Sesstion khách";
                }
            }
            return Json(code);
        }



        ///Xác Nhận đã nhận được đơn hàng
        [HttpPost]
        public ActionResult ConFirmOrder(int id)
        {
            var code = new { Success = false, Code = -1, Url = "" };

            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];

                var checkOrder = db.tb_Order.FirstOrDefault(x => x.IdKhachHang == idKhach && x.OrderId == id);
                if (checkOrder != null)
                {

                    checkOrder.SuccessDate = DateTime.Now;
                    checkOrder.Success = true;
                    db.Entry(checkOrder).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    code = new { Success = true, Code = 1, Url = "" };
                }
            }
            else
            {
                ViewBag.error = "Không tìm thấy Sesstion khách";
            }

            return Json(code);
        }








        ///Trả Hàng

        public ActionResult Partial_ListReturnOrder(int id)
        {
            var item = db.tb_OrderDetail.Where(x => x.OrderId == id).ToList();
            return View(item);
        }

        public ActionResult Partial_ReturnOrder()
        {
            return PartialView();
        }
       
        public ActionResult ReturnOrder(int id)
        {
            var item = db.tb_Order.Find(id);
            return View(item);
        }




        //test




        public ActionResult Partial_purchaselist()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }



        [HttpPost]
        public ActionResult DatHang(int id ,List<int> productIds)
        {
            var code = new { Success = false, msg = "", code = -1 };
            if (productIds != null && productIds.Any())
            {
                if (Session["IdKhachHang"] != null)
                {
                    int idKhach = (int)Session["IdKhachHang"];
                    var checkIdOrder = db.tb_Order.SingleOrDefault(x => x.OrderId==id);
                    if (checkIdOrder != null)
                    {
                        int checkId = checkIdOrder.OrderId;

                        ShoppingCart cart = (ShoppingCart)Session[""];
                        if (cart == null)
                        {
                            cart = new ShoppingCart();
                        }

                        foreach (var productId in productIds)
                        {
                            var cartItem = db.tb_OrderDetail.SingleOrDefault(row => row.OrderId == checkId && row.ProductDetai == productId);
                            if (cartItem != null)
                            {
                                var checkSanPham = db.tb_ProductDetai.FirstOrDefault(row => row.ProductDetai == productId);
                                if (checkSanPham != null)
                                {
                                    ShoppingCartItem item = new ShoppingCartItem
                                    {
                                        ProductId = (int)cartItem.ProductDetai,
                                        ProductName = cartItem.tb_ProductDetai.tb_Products.Title.ToString(),
                                        CategoryName = cartItem.tb_ProductDetai.tb_Products.tb_ProductCategory.Title.ToString(),
                                        Alias = cartItem.tb_ProductDetai.tb_Products.Alias.ToString(),
                                        SoLuong = cartItem.Quantity,
                                    };

                                    if (cartItem.tb_ProductDetai.tb_Products.tb_ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                                    {
                                        item.ProductImg = cartItem.tb_ProductDetai.tb_Products.tb_ProductImage.FirstOrDefault(row => row.IsDefault).Image;
                                    }

                                    item.Price = (decimal)checkSanPham.tb_Products.Price;
                                    if (checkSanPham.tb_Products.PriceSale > 0)
                                    {
                                        item.Price = (decimal)checkSanPham.tb_Products.PriceSale;
                                    }
                                    item.PriceTotal = item.SoLuong * item.Price;
                                    cart.AddToCart(item, cartItem.Quantity);
                                }
                            }
                        }

                        Session["Cart"] = cart;
                        code = new
                        {
                            Success = true,
                            msg = "",
                            code = 1
                        };
                        //return RedirectToAction("CheckOut");
                    }
                }
            }
            else
            {
                code = new { Success = false, msg = "", code = -2 };


            }


            return Json(code);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnOrder(ReturnOrderToken req , int id) 
        {
            var code = new { Success = false, Code = -1, Url = "" };
            if (ModelState.IsValid)
            {
                if (Session["IdKhachHang"] != null)
                {
                    int idKhach = (int)Session["IdKhachHang"];
                   
                   
                       
                            var itemOrder = db.tb_Order.FirstOrDefault(x => x.IdKhachHang == idKhach && x.OrderId == id);
                            if (itemOrder != null)
                            {

                                  
                                            tb_Return trahang = new tb_Return();
                                            trahang.OrderId = itemOrder.OrderId;
                                            trahang.CreateDate = DateTime.Now;
                                            trahang.IdKhachHang = idKhach;
                                            trahang.Code = itemOrder.Code;
                                            trahang.Confirm = false;

                                            if (req.Status == 1)
                                            {
                                                trahang.Satus = "Mặt hàng bị lỗi";
                                            }
                                            else if (req.Status == 2)
                                            {
                                                trahang.Satus = "Không đúng sản phẩm trên Web   ";
                                            }
                                            else if (req.Status == 3)
                                            {
                                                trahang.Satus = "Số lượng bị thiếu ";
                                            }
                                            itemOrder.typeReturn = true;

                                            db.tb_Return.Add(trahang);
                                            db.SaveChanges();

                                            db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;

                                            db.SaveChanges();
                                            //code = new { Success = true, Code = 1, Url = "" };
                                            return RedirectToAction("SuccessReturnOrder");
                                   
                                  
                            }
                }
                else
                {
                    ViewBag.error = "Không tìm thấy Sesstion khách";
                }
            }
            return Json(code);
        }




        public ActionResult SuccessCancelOrder() 
        {
            return View();
        }

        public ActionResult SuccessReturnOrder()
        {
            return View();
        }
    }



 }
