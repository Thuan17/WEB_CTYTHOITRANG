using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSite_ShowRoom_CtyThoiTrang.Models
{
    public class Admin_Add_Staff_ToKen
    {
        public string SDT { get; set; }    
        public string Name { get; set; }
        public string CCCD { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string DiaChi { get; set; }
        public decimal Luong { get; set; }
        public string GioiTinh { get; set; }
        public int  IdChucNang { get; set; }
    }
}