using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using BanSach.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;


namespace BanSachWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //tạo 1 biến 
        private readonly IUnitOfWork _unitOfWork;

        //tạo môi trường lưu hình
        private readonly IWebHostEnvironment _webHostEnvironment;

        //hàm khởi tạo
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        //hàm khởi tạo
        /* public ProductController(IUnitOfWork unitOfWork)
         {
             _unitOfWork = unitOfWork;
         }*/
        public IActionResult Index()
        {
            //tạo một biến hứng dl
            IEnumerable<Product> objcovertypeList = _unitOfWork.Product.GetAll();
            return View(objcovertypeList);
        }

        // lấy ra  1 đối tượng với id
        public IActionResult Upsert(int? id)
        {

           /* Product product = new Product();
            // lấy ra danhs sách Category, coverType dự vào Id trong product
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });


            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.coverType.GetAll().Select(
                u => new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

*/
            ProductVM productVM = new ProductVM();

            productVM.product = new Product();

            productVM.CategoryList = _unitOfWork.Category.GetAll().Select(
              u => new SelectListItem()
              {
                  Text = u.Name,
                  Value = u.Id.ToString()
              });

            productVM.CoverTypeList = _unitOfWork.coverType.GetAll().Select(
              u => new SelectListItem()
              {
                  Text = u.Name,
                  Value = u.Id.ToString()
              });


            if (id == null || id == 0)
            {

                //create Product
                //viewbag
                //ViewBag.CategoryList = CategoryList;
                // viewdata
              //  ViewData["CoverTypeList"] = CoverTypeList;
                return View(productVM);
            }
            else
            {
                // update


                productVM.product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            }

            // kiếm id 
            // var categoryFromDb = _db.Categories.Find(id);

            return View(productVM);
        }
        // 



        [HttpPost] // nhận dl tu form
        [ValidateAntiForgeryToken]  // chống giả mạo pt post
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {
            //tiến hành update
            if (ModelState.IsValid)
            {
                //upload Images
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products\"); //
                    var extension = Path.GetExtension(file.FileName);
                    if (obj.product.ImageUrl != null)
                    {
                        //nếu đã tồn tại rồi
                        var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams =                        // coppy file
                        new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (obj.product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                }


                _unitOfWork.Save();
                TempData["success"] = "Product Create Successfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

     


        // Api get All Product
        #region API_Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,coverType");
            return Json(new { data = productList });
        }

        #endregion



        /*   public IActionResult DeletePost(int? id)
           {

               // kiếm đối tượng theo id
               var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
               if (obj == null)
               {
                   return NotFound();
               }
               else
               {

                   if (obj.ImageUrl != null)
                   {


                       if (obj.ImageUrl != null)
                       {
                           string wwwRootPath = _webHostEnvironment.WebRootPath;
                           // Delete the old image
                           var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                           if (System.IO.File.Exists(oldImagePath))
                           {
                               System.IO.File.Delete(oldImagePath);
                           }
                       }
                   }

                       _unitOfWork.Product.Remove(obj);
                   _unitOfWork.Save();

                   return Json(new { success = true, message = "Delete Successful" });
               }

               return View(obj);

           }*/
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            if (obj.ImageUrl != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }



    }

}
