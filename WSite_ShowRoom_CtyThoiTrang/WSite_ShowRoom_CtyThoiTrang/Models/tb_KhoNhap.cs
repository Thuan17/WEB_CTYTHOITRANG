//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSite_ShowRoom_CtyThoiTrang.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_KhoNhap
    {
        public int IdKhoNhap { get; set; }
        public System.DateTime ImportDate { get; set; }
        public string ImportBy { get; set; }
        public int ProductId { get; set; }
        public int SoLuong { get; set; }
        public string MaPhieuNhap { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Modifeby { get; set; }
        public string MSNV { get; set; }
        public int IdKho { get; set; }
    
        public virtual tb_Kho tb_Kho { get; set; }
        public virtual tb_NhanVien tb_NhanVien { get; set; }
    }
}
