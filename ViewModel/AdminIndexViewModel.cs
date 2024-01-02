using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppBanHangOnlineNhomNBTPQ.ViewModel
{
    public class AdminIndexViewModel
    {
        public IEnumerable<tbSANPHAM> Products { get; set; }
        public IEnumerable<tbDANHMUC> Categories { get; set; }
    }
}