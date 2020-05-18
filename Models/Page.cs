using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCart.Models
{
    public class Page
    {
        public int Id { get; set; }
        public int Title { get; set; }

        public int Slug { get; set; }

        public int Content { get; set; }
        public int Sorting { get; set; }



    }
}
