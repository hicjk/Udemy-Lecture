﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Udemy_ASPNETCORE_MVC_6.DataAccess;
using Udemy_ASPNETCORE_MVC_6.Models;

namespace Udemy_ASPNETCORE_MVC_6.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: HomeController1
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> objCategoryList = await _db.Categories.ToListAsync();

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
        public async Task<IActionResult> Create(Category model)
        {
            //Custom Validation
            if(model.Name.Equals(model.DisplayOrder.ToString()))
            {
                ModelState.AddModelError(nameof(model.Name), "The DisplayOrder cannot exactly match the Name.");
            }

            //Server Side Validation
            if(!ModelState.IsValid)
            {
                return View();
            }

            _db.Categories.Add(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "Category Created Successfully";

            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = await _db.Categories.FindAsync(id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category model)
        {
            //Custom Validation
            if(model.Name.Equals(model.DisplayOrder.ToString()))
            {
                ModelState.AddModelError(nameof(model.Name), "The DisplayOrder cannot exactly match the Name.");
            }

            //Server Side Validation
            if(!ModelState.IsValid)
            {
                return View();
            }

            _db.Categories.Update(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "Category Updated Successfully";

            return RedirectToAction(nameof(Index));
        }

        // GET: HomeController1/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = await _db.Categories.FindAsync(id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var model = await _db.Categories.FindAsync(id);

            if(model is null)
            {
                return NotFound();
            }

            _db.Categories.Remove(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "Category Deleted Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
