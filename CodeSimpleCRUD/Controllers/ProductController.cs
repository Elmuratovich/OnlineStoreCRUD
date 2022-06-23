using CodeSimpleCRUD.Data;
using CodeSimpleCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CodeSimpleCRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly CodeSimpleContext _context;
        public ProductController(CodeSimpleContext context)
        {
            _context = context;
        }

        public IActionResult Index(int pg = 1)
        {
            List<Product> products = _context.Products.ToList();

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;

            int recsCount = products.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = products.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);

            //return View(products);
        }

        public IActionResult IndexAjax()
        {
            List<Product> products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Product prod = new Product();
            return View(prod);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index"); 
        }

        public IActionResult Details(string id)
        {
            Product prod = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            return View(prod);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            Product prod = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            return View(prod);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            Product prod = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            return View(prod);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        #region "Ajax Functions"

        [HttpPost]
        public IActionResult DeleteProduct(string id)
        {
            Product prod = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            _context.Entry(prod).State = EntityState.Deleted;
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult ViewProduct(string id)
        {
            Product prod = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            return PartialView("_detail", prod);
        }

        public IActionResult EditProduct(string id)
        {
            Product prod = _context.Products.Where(p => p.Code == id).FirstOrDefault();
            return PartialView("_Edit", prod);
        }

        public IActionResult UpdateProduct(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
            return PartialView("_Product", product);
        }

        #endregion

    }
}