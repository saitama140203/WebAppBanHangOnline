using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppBanHangOnlineNhomNBTPQ.Controllers
{
    public class HomeController : Controller
    {
        private dbQUANLYBANQUANAOEntities1 db = new dbQUANLYBANQUANAOEntities1();
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            tbSANPHAM tbSANPHAMM = db.tbSANPHAMs.Find(id);
            if (tbSANPHAMM == null)
            {
                return HttpNotFound();
            }
            //Sử dụng LINQ để truy vân dữ liệu ảnh
            ViewBag.Images = db.tbHINHANHs
                .Where(image => image.MASANPHAM == id)
                .Select(image => image.HINHANH).ToList();
            ViewBag.Reviews = db.tbREVIEWs
                .Where(review => review.MASANPHAM == id)
                .Select(review => review).ToList();
            return View(tbSANPHAMM);
        }
        public ActionResult Back()
        {
            return RedirectToAction("Index", "home/Home");
        }


    }

}