using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.DAL;
using Ecommerce.Models;
using Ecommerce.ViewModels;

namespace Ecommerce.Controllers
{
    public class CategoriesController : Controller
    {
        private EcomContext db = new EcomContext();
        private CRUD crud = new CRUD();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        public ActionResult SaleDetails()
        {
            // 1 Way
            var products = db.Products.Include(p => p.Category).ToList();
            var salesData = new List<SaleDataViewModel>();
            foreach (var product in products)
            {
                var saleData = new SaleDataViewModel()
                {
                    ProductName = product.Name,
                    CategoryName = product.Category.Name,
                    DateStamp = DateTime.Now,
                    Quantity = 1,
                };

                salesData.Add(saleData);
            }

            // 2 Way
            var query1 = from category in db.Categories
                        join product in db.Products on category.Id equals product.CategoryId
                        select new SaleDataViewModel { CategoryName = category.Name, ProductName = product.Name };

            // 3 Way
            var query = db.Products.Include(p => p.Category)
                .Select(s => new SaleDataViewModel
                {
                    ProductName = s.Name,
                    CategoryName = s.Category.Name,
                    DateStamp = DateTime.Now,
                    Quantity = 1,
                }).ToList();

            return View(salesData);
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = crud.FetchCategory(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description")] Category category, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var MaxContentLength = 512 * 512 * 1; //0.5 MB
                var AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png" };

                if (file == null)
                {
                    //ModelState.AddModelError("File", "Please Upload Your file");
                    //ViewBag.message = "Please pick a file for upload!";
                    category.PhotoPath = "/Images/noimage.png";
                }
                else if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    ModelState.AddModelError("File", "Please use file of type: " + string.Join(", ", AllowedFileExtensions));
                    TempData["Message"] = "Wrong file Type";
                    return RedirectToAction("index");
                }
                else if (file.ContentLength > MaxContentLength)
                {
                    ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + 512 + " KB");
                    TempData["Message"] = "Your file is too large, maximum allowed size is: " + 512 + " KB";
                    return RedirectToAction("index");
                }
                else
                {
                    var filename = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), filename);
                    file.SaveAs(path);
                    category.PhotoPath = "/Images/" + filename;
                }

                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,PhotoPath")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
