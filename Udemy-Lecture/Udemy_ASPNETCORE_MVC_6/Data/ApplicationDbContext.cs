using Microsoft.EntityFrameworkCore;
using Udemy_ASPNETCORE_MVC_6.Models;

namespace Udemy_ASPNETCORE_MVC_6.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
