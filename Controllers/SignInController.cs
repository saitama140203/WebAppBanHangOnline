using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppBanHangOnlineNhomNBTPQ.Controllers
{
    public class SignInController : Controller
    {
        // GET: SignIn
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tbKHACHHANG objUser)
        {
            if (ModelState.IsValid)
            {
                using (dbQUANLYBANQUANAOEntities1 db = new dbQUANLYBANQUANAOEntities1())
                {
                    var obj = db.tbKHACHHANGs.Where(a => a.TAIKHOAN.Equals(objUser.TAIKHOAN) && a.MATKHAU.Equals(objUser.MATKHAU)).FirstOrDefault();
                    if (obj != null)
                    {
                       
                        Session["hoten"] = obj.HOTEN.ToString();
                        return RedirectToAction("Index","Home/Home");
                    }
                }
            }
            return View(objUser);
        }
        public ActionResult Home()
        {
            if (Session["hoten"] != null)
            {
                return View();
                
            }
            else
            {
                return RedirectToAction("SignIn", "Login");
            }
        }
    

    }
}