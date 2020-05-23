using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models
{
    public class AdminCategory
    {
        [Required]
        public int Id { get; set; }
        [Required,MinLength(2, ErrorMessage ="Minimum Length 2 only ")]
        
        public string Name { get; set; }
        [Required]

        public string Slug { get; set; }
        [Required]

        public int Sorting { get; set; }


    }
}
