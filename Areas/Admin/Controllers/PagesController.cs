using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CmsShoppingCart.Infrastructure;
using CmsShoppingCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("admin")]
    public class PagesController : Controller
    {
        private CmsShoppingCartContext context;

        public PagesController(CmsShoppingCartContext context)
        {
            this.context = context;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }

        //https://localhost:44345/Admin/Pages/Details/1
        // GET  area/controller/actionmethod/id 
        //GET are/Pages/details/5
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
        //admin/pages/Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        //admin/pages/Create
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Page page)
        {
            if (ModelState.IsValid)
            {


                page.Slug = page.Title;
                page.Sorting = "200";

                var slug = await context.pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Title Already Exists");
                    return View(page);
                }
                context.Add(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "Page Has been Created";
                return RedirectToAction("Index");

            }
            return View(page);
        }

        [HttpGet]
        //GET area/Pages/Edit/5

        public async Task<IActionResult> Edit(int id)
        {
            Page page = await context.pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }
        public async Task<IActionResult> Edit (Page page)
        {
            if (ModelState.IsValid)
            {


                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace("", "-");

                var slug = await context.pages.Where(x=>x.Id!=page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Title Already Exists");
                    return View(page);
                }
                context.Add(page);
                await context.SaveChangesAsync();

                TempData["Success"] = "Page Has been Created";
                return RedirectToAction("Edit",new { id=page.Id});

            }
            return View(page);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Page page = await context.pages.FindAsync(id);
            if (page == null)
            {
                TempData["DeletPage"] = "Page Not Found";

            }
            else
            {
                context.pages.Remove(page);
                await context.SaveChangesAsync();
                TempData["pagesuccess"] = "Page Delted Successuflly";
                return RedirectToAction("Index");
            }

            return View(page);
        }
    }
}