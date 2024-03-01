using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace WSite_ShowRoom_CtyThoiTrang.Models
{
    public class Seller
    {

        public string SelectedOption { get; set; }

        public List<SelectListItem> Options { get; set; } = new List<SelectListItem>
        {
             new SelectListItem { Value = "TatCaHoaDon", Text = "Tất cả hóa đơn" },
            new SelectListItem { Value = "HoaDonHomNay", Text = "Hóa đơn hôm nay" },
            new SelectListItem { Value = "KiemTraHoaDon", Text = "Kiểm tra hóa đơn" },
            // Thêm các lựa chọn khác nếu cần
        };
    }

}