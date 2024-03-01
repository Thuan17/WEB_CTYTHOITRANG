using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSite_ShowRoom_CtyThoiTrang.Models;
using WSite_ShowRoom_CtyThoiTrang.Models.Payment;

namespace WSite_ShowRoom_CtyThoiTrang.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();

        public ActionResult tesst() 
        {   
           
            return View(); 
        }



        public ActionResult Index()
        {
            ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.Cart = cart;
            }
            return View();
        }

        public ActionResult ShowCount()
        {
            ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];
            if (cart != null && cart.Items.Any())
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Partial_ItemCart()
        {
            ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }

            return PartialView();
        }

        public ActionResult P_ThongTinKhach()
        {
            return PartialView();
        }
        public ActionResult P_ChiTietSanPhamMua()
        {

            ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }

            return PartialView();
        }
        public ActionResult MuaThanhCong()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };
            ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];
            if (cart != null)
            {
                var checkSanPham = cart.Items.FirstOrDefault(row => row.ProductId == id);
                if (checkSanPham != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }


        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];

            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            else
            {

                return Json(new { Success = false, msg = "Không có sản phẩm trong giỏ hàng!" });
            }

        }




        public ActionResult CheckOut()
        {
            ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.Cart = cart;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new { Success = false, Code = -1, Url="" };
            if (ModelState.IsValid)
            {
                ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];
                if (cart != null)
                {


                    tb_Products pro = new tb_Products();
                    tb_Order order = new tb_Order();
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    //order.Address = req.Address;
                    //order.Email = req.Email;
                    cart.Items.ForEach(row => order.tb_OrderDetail.Add(new tb_OrderDetail {
                        ProductDetai = row.ProductId,
                        Quantity = row.SoLuong,
                        Price = row.Price,
                    }));
                    order.TotalAmount = cart.Items.Sum(x => (x.Price * x.SoLuong));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    order.Quantity = cart.Items.Count();
                    //pro-=order.Quantity;


                    Random ran = new Random();
                    order.Code = "DH" + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9);

                    db.tb_Order.Add(order);
                    db.SaveChanges();

                    //cap nhap lai so luong san pham

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
                            }
                            else
                            {
                                code = new { Success = false, Code = -3, Url = "" };//Số lượng hiện không đủ !
                            }
                        }
                    }



                    //Gui Mail cho khach Hang

                    var SanPham = "";
                    var thanhTien = decimal.Zero;
                    var tongTien = decimal.Zero;
                    foreach (var item in cart.Items)
                    {
                        SanPham += "<tr>";
                        SanPham += "<td>" + item.ProductName + "</td>";
                        SanPham += "<td>" + item.SoLuong + "</td>";
                        SanPham += "<td>" + WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(item.PriceTotal, 0) + "</td>";
                        SanPham += "</tr>";
                        thanhTien += item.Price * item.SoLuong;
                    }
                    tongTien = thanhTien;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send2.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", SanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", req.Email);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(thanhTien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(tongTien, 0));
                    WSite_ShowRoom_CtyThoiTrang.Common.Common.SendMail("ShopOnline", "Đơn hàng #" + order.Code, contentCustomer.ToString(), req.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", SanPham);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    //contentAdmin = contentAdmin.Replace("{{Email}}", req.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(thanhTien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(tongTien, 0));
                    WSite_ShowRoom_CtyThoiTrang.Common.Common.SendMail("ShopOnline", "Đơn hàng mới #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    cart.ClearCart(); 
                    var url = UrlPayment(req.TypePaymentVNPay, order.Code);
                    code = new { Success = true, Code = 1, Url = "" };//Mua hàng thành công
                    
                }
            }
            else 
            {
                code = new { Success = false, Code = -2, Url = "" };//Không có sản phẩm trong giỏ hàng
            }
            return Json(code);
        }





        [HttpPost]
        public ActionResult AddToCart(int id, int soluong)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            var checkProducDetail =db.tb_ProductDetai.FirstOrDefault(x => x.ProductDetai == id);
            if (checkProducDetail != null) 
            {
                var checkSanPham = db.tb_Products.FirstOrDefault(row => row.ProductId == checkProducDetail.ProductId);
                if (checkSanPham != null)
                {

                    if (checkProducDetail.Quantity >= soluong)
                    {
                        ShoppingCartNoneClient cart = (ShoppingCartNoneClient)Session["CartNoneClient"];
                        if (cart == null)
                        {
                            cart = new ShoppingCartNoneClient();
                        }
                        ShoppingCartNoneClientItem item = new ShoppingCartNoneClientItem
                        {
                            ProductId = checkProducDetail.ProductDetai,
                            ProductName = checkSanPham.Title,
                            CategoryName = checkSanPham.tb_ProductCategory.Title,
                            Alias = checkSanPham.Alias,
                            SoLuong = soluong,
                        };
                        if (checkSanPham.tb_ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                        {
                            item.ProductImg = checkSanPham.tb_ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                        }
                        item.Price = (decimal)checkSanPham.Price;
                        if (checkSanPham.PriceSale > 0)
                        {
                            item.Price = (decimal)checkSanPham.PriceSale;
                        }
                        item.PriceTotal = item.SoLuong * item.Price;
                        //checkSanPham.Quantity = -soluong;
                        cart.AddToCart(item, soluong);
                        Session["CartNoneClient"] = cart;
                        code = new { Success = true, msg = "Thêm sản phẩm vào giở hàng thành công!", code = 1, Count = cart.Items.Count };

                    }
                    else
                    {
                        code = new { Success = false, msg = "Số lượng không đủ vui lòng liên hệ Shop!", code = -3, Count = 0 };
                    }

                }
            }
            else
            {
                code = new { Success = false, msg = "Không tìm thấy sản phẩm", code = -2, Count = 0 };
            }


          
            return Json(code);
        }


        public ActionResult CheckOutSuccess() 
        {
                return View();
        
        }



        #region
        public string UrlPayment(int TypePaymentVn, string orderCode) 
        {

            var urlPayment = "";
            var order = db.tb_Order.FirstOrDefault(row => row.Code ==orderCode);
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key



            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (order.TotalAmount * 100).ToString());
            if(TypePaymentVn ==1 )
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVn==2 )
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVn==3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());






            vnpay.AddRequestData("vnp_Locale","vn");
            vnpay.AddRequestData("vnp_OrderInfo","Thanh toan hoa don: "+order.Code);
            vnpay.AddRequestData("vnp_OrderType","other ");
            vnpay.AddRequestData("vnp_ReturnUrl",vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef",order.Code);



            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return urlPayment;
        }
        #endregion
    }
}