using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSite_ShowRoom_CtyThoiTrang.Models
{
    
    public class Cancel
    {
        public List<CanceltItem> Items { get; set; }
        
        public Cancel()
        {

            Items = new List<CanceltItem>();
        }
        public decimal GetPriceTotal()
        {
            return Items.Sum(x => x.PriceTotal);
        }

        public void AddToCart(CanceltItem item, int SoLuong)
        {
            var checkSanPham = Items.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (checkSanPham != null)
            {
                checkSanPham.SoLuong = SoLuong;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void ClearCart()
        {
            Items.Clear();
        }

    }



    public class CanceltItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SoLuong { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set; }
        public int Size { get;set; }
        public string ProductImg { get; set; }

    }
}