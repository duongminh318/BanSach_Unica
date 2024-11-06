using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using Microsoft.AspNetCore.Mvc;

namespace BanSachWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        //tạo 1 biến 
        private readonly IUnitOfWork _unitOfWork;

        //hàm khởi tạo
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //tạo một biến hứng dl
            IEnumerable<CoverType> objcovertypeList = _unitOfWork.coverType.GetAll();
            return View(objcovertypeList);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost] // nhận dl tu form
        [ValidateAntiForgeryToken]  // chống giả mạo pt post
        public IActionResult Create(CoverType obj)
        {/*
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder không được trùng với Name");
            }*/
            if (ModelState.IsValid)
            {
                _unitOfWork.coverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType created successfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        // lấy ra  1 đối tượng với id
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // kiếm id 
            // var categoryFromDb = _db.Categories.Find(id);
            var coverTypeFromDbFirst = _unitOfWork.coverType.GetFirstOrDefault(u => u.Id == id);
            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }
            return View(coverTypeFromDbFirst);
        }
        // xử lý Edit

        [HttpPost] // nhận dl tu form
        [ValidateAntiForgeryToken]  // chống giả mạo pt post
        public IActionResult Edit(CoverType obj)
        {
           /* if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder không được trùng với Name");
            }*/
            if (ModelState.IsValid)
            {
                _unitOfWork.coverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType updated successfully";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        // lấy ra  1 đối tượng với id
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // kiếm id
            var CoverTypeFromDb = _unitOfWork.coverType.GetFirstOrDefault(u => u.Id == id);
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(CoverTypeFromDb);
        }
        // xử lý Edit

        [HttpPost] // nhận dl tu form
        [ValidateAntiForgeryToken]  // chống giả mạo pt post
        public IActionResult DeletePost(int? id)
        {

            // kiếm đối tượng theo id
            var CoverTypeFromDb = _unitOfWork.coverType.GetFirstOrDefault(u => u.Id == id);
            if (CoverTypeFromDb == null)
            {
                return NotFound();
            }
            else
            {

                _unitOfWork.coverType.Remove(CoverTypeFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Delete successfully";
                return RedirectToAction("index");
            }

            return View(CoverTypeFromDb);

        }
    }
}
