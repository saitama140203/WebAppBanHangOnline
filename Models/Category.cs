using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBanHangOnlineNhomNBTPQ.Models
{
    public class Category
    {
        public int MADANHMUC { get; set; }
        public string TENDANHMUC { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}