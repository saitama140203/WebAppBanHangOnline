using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppBanHangOnlineNhomNBTPQ.ViewModel;
using System.IO;

namespace WebAppBanHangOnlineNhomNBTPQ.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private dbQUANLYBANQUANAOEntities2 db = new dbQUANLYBANQUANAOEntities2();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var products = db.tbSANPHAM.ToList() ?? new List<tbSANPHAM>(); // Đảm bảo không null
            var categories = db.tbDANHMUC.ToList() ?? new List<tbDANHMUC>(); // Đảm bảo không null
            ViewBag.Categories = categories;
            return View(products);
        }

        [HttpPost]
        public ActionResult AddProduct(tbSANPHAM product, HttpPostedFileBase[] fileInputs)
        {
            if (ModelState.IsValid)
            {
                db.tbSANPHAM.Add(product);
                db.SaveChanges();

                // Xử lý và lưu thông tin ảnh
                foreach (var file in fileInputs)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/path/to/images/"), fileName);
                        file.SaveAs(path);

                        var newImage = new tbHINHANH { MASANPHAM = product.MASANPHAM, HINHANH = fileName };
                        db.tbHINHANH.Add(newImage);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(int id, tbSANPHAM updatedProduct, HttpPostedFileBase[] fileInputs)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = db.tbSANPHAM.Find(id);
                if (existingProduct != null)
                {
                    // Cập nhật thông tin sản phẩm
                    existingProduct.TENSANPHAM = updatedProduct.TENSANPHAM;
                    existingProduct.MOTA = updatedProduct.MOTA;
                    // Cập nhật các thuộc tính khác...

                    // Xóa ảnh cũ từ cơ sở dữ liệu
                    var existingImages = db.tbHINHANH.Where(h => h.MASANPHAM == id).ToList();
                    foreach (var existingImage in existingImages)
                    {
                        db.tbHINHANH.Remove(existingImage);
                        // Xóa file ảnh từ thư mục nếu cần
                        var filePath = Server.MapPath("~/path/to/images/" + existingImage.HINHANH);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    // Xử lý và lưu ảnh mới
                    foreach (var file in fileInputs)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/path/to/images/"), fileName);
                            file.SaveAs(path);

                            // Thêm thông tin ảnh mới vào cơ sở dữ liệu
                            var newImage = new tbHINHANH { MASANPHAM = id, HINHANH = fileName };
                            db.tbHINHANH.Add(newImage);
                        }
                    }

                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, hiển thị lại form với thông tin hiện tại
            ViewBag.Images = db.tbHINHANH.Where(h => h.MASANPHAM == id).ToList();
            ViewBag.Categories = db.tbDANHMUC.ToList();
            return View(updatedProduct);
        }


        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            var product = db.tbSANPHAM.Find(id);
            if (product != null)
            {
                // Xóa ảnh liên quan
                var images = db.tbHINHANH.Where(h => h.MASANPHAM == id).ToList();
                foreach (var image in images)
                {
                    db.tbHINHANH.Remove(image);
                }

                // Xóa sản phẩm
                db.tbSANPHAM.Remove(product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}