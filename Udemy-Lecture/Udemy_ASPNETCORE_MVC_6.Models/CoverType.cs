using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy_ASPNETCORE_MVC_6.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Cover Type")]
        public string Name { get; set; } = string.Empty;
    }
}
