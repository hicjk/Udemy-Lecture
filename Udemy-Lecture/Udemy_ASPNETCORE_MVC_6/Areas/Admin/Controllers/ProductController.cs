using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Udemy_ASPNETCORE_MVC_6.DataAccess;
using Udemy_ASPNETCORE_MVC_6.Models;
using Udemy_ASPNETCORE_MVC_6.Models.ViewModels;

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

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _db.Categories.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }),
                CoverTypeList = _db.CoverTypes.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
            };

            if(id is null || id is 0)
            {

                return View(productVM);
            }
            else
            {

            }

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM model, IFormFile file)
        {
            //Server Side Validation
            if(!ModelState.IsValid)
            {
                return View();
            }

            //_db.CoverTypes.Update(model);
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
