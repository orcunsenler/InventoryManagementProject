using InventoryManagementProject.Data;
using InventoryManagementProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using X.PagedList;

namespace InventoryManagementProject.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int page = 1)
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList.ToPagedList(page, 3));
        }
        public IActionResult Create()
        {

            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            ModelState["Products"].ValidationState = ModelValidationState.Valid;
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.CategoryId==id);
            var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.CategoryId == id);

            if (categoryFromDbSingle == null)
            {
                return NotFound();
            }

            return View(categoryFromDbSingle);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            if (obj.CategoryName == obj.CategoryDescription)
            {
                ModelState.AddModelError("CategoryName", "The Description cannot exactly match the CategoryName");
            }
            ModelState["Products"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var productFromDbFirst = _db.Products.FirstOrDefault(u=>u.ProductId==id);
            //var productFromDbSingle = _db.Products.SingleOrDefault(u => u.ProductId == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Category obj)
        {
            var cat_Id = _db.Categories.Find(obj.CategoryId);

            if (cat_Id == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(cat_Id);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
