using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSite_ShowRoom_CtyThoiTrang.Models;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace WSite_ShowRoom_CtyThoiTrang.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
        // GET: Admin/Statistical
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
                var item = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == nvSession.MSNV && (row.IdChucNang == 1));
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



        public ActionResult Statistical()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "Account");
            }
            else
            {

                tb_NhanVien nvSession = (tb_NhanVien)Session["user"];
                var item = db.tb_PhanQuyen.SingleOrDefault(row => row.MSNV == nvSession.MSNV && (row.IdChucNang == 1));
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





        public ActionResult Partial_StatisticalByMon()
        {

            return View();
        }

        public ActionResult StatisticalByYear()
        {
            return View();
        }

        //thong ke theo năm

        [HttpGet]
        public ActionResult GetYearlyStatistical(string fromDate, string toDate)
        {
            var loinhuan = from a in db.tb_Order
                           join b in db.tb_OrderDetail on a.OrderId equals b.OrderId
                           join c in db.tb_ProductDetai on b.ProductDetai equals c.ProductDetai
                           join d in db.tb_Products on c.ProductId equals d.ProductId
                           select new
                           {
                               CreatedDate = a.CreatedDate,
                               Quantity = b.Quantity,
                               Price = b.Price,
                               OriginalPrice = d.OrigianlPrice
                           };

            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                loinhuan = loinhuan.Where(x => x.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                loinhuan = loinhuan.Where(x => x.CreatedDate < endDate.AddDays(1)); // Include the end date
            }

            var result = loinhuan.GroupBy(x => x.CreatedDate.Year)
                .Select(x => new
                {
                    Year = x.Key,
                    TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                    TotalSell = x.Sum(y => y.Quantity * y.Price),
                })
                .Select(x => new
                {
                    Year = x.Year,
                    DoanhThu = x.TotalSell,
                    LoiNhuan = x.TotalSell - x.TotalBuy
                });

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }


        // thong ke theo thang

        [HttpGet]
        public ActionResult GetStatisticalByMon(string fromDate, string toDate)
        {
            var loinhuan = from a in db.tb_Order
                           join b in db.tb_OrderDetail on a.OrderId equals b.OrderId
                           join c in db.tb_ProductDetai on b.ProductDetai equals c.ProductDetai
                           join d in db.tb_Products on c.ProductId equals d.ProductId
                           where (a.typeOrder == false)
                           select new
                           {
                               CreatedDate = a.CreatedDate,
                               Quantity = b.Quantity,
                               Price = b.Price,
                               OriginalPrice = d.OrigianlPrice
                           };

            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                loinhuan = loinhuan.Where(x => x.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                loinhuan = loinhuan.Where(x => x.CreatedDate < endDate.AddDays(1));
            }

            var result = loinhuan.GroupBy(x => new { x.CreatedDate.Year, x.CreatedDate.Month })
                .Select(x => new
                {
                    Year = x.Key.Year,
                    Month = x.Key.Month,
                    TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                    TotalSell = x.Sum(y => y.Quantity * y.Price),
                })
                .Select(x => new
                {
                    Year = x.Year,
                    Month = x.Month,
                    DoanhThu = x.TotalSell,
                    LoiNhuan = x.TotalSell - x.TotalBuy
                });

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);

        }


        //thong ke theo ngay

        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
        {


            var loinhuan = from a in db.tb_Order
                           join b in db.tb_OrderDetail
                           on a.OrderId equals b.OrderId
                           join c in db.tb_ProductDetai
                           on b.ProductDetai equals c.ProductDetai
                           join d in db.tb_Products
                           on c.ProductId equals d.ProductId
                           where(a.typeOrder==false)
                           select new
                           {
                               CreatedDate = a.CreatedDate,
                               Quantity = b.Quantity,
                               Price = b.Price,
                               OriginalPrice = d.OrigianlPrice

                           };




            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                loinhuan = loinhuan.Where(x => x.CreatedDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                loinhuan = loinhuan.Where(x => x.CreatedDate < endDate);
            }

            var result = loinhuan.GroupBy(x => DbFunctions.TruncateTime(x.CreatedDate)).Select(x => new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y => y.Quantity * y.Price),
            }).Select(x => new
            {
                Date = x.Date,
                DoanhThu = x.TotalSell,
                LoiNhuan = x.TotalSell - x.TotalBuy
            });
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }


        //thong ke theo san pham ban nhieu nhat trong ngay
        public ActionResult StatisticalTopProductsOnDay() 
        {
            return View();  
        }


        [HttpGet]
        public ActionResult GetTopProductsSoldToday()
        {
            DateTime today = DateTime.Now.Date; 
            var topProducts = db.tb_Order
                .Where(x => DbFunctions.TruncateTime(x.CreatedDate) == today && x.typeOrder == false)
                .Join(db.tb_OrderDetail, o => o.OrderId, od => od.OrderId, (o, od) => od)
                .Join(db.tb_ProductDetai, od => od.ProductDetai, pd => pd.ProductDetai, (od, pd) => new
                {
                    ProductId = pd.ProductId,
                    QuantitySold = od.Quantity,
                    Price = od.Price, 
                    OriginalPrice = od.tb_ProductDetai.tb_Products.OrigianlPrice 
                })
                .Join(db.tb_Products, pd => pd.ProductId, p => p.ProductId, (pd, p) => new
                {
                    ProductName = p.Title,
                    QuantitySold = pd.QuantitySold,
                    Price = pd.Price,
                    OriginalPrice = pd.OriginalPrice
                })
                .GroupBy(x => x.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalQuantitySold = g.Sum(x => x.QuantitySold),
                    TotalRevenue = g.Sum(x => x.QuantitySold * x.Price),
                    TotalCost = g.Sum(x => x.QuantitySold * x.OriginalPrice) 
                })
                .OrderByDescending(x => x.TotalQuantitySold)
                .Take(10)
                .ToList();

            return Json(new { Data = topProducts }, JsonRequestBehavior.AllowGet);

        }



    }
}