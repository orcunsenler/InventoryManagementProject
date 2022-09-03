using InventoryManagementProject.Data;
using InventoryManagementProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using X.PagedList;

namespace InventoryManagementProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int page = 1)
        {
            IEnumerable<Product> objProductList = _db.Products.Include(x =>x.Category).ToList();

            return View(objProductList.ToPagedList(page, 3));
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> values = (from x in _db.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {
            _db.Products.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var productFromDb = _db.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product obj)
        {
            ModelState["Category"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                _db.Products.Update(obj);
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
            //var productFromDb = _db.Products.Find(id);
            //var productFromDbFirst = _db.Products.FirstOrDefault(u=>u.ProductId==id);
            var productFromDbSingle = _db.Products.SingleOrDefault(u => u.ProductId == id);

            if (productFromDbSingle == null)
            {
                return NotFound();
            }
            return View(productFromDbSingle);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Product obj)
        {
            var productFromDb = _db.Products.Find(obj.ProductId);

            if (productFromDb == null)
            {
                return NotFound();
            }
            _db.Products.Remove(productFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
