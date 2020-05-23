using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin")]

    public class CategoriesController : Controller
    {
        private CmsShoppingCartContext context;

        public CategoriesController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            IQueryable<AdminCategory> adminCategories = from catogeries in context.adminCategories
                                                        orderby catogeries.Sorting
                                                        select catogeries;
            List<AdminCategory> adminCategories1 = await adminCategories.ToListAsync();

            return View(adminCategories1.ToList());
        }
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        //admin/pages/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminCategory adminCategory)
        {
            if (ModelState.IsValid)
            {


                adminCategory.Slug = adminCategory.Name;
                adminCategory.Sorting = 200;

                var slug = await context.pages.FirstOrDefaultAsync(x => x.Slug == adminCategory.Name);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Title Already Exists");
                    return View(adminCategory);
                }
                context.Add(adminCategory);
                await context.SaveChangesAsync();

                TempData["Success"] = "Page Has been Created";
                return RedirectToAction("Index");

            }
            return View(adminCategory);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AdminCategory adminCategory = await context.adminCategories.FindAsync(id);
            if (adminCategory == null)
            {
                return NotFound();
            }
            return View(adminCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AdminCategory adminCategory)
        {
            if (ModelState.IsValid)
            {


                adminCategory.Slug = adminCategory.Name.ToLower().Replace("", "-").Trim();

                var slug = await context.adminCategories.Where(x => x.Id != adminCategory.Id).FirstOrDefaultAsync(x => x.Slug == adminCategory.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Title Already Exists");
                    return View(adminCategory);
                }
                context.Add(adminCategory);
                await context.SaveChangesAsync();

                TempData["Success"] = "Page Has been Created";
                return RedirectToAction("Edit", new { id = adminCategory.Id });

            }
            return View(adminCategory);
        }

    }
}