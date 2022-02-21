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
        private readonly IWebHostEnvironment _hostEnvironmemt;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            //_db.Products.Include(u => u.Category).Include(u => u.CoverType);
            _hostEnvironmemt = hostEnvironment;
        }

        // GET: HomeController1
        public IActionResult Index()
        {
            return View();
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
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
            }
            else
            {
                productVM.Product = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
            }

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM viewModel, IFormFile? file)
        {
            //Server Side Validation
            if(!ModelState.IsValid)
            {
                return View();
            }

            var wwwRootPath = _hostEnvironmemt.WebRootPath;

            if(file is not null)
            {
                var fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(file.FileName);

                if(viewModel.Product.ImageUrl is not null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, viewModel.Product.ImageUrl.TrimStart('\\'));

                    if(System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                using var fileStream = new FileStream(Path.Combine(uploads, $"{fileName}{extension}"), FileMode.Create);

                file.CopyTo(fileStream);

                viewModel.Product.ImageUrl = $@"/images/products/{fileName}{extension}";
            }

            if(viewModel.Product.Id is 0)
            {
                await _db.Products.AddAsync(viewModel.Product);
            }
            else
            {
                _db.Products.Update(viewModel.Product);
            }

            await _db.SaveChangesAsync();

            TempData["success"] = "Product Created Successfully";

            return RedirectToAction(nameof(Index));
        }

        // POST: HomeController1/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var model = await _db.Products.FindAsync(id);

            if(model is null)
            {
                return Json(new { success = false, message = "Error" });
            }

            if(model.ImageUrl is not null)
            {
                var oldImagePath = $"{_hostEnvironmemt.WebRootPath}{model.ImageUrl}";

                if(System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _db.Products.Remove(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "Product Deleted Successfully";

            return Json(new { success = true, message = "Delete Success" });
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _db.Products.AsQueryable().Include(u => u.Category).Include(u => u.CoverType);

            return Json(new { data = productList });
        }
        #endregion
    }
}
