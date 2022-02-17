using Microsoft.AspNetCore.Mvc.Rendering;

#nullable disable

namespace Udemy_ASPNETCORE_MVC_6.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}
