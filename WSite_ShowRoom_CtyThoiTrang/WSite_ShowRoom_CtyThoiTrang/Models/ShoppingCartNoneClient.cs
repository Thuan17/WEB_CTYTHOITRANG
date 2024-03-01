using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSite_ShowRoom_CtyThoiTrang.Models
{
    public class ShoppingCartNoneClient
    {
        public List<ShoppingCartNoneClientItem> Items { get; set; }
        public ShoppingCartNoneClient()
        {

            Items = new List<ShoppingCartNoneClientItem>();
        }

        public void AddToCart(ShoppingCartNoneClientItem item, int SoLuong)
        {
            var checkSanPham = Items.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (checkSanPham != null)
            {
                checkSanPham.SoLuong = SoLuong;
                checkSanPham.PriceTotal = checkSanPham.Price * checkSanPham.SoLuong;
            }
            else
            {
                Items.Add(item);
            }
        }
        public decimal GetPriceTotal()
        {
            return Items.Sum(x => x.PriceTotal);
        }
        public int GetTongSoLuong()
        {
            return Items.Sum(x => x.SoLuong);
        }

        public void ClearCart()
        {
            Items.Clear();
        }


        ///Xoa San Pham Ra khoi gio hang
        ///
        public void Remove(int id)
        {
            var checkSanPham = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkSanPham != null)
            {
                Items.Remove(checkSanPham);
            }
        }

        //Cap Nhap San Pham Ra khoi gio hang
        public void UpSoLuong(int id, int SoLuong)
        {
            var checkSanPham = Items.SingleOrDefault(x => x.ProductId == id);
            if (checkSanPham != null)
            {
                checkSanPham.SoLuong = SoLuong;
                checkSanPham.PriceTotal = checkSanPham.Price * checkSanPham.SoLuong;
            }
        }

    }




    public class ShoppingCartNoneClientItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg { get; set; }
        public int SoLuong { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set; }

    }

}