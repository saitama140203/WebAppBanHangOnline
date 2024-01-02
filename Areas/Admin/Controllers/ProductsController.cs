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
        private dbQUANLYBANQUANAOEntities2 db = new dbQUANLYBANQUANAOEntities2();

        [HttpGet]
        public ActionResult AddProduct()
        {
            // Tạo một đối tượng sản phẩm mới
            var newProduct = new tbSANPHAM();

            // Nếu bạn cần danh sách danh mục trong form, hãy đảm bảo bạn lấy danh sách này
            // và gửi nó đến View thông qua ViewBag hoặc ViewData
            ViewBag.Categories = db.tbDANHMUC.ToList();

            // Trả về View với đối tượng sản phẩm mới
            return View(newProduct);
        }

        [HttpPost]
        public ActionResult AddProduct(tbSANPHAM product, HttpPostedFileBase[] fileInputs)
        {
            if (ModelState.IsValid)
            {
                // Thêm thông tin sản phẩm vào cơ sở dữ liệu
                db.tbSANPHAM.Add(product);
                db.SaveChanges();

                // Xử lý các file ảnh
                foreach (var file in fileInputs)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                        file.SaveAs(path);

                        // Tạo mới đối tượng tbHINHANH và thêm vào cơ sở dữ liệu
                        var newImage = new tbHINHANH
                        {
                            MASANPHAM = product.MASANPHAM, // Sử dụng ID sản phẩm mới được thêm
                            HINHANH = fileName // Lưu tên file (hoặc đường dẫn)
                        };
                        db.tbHINHANH.Add(newImage);
                    }
                }

                db.SaveChanges(); // Lưu tất cả thay đổi vào cơ sở dữ liệu
                return RedirectToAction("Index", "Home", new { area = "Admin" }); // Chuyển hướng đến trang danh sách sau khi thêm
            }

            // Trường hợp form không hợp lệ, hiển thị lại form với dữ liệu hiện tại
            ViewBag.Categories = db.tbDANHMUC.ToList(); // Đảm bảo ViewBag.Categories được cập nhật
            return View(product);
        }

        // GET: /Admin/Products/Edit/{id}
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm có id tương ứng
            var product = db.tbSANPHAM.Find(id);

            if (product == null)
            {
                // Trả về một trang lỗi hoặc thông báo nếu sản phẩm không tồn tại
                return HttpNotFound();
            }
            ViewBag.Categories = db.tbDANHMUC.ToList();
            ViewBag.Images = db.tbHINHANH.ToList();
            // Trả về trang chỉnh sửa sản phẩm và truyền dữ liệu sản phẩm đến view
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(tbSANPHAM updatedProduct, HttpPostedFileBase[] newImages)
        {
            if (ModelState.IsValid)
            {
                int productID = updatedProduct.MASANPHAM;
                // Lấy sản phẩm cũ từ cơ sở dữ liệu
                var existingProduct = db.tbSANPHAM.Find(productID);

                if (existingProduct != null)
                {
                    // Cập nhật thông tin sản phẩm với dữ liệu mới từ biểu mẫu
                    existingProduct.TENSANPHAM = updatedProduct.TENSANPHAM;
                    existingProduct.MOTA = updatedProduct.MOTA;
                    existingProduct.SIZE = updatedProduct.SIZE;
                    existingProduct.MAU = updatedProduct.MAU;
                    existingProduct.DONGIA = updatedProduct.DONGIA;
                    existingProduct.MADANHMUC = updatedProduct.MADANHMUC;
                    existingProduct.SOLUONG = updatedProduct.SOLUONG;

                    var imageNamesToKeep = new List<string>(); // Danh sách tên các ảnh cũ giữ lại

                    foreach (var oldImage in existingProduct.tbHINHANH)
                    {
                        imageNamesToKeep.Add(oldImage.HINHANH);
                    }

                    // Lấy danh sách ảnh cũ mà bạn muốn giữ lại
                    var imagesToKeep = existingProduct.tbHINHANH.Where(oldImage => imageNamesToKeep.Contains(oldImage.HINHANH)).ToList();

                    // Xoá toàn bộ ảnh cũ của sản phẩm
                    existingProduct.tbHINHANH.Clear();

                    if (newImages == null)
                    {
                        // thêm các ảnh cũ muốn giữ lại vào sản phẩm
                        foreach (var imageToKeep in imagesToKeep)
                        {
                            existingProduct.tbHINHANH.Add(imageToKeep);
                        }
                    }
                    else
                    {
                        // Thêm các ảnh mới vào sản phẩm                    
                        foreach (var file in newImages)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                var path = Path.Combine(Server.MapPath("~/images/"), fileName);
                                file.SaveAs(path);

                                // Tạo mới đối tượng tbHINHANH và thêm vào cơ sở dữ liệu
                                var newImage = new tbHINHANH
                                {
                                    MASANPHAM = updatedProduct.MASANPHAM, // Sử dụng ID sản phẩm mới được thêm
                                    HINHANH = fileName // Lưu tên file (hoặc đường dẫn)
                                };

                                db.tbHINHANH.Add(newImage);
                            }
                        }

                        // thêm các ảnh cũ muốn giữ lại vào sản phẩm
                        foreach (var imageToKeep in imagesToKeep)
                        {
                            existingProduct.tbHINHANH.Add(imageToKeep);
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    // Xử lý trường hợp sản phẩm không tồn tại
                    return RedirectToAction("NotFound");
                }
            }

            // Nếu ModelState không hợp lệ, quay lại trang chỉnh sửa và hiển thị thông báo lỗi
            ViewBag.Categories = db.tbDANHMUC.ToList();
            return View(updatedProduct);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            var productToDelete = db.tbSANPHAM.Find(id);
            if (productToDelete != null)
            {
                // Lấy danh sách hình ảnh có ID sản phẩm tương ứng
                var imagesToDelete = db.tbHINHANH.Where(image => image.MASANPHAM == id).ToList();

                // Xoá tất cả các hình ảnh liên quan đến sản phẩm
                foreach (var image in imagesToDelete)
                {
                    db.tbHINHANH.Remove(image);
                }

                // Xoá sản phẩm và lưu thay đổi vào cơ sở dữ liệu
                db.tbSANPHAM.Remove(productToDelete);
                db.SaveChanges();

                return RedirectToAction("Index", "Home", new { area = "Admin" }); // Chuyển về trang chủ sau khi xoá
            }
            return HttpNotFound(); // Trả về HTTP 404 nếu sản phẩm không tồn tại
        }

        [HttpPost]
        public ActionResult DeleteSelectedProducts(string selectedIds)
        {
            if (!string.IsNullOrEmpty(selectedIds))
            {
                var idArray = selectedIds.Split(',').Select(int.Parse).ToList();

                foreach (var productId in idArray)
                {
                    var productToDelete = db.tbSANPHAM.Find(productId);
                    if (productToDelete != null)
                    {
                        // Lấy danh sách hình ảnh có ID sản phẩm tương ứng
                        var imagesToDelete = db.tbHINHANH.Where(image => image.MASANPHAM == productId).ToList();

                        // Xoá tất cả các hình ảnh liên quan đến sản phẩm
                        foreach (var imageToDelete in imagesToDelete)
                        {
                            db.tbHINHANH.Remove(imageToDelete);
                        }

                        // Xoá sản phẩm
                        db.tbSANPHAM.Remove(productToDelete);
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home", new { area = "Admin" });  // Chuyển về trang chủ sau khi xoá
        }





        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.tbSANPHAM.ToList() ?? new List<tbSANPHAM>(); // Đảm bảo không null
            var categories = db.tbDANHMUC.ToList() ?? new List<tbDANHMUC>(); // Đảm bảo không null
            ViewBag.Categories = categories;
            return View("Index", products);
        }
    }
}