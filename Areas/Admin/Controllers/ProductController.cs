using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsShoppingCart.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
   
    [Area("Admin")]
    //[Route("admin")]
    public class ProductController : Controller
    {
        private CmsShoppingCartContext context;

        public ProductController(CmsShoppingCartContext context)
        {
            this.context = context;
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}