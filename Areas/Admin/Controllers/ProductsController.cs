using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppBanHangOnlineNhomNBTPQ.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private dbQUANLYBANQUANAOEntities1 db = new dbQUANLYBANQUANAOEntities1();

        // THÊM SẢN PHẨM
        [HttpGet]
        public ActionResult AddProduct()
        {
            // Trả về View để thêm sản phẩm
            ViewBag.Categories = db.tbDANHMUCs.ToList();
            return View("AddProduct");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(tbSANPHAM product, HttpPostedFileBase fileInput)
        {
            if (ModelState.IsValid)
            {
                db.tbSANPHAMs.Add(product);
                db.SaveChanges();

                if (fileInput != null && fileInput.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileInput.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                    fileInput.SaveAs(path);

                    tbHINHANH newImage = new tbHINHANH
                    {
                        MASANPHAM = product.MASANPHAM,
                        HINHANH = "/images/" + fileName
                    };
                    db.tbHINHANHs.Add(newImage);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }



        // SỬA SẢN PHẨM
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            var product = db.tbSANPHAMs.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.tbDANHMUCs.ToList();
            return View("EditProduct", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduct(tbSANPHAM product, HttpPostedFileBase fileInput)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = System.Data.EntityState.Modified;

                if (fileInput != null && fileInput.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileInput.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                    fileInput.SaveAs(path);

                    // Tìm hình ảnh hiện tại và cập nhật hoặc thêm mới nếu cần
                    var currentImage = db.tbHINHANHs.FirstOrDefault(h => h.MASANPHAM == product.MASANPHAM);
                    if (currentImage != null)
                    {
                        currentImage.HINHANH = "/images/" + fileName;
                    }
                    else
                    {
                        tbHINHANH newImage = new tbHINHANH
                        {
                            MASANPHAM = product.MASANPHAM,
                            HINHANH = "/images/" + fileName
                        };
                        db.tbHINHANHs.Add(newImage);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("EditProduct", product);
        }



        // XOÁ SẢN PHẨM
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.tbSANPHAMs.Find(id);
            db.tbSANPHAMs.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.tbSANPHAMs.ToList();
            return View("Index", products);
        }
    }
}