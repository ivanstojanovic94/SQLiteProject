using CrudAspNetCoreMVCAndSQLite.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAspNetCoreMVCAndSQLite.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private DatabaseContext db;

        public ProductController(DatabaseContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("~/")]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewBag.products = db.Products.ToList();
            return View();
        }

        [HttpGet]
        [Route("Add")]
        public IActionResult Add()
        {
            return View("Add", new Product());
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int Id)
        {
            db.Products.Remove(db.Products.Find(Id));
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        [Route("edit/{Id}")]
        public IActionResult Edit(int Id)
        {
            var product = db.Products.Find(Id);
            return View("Edit", product);
        }

        [HttpPost]
        [Route("edit/{Id}")]
        public IActionResult Edit(int Id, Product product) {

            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
