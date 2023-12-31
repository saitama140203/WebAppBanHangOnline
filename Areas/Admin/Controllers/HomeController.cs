using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppBanHangOnlineNhomNBTPQ.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private dbQUANLYBANQUANAOEntities1 db = new dbQUANLYBANQUANAOEntities1();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var products = db.tbSANPHAMs.ToList(); //Lấy danh sách sản phẩm
            var categories = db.tbDANHMUCs.ToList(); //Lấy danh sách danh mục
            ViewBag.Categories = categories; //Gửi danh mục đến View thông qua ViewBag
            return View(products);
        }
    }
}