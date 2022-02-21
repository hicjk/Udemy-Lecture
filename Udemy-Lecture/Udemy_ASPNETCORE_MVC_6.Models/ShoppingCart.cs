using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Udemy_ASPNETCORE_MVC_6.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        [Range(1, 1000, ErrorMessage = "Plase Enter a value between 1 and 1000" )]
        public int Count { get; set; }
    }
}
