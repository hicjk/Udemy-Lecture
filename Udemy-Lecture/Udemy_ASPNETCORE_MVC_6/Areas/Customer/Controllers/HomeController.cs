using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Udemy_ASPNETCORE_MVC_6.DataAccess;
using Udemy_ASPNETCORE_MVC_6.Models;
using Udemy_ASPNETCORE_MVC_6.Models.ViewModels;

namespace Udemy_ASPNETCORE_MVC_6.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var productList = _db.Products.Include(m => m.Category).Include(m => m.CoverType).AsNoTracking().AsEnumerable();

            return View(productList);
        }

        public async Task<IActionResult> Details(int id)
        {
            ShoppingCart cartVM = new()
            {
                Count = 1,
                Product = await _db.Products.Include(m => m.Category).Include(m => m.CoverType).FirstOrDefaultAsync(u => u.Id == id)
            };

            return View(cartVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}