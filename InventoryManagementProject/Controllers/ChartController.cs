using InventoryManagementProject.Data;
using InventoryManagementProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementProject.Controllers
{
    public class ChartController : Controller
    {
        private readonly AppDbContext _db;
        public ChartController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult VisualizeProduct()
        {
            return Json(ProductList());
        }
        public IActionResult VisualizeProduct2()
        {
            return Json(ProductList);
        }
        public List<Chart> ProductList()
        {
            List<Chart> charts = new List<Chart>();
            using(var db = _db)
            {
                charts = db.Products.Select(x => new Chart
                {
                    ProductName=x.ProductName,
                    Stock=x.UnitInStock
                }).ToList();
            }
            return charts;
        }

       

    }
}
