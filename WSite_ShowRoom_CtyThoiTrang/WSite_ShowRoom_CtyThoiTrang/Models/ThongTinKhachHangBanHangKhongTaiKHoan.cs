using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WSite_ShowRoom_CtyThoiTrang.Models
{
    public class ThongTinKhachHangBanHangKhongTaiKHoan
    {

        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public int TypePayment { get; set; }
    }
}