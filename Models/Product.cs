using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBanHangOnlineNhomNBTPQ.Models
{
    public class Product
    {
        public int MASANPHAM { get; set; }
        public int TENSANPHAM { get; set; }
        public int SIZE { get; set; }
        public int MAU { get; set; }
        public int DONGIA { get; set; }
        public int SOLUONG { get; set; }
        public int MOTA  { get; set; }
        public int MADANHMUC { get; set; }
        public Category Category { get; set; }
    }
}