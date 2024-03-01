using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WSite_ShowRoom_CtyThoiTrang.Models;
using WSite_ShowRoom_CtyThoiTrang.Models.Payment;

namespace WSite_ShowRoom_CtyThoiTrang.Controllers
{
    public class TestCartController : Controller
    {
        CONGTYTHOITRANGEntities db = new CONGTYTHOITRANGEntities();

        // GET: TestCart
        public ActionResult Index()
        {
            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];

                var checkIdCart = db.tb_Cart.SingleOrDefault(x => x.IdKhachHang == idKhach);

                if (checkIdCart != null)
                {
                    int checkId = checkIdCart.CartId;

                    var cartItem = db.tb_CartItem.Where(row => row.CartId == checkId);
                    if (cartItem != null && cartItem.Any())
                    {
                        ViewBag.Cart = cartItem;
                    }

                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult Partial_ItemCart()
        {

            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];
                //tb_Cart sessCart = (tb_Cart)idKhach;
                var checkIdCart = db.tb_Cart.FirstOrDefault(x => x.IdKhachHang == idKhach);
                if (checkIdCart != null)
                {
                    int checkId = checkIdCart.CartId;

                    var cartItem = db.tb_CartItem.Where(row => row.CartId == checkId);
                    return PartialView(cartItem);
                }
                else
                {

                }

            }
            return PartialView();
        }





        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1 };

            try
            {
                if (Session["IdKhachHang"] != null)
                {
                    int idKhach = (int)Session["IdKhachHang"];

                    var checkIdCart = db.tb_Cart.FirstOrDefault(x => x.IdKhachHang == idKhach);

                    if (checkIdCart != null)
                    {
                        int checkId = checkIdCart.CartId;

                        var checkIdCartItem = db.tb_CartItem.FirstOrDefault(ci => ci.CartId == checkId && ci.ProductDetai == id);

                        if (checkIdCartItem != null)
                        {
                            checkIdCartItem.Quantity = quantity;
                            db.SaveChanges();

                            code = new { Success = true, msg = "ok", code = 1 };
                        }
                        else
                        {
                            code = new { Success = false, msg = "Sản phẩm không tồn tại trong giỏ hàng", code = 0 };
                        }
                    }
                    else
                    {
                        code = new { Success = false, msg = "Không tìm thấy giỏ hàng", code = -1 };
                    }
                }
                else
                {
                    code = new { Success = false, msg = "Không có phiên làm việc (session) cho khách hàng", code = -1 };
                }
            }
            catch (Exception ex)
            {
                code = new { Success = false, msg = "Lỗi cập nhật số lượng sản phẩm: " + ex.Message, code = -1 };
            }

            return Json(code);
        }





        [HttpPost]
        public ActionResult DeleteAll(List<int> CartItemId)
        {
            var code = new { Success = false, msg = "", code = -1 };

            try
            {
                if (Session["IdKhachHang"] != null)
                {
                    int idKhach = (int)Session["IdKhachHang"];

                    // Kiểm tra xem có giỏ hàng nào cho khách hàng này chưa
                    var checkIdCart = db.tb_Cart.FirstOrDefault(x => x.IdKhachHang == idKhach);

                    if (checkIdCart != null)
                    {
                        // Lấy CartId từ giỏ hàng của khách hàng
                        int checkId = checkIdCart.CartId;
                        foreach (var cartItemId in CartItemId)
                        {
                            // Kiểm tra xem sản phẩm có tồn tại trong giỏ hàng không
                            var checkIdCartItem = db.tb_CartItem.FirstOrDefault(ci => ci.CartId == checkId && ci.ProductDetai == cartItemId);
                            if (checkIdCartItem != null)
                            {
                                db.tb_CartItem.Remove(checkIdCartItem);
                                db.SaveChanges();
                                code = new { Success = true, msg = "ok", code = 1 };
                            }
                            else
                            {


                            }
                        }
                    }
                    else
                    {
                        code = new { Success = false, msg = "Không tìm thấy giỏ hàng", code = -1 };
                    }
                }
                else
                {
                    code = new { Success = false, msg = "Không có phiên làm việc (session) cho khách hàng", code = -1 };
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                code = new { Success = false, msg = "Lỗi xóa sản phẩm: " + ex.Message, code = -1 };
            }

            return Json(code);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1 };

            try
            {
                if (Session["IdKhachHang"] != null)
                {
                    int idKhach = (int)Session["IdKhachHang"];

                    // Kiểm tra xem có giỏ hàng nào cho khách hàng này chưa
                    var checkIdCart = db.tb_Cart.FirstOrDefault(x => x.IdKhachHang == idKhach);

                    if (checkIdCart != null)
                    {
                        // Lấy CartId từ giỏ hàng của khách hàng
                        int checkId = checkIdCart.CartId;

                        // Kiểm tra xem sản phẩm có tồn tại trong giỏ hàng không
                        var checkIdCartItem = db.tb_CartItem.FirstOrDefault(ci => ci.CartId == checkId && ci.ProductDetai == id);

                        if (checkIdCartItem != null)
                        {
                            // Nếu sản phẩm tồn tại trong giỏ hàng, xóa nó
                            db.tb_CartItem.Remove(checkIdCartItem);
                            db.SaveChanges();

                            code = new { Success = true, msg = "ok", code = 1 };
                        }
                        else
                        {
                            code = new { Success = false, msg = "Sản phẩm không tồn tại trong giỏ hàng", code = 0 };
                        }
                    }
                    else
                    {
                        code = new { Success = false, msg = "Không tìm thấy giỏ hàng", code = -1 };
                    }
                }
                else
                {
                    code = new { Success = false, msg = "Không có phiên làm việc (session) cho khách hàng", code = -1 };
                }
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                code = new { Success = false, msg = "Lỗi xóa sản phẩm: " + ex.Message, code = -1 };
            }

            return Json(code);
        }

        public ActionResult AddtoCart() 
        {
            if (Session["IdKhachHang"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
                return RedirectToAction("index");
           
        }




      [HttpPost]
        public ActionResult AddtoCart(int id, int soluong)
        {
            var code = new { Success = false, msg = "", code = -1 };

            if (Session["IdKhachHang"] != null)
            {
                
                int idKhach = (int)Session["IdKhachHang"];

                var checkIdCart = db.tb_Cart.SingleOrDefault(x => x.IdKhachHang == idKhach);

                if (checkIdCart != null)
                {
                    // Lấy CartId từ giỏ hàng của khách hàng
                    int checkId = checkIdCart.CartId;

                    // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
                    var checkIdCartItem = db.tb_CartItem.SingleOrDefault(ci => ci.CartId == checkId && ci.ProductDetai == id);
            
                    if (checkIdCartItem != null)
                    {
                        // Nếu sản phẩm đã tồn tại trong giỏ hàng, cập nhật số lượng và giá
                        checkIdCartItem.Quantity += soluong;
                        checkIdCartItem.TemPrice = checkIdCartItem.Price * checkIdCartItem.Quantity;
                        db.SaveChanges();
                    }
                    else
                    {


                        // Nếu sản phẩm chưa tồn tại trong giỏ hàng, thêm mới
                        var product = db.tb_ProductDetai.Find(id);
                        if (product != null)
                        {
                            tb_CartItem cartitem = new tb_CartItem
                            {
                                CartId = checkId,
                                ProductDetai = product.ProductDetai,
                                Quantity = soluong,
                                Price = (decimal)product.tb_Products.Price,
                                TemPrice = (decimal)product.tb_Products.Price * soluong,
                                PriceTotal = (decimal)product.tb_Products.Price
                            };
                            db.tb_CartItem.Add(cartitem);
                            db.SaveChanges();
                        }
                        else
                        {
                            code = new { Success = false, msg = "Sản phẩm không tồn tại!", code = -1 };
                            return Json(code);
                        }
                    }

                    code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công!", code = 1 };
                
                }
                else
                {
                    return PartialView("Cartnull");
                }

            }
            else
            {
               code=new { Success = false, msg = "", code = -2 };
            }
            return Json(code);

        }






        public ActionResult Partial_purchaselist()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }



        public ActionResult purchaselist()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return View(cart.Items);
            }
            return View();

        }
        public ActionResult ShowCount()
        {
            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];

                var checkIdCart = db.tb_Cart.SingleOrDefault(x => x.IdKhachHang == idKhach);

                if (checkIdCart != null)
                {
                    int checkId = checkIdCart.CartId;

                    var cartItem = db.tb_CartItem.Count(row => row.CartId == checkId);
                    return Json(new { Count = cartItem }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }






        [HttpPost]
        public ActionResult DatHang(List<int> productIds)
        {
            var code = new { Success = false, msg = "", code = -1 };
            if (productIds != null && productIds.Any())
            {
                if (Session["IdKhachHang"] != null)
                {
                    int idKhach = (int)Session["IdKhachHang"];
                    var checkIdCart = db.tb_Cart.SingleOrDefault(x => x.IdKhachHang == idKhach);
                    if (checkIdCart != null)
                    {
                        int checkId = checkIdCart.CartId;

                        ShoppingCart cart = (ShoppingCart)Session[""];
                        if (cart == null)
                        {
                            cart = new ShoppingCart();
                        }

                        foreach (var productId in productIds)
                        {
                            var cartItem = db.tb_CartItem.SingleOrDefault(row => row.CartId == checkId && row.ProductDetai == productId);
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


        public ActionResult P_ThongTinKhach()
        {
            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];
                var khachHang = db.tb_KhachHang.Find(idKhach);
                return PartialView(khachHang);
            }
            return PartialView();
        }

        public ActionResult P_ChiTietSanPhamMua()
        {

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }

            return PartialView();
        }


        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }


        public ActionResult VnpayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }


                string orderCode = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                String TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                String bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        var itemOrder = db.tb_Order.FirstOrDefault(x => x.Code == orderCode);
                        if (itemOrder != null)
                        {
                            itemOrder.TypePayment = 2;//đã thanh toán

                            db.tb_Order.Attach(itemOrder);
                            db.Entry(itemOrder).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                    }
                    else
                    {
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                    }
                    ViewBag.ThanhToanThanhCong = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                }

            }
            //var a = UrlPayment(0, "DH64050");
            return View();
        }

        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.Cart = cart;
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewVNPay req, tb_Products model)
        {

            try {

                var code = new { Success = false, Code = -1, Url = "" };
                if (ModelState.IsValid)
                {

                    if (Session["IdKhachHang"] != null)
                    {
                        int idKhach = (int)Session["IdKhachHang"];
                        var inforKhachHang = db.tb_KhachHang.FirstOrDefault(x => x.IdKhachHang == idKhach);
                        ShoppingCart cart = (ShoppingCart)Session["Cart"];
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

                                        DeleteCartSucces(idKhach, item.ProductId);

                                        db.Entry(checkQuantityPro).State = System.Data.Entity.EntityState.Modified;
                                        db.SaveChanges();
                                        isTransactionSuccessful = true;
                                    }
                                    else
                                    {
                                        ViewBag.error = "Số lượng sản phẩm không đủ";
                                        code = new { Success = false, Code = -7, Url = "" };//Số lượng sản phẩm hiện không đủ 
                                    }
                                }
                            }


                            if (isTransactionSuccessful)
                            {
                                tb_Order order = new tb_Order();
                                order.CustomerName = inforKhachHang.TenKhachHang;
                                order.Phone = inforKhachHang.SDT;
                                order.Address = inforKhachHang.DiaChi;
                                order.Email = inforKhachHang.Email;
                                order.typeOrder = false;





                                cart.Items.ForEach(row => order.tb_OrderDetail.Add(new tb_OrderDetail
                                {

                                    ProductDetai = row.ProductId,
                                    Quantity = row.SoLuong,
                                    Price = row.Price,
                                }));
                                order.TotalAmount = cart.Items.Sum(x => (x.Price * x.SoLuong));
                                order.TypePayment = req.TypePayment;
                                //inforKhachHang.TypePayment = req.TypePayment;   

                                order.CreatedDate = DateTime.Now;
                                order.ModifiedDate = DateTime.Now;
                                order.CreatedBy = inforKhachHang.SDT;
                                order.IdKhachHang = inforKhachHang.IdKhachHang;
                                order.Confirm = false;
                                order.typeOrder = false;
                                order.Status = null;
                                order.typeReturn = false;
                                order.Success = false;
                                Random ran = new Random();
                                order.Code = "DH" + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9) + ran.Next(0, 9);


                                //var checkSanPham=db.tb_Products.SingleOrDefault(row=>row.ProductId==cart.Imm)




                                db.tb_Order.Add(order);
                                //db.tb_KhachHang.Add(inforKhachHang);
                                db.SaveChanges();








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
                                contentCustomer = contentCustomer.Replace("{{Email}}", inforKhachHang.Email);
                                contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
                                contentCustomer = contentCustomer.Replace("{{ThanhTien}}", WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(thanhTien, 0));
                                contentCustomer = contentCustomer.Replace("{{TongTien}}", WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(tongTien, 0));
                                WSite_ShowRoom_CtyThoiTrang.Common.Common.SendMail("ShopOnline", "Đơn hàng #" + order.Code, contentCustomer.ToString(), inforKhachHang.Email);

                                string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/templates/send1.html"));
                                contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                                contentAdmin = contentAdmin.Replace("{{SanPham}}", SanPham);
                                contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                                contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                                contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                                contentAdmin = contentAdmin.Replace("{{Email}}", inforKhachHang.Email);
                                contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
                                contentAdmin = contentAdmin.Replace("{{ThanhTien}}", WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(thanhTien, 0));
                                contentAdmin = contentAdmin.Replace("{{TongTien}}", WSite_ShowRoom_CtyThoiTrang.Common.Common.FormatNumber(tongTien, 0));
                                WSite_ShowRoom_CtyThoiTrang.Common.Common.SendMail("ShopOnline", "Đơn hàng mới #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                                cart.ClearCart();
                                code = new { Success = true, Code = req.TypePayment, Url = "" };
                                if (req.TypePayment == 2)
                                {
                                    var url = UrlPayment(req.TypePaymentVN, order.Code);
                                    code = new { Success = true, Code = req.TypePayment, Url = url };
                                }
                            }
                            else
                            {
                                code = new { Success = false, Code = -5, Url = "" };//Số lượng sản phẩm hiện không đủ 

                            }


                        }
                    }
                }
               
                return Json(code);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Cartnull");
            }
        }




        private void DeleteCartSucces(int idKhachHang, int productId)
        {
            if (Session["IdKhachHang"] != null)
            {
                int idKhach = (int)Session["IdKhachHang"];
                var checkCart = db.tb_Cart.FirstOrDefault(x => x.IdKhachHang == idKhach);
                if (checkCart != null)
                {
                    var checkItemCart = db.tb_CartItem.SingleOrDefault(x => x.CartId == checkCart.CartId && x.ProductDetai == productId);
                    if (checkItemCart != null)
                    {
                        db.tb_CartItem.Remove(checkItemCart);
                        db.SaveChanges();
                    }
                }
            }
        }

        public ActionResult MuaHangThanhCong()
        {
            return View();
        }
        #region/* Thanh toán vnpay*/
        public string UrlPayment(int TypePaymentVN, string orderCode)
        {
            var urlPayment = "";
            var order = db.tb_Order.FirstOrDefault(x => x.Code == orderCode);
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (long)order.TotalAmount * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + order.Code);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.Code); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
        }
        #endregion



        public void TruSanPham(List<int> productIds) 
        {
            
        }




        public ActionResult Order()
        {             
            int idKhach = (int)Session["IdKhachHang"];
            var checkIdOrder = db.tb_Order.Where(x => x.IdKhachHang == idKhach);
            //if (checkIdOrder != null) 
            //{
            //    var item = db.tb_OrderDetail.Find(checkIdOrder);
            //    return View(item);
            //}
            return View(checkIdOrder);
        }


        public ActionResult Details(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Lấy thông tin đơn đặt hàng từ cơ sở dữ liệu
            var order = db.tb_Order.Find(orderId);

            if (order == null)
            {
                return HttpNotFound();
            }

            // Lấy chi tiết đơn đặt hàng tương ứng từ cơ sở dữ liệu
            var orderDetails = db.tb_OrderDetail.Where(od => od.OrderId == orderId).ToList();

            // Đặt thông tin đơn đặt hàng và chi tiết đơn đặt hàng vào ViewBag để hiển thị trong View
            ViewBag.Order = order;
            ViewBag.OrderDetails = orderDetails;

            return PartialView(orderDetails);
        }

        public ActionResult OrderDetail(int? id) 
        {
            var item = db.tb_OrderDetail.Where(x=>x.OrderId==id).ToList();  
            return PartialView(item);
        }



        public ActionResult Cartnull()
        {
            return View();
        }



    }
}