using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy_ASPNETCORE_MVC_6.DataAccess;
using Udemy_ASPNETCORE_MVC_6.Models;

namespace Udemy_ASPNETCORE_MVC_6.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: HomeController1
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> objProductList = await _db.Products.ToListAsync();

            return View(objProductList);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model)
        {
            //Server Side Validation
            if(!ModelState.IsValid)
            {
                return View();
            }

            _db.Products.Add(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "Products Created Successfully";

            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var ProductFromDb = await _db.Products.FindAsync(id);

            if(ProductFromDb == null)
            {
                return NotFound();
            }

            return View(ProductFromDb);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model)
        {
            //Server Side Validation
            if(!ModelState.IsValid)
            {
                return View();
            }

            _db.Products.Update(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "Product Updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController1/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var productFromDb = await _db.Products.FindAsync(id);

            if(productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var model = await _db.Products.FindAsync(id);

            if(model is null)
            {
                return NotFound();
            }

            _db.Products.Remove(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "Product Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
