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
    
    public partial class tb_NhanVienImage
    {
        public int Id { get; set; }
        public string MSNV { get; set; }
        public string Image { get; set; }
        public bool IsDefault { get; set; }
    
        public virtual tb_NhanVien tb_NhanVien { get; set; }
    }
}
