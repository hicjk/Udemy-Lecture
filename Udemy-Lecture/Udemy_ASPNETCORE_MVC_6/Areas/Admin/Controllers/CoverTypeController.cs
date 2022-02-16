using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy_ASPNETCORE_MVC_6.DataAccess;
using Udemy_ASPNETCORE_MVC_6.Models;

namespace Udemy_ASPNETCORE_MVC_6.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CoverTypeController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: HomeController1
        public async Task<IActionResult> Index()
        {
            IEnumerable<CoverType> objCategoryList = await _db.CoverTypes.ToListAsync();

            return View(objCategoryList);
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
        public async Task<IActionResult> Create(CoverType model)
        {
            //Server Side Validation
            if(!ModelState.IsValid)
            {
                return View();
            }

            _db.CoverTypes.Add(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "CoverType Created Successfully";

            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDb = await _db.CoverTypes.FindAsync(id);

            if(coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CoverType model)
        {
            //Server Side Validation
            if(!ModelState.IsValid)
            {
                return View();
            }

            _db.CoverTypes.Update(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "CoverType Updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController1/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var coverTypeFromDb = await _db.CoverTypes.FindAsync(id);

            if(coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var model = await _db.CoverTypes.FindAsync(id);

            if(model is null)
            {
                return NotFound();
            }

            _db.CoverTypes.Remove(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "CoverType Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
