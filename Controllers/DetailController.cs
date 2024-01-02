using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppBanHangOnlineNhomNBTPQ.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        private dbQUANLYBANQUANAOEntities2 db = new dbQUANLYBANQUANAOEntities2();
        public ActionResult Index()
        {
            return View();
        }

    }
}