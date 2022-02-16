using Microsoft.EntityFrameworkCore;
using Udemy_ASPNETCORE_MVC_6.Models;

#nullable disable

namespace Udemy_ASPNETCORE_MVC_6.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
