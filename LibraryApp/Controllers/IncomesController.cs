using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryApp.Models;
using System;

namespace LibraryApp.Controllers
{
    public class IncomesController : Controller
    {
        private LibraryAppContext db;

        public IncomesController()
        {
            db = new LibraryAppContext();
        }

        public ActionResult Index()
        {
            var incomes = db.Incomes.Include(i => i.Book).OrderBy(i => i.Date).Include(i => i.Supplier);
            return View(incomes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Income income = db.Incomes.Find(id);

            if (income == null)
            {
                return HttpNotFound();
            }

            return View(income);
        }

        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Income income)
        {
            if (ModelState.IsValid)
            {
                db.Incomes.Add(income);

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", income.BookId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Description", income.SupplierId);
            return View(income);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Income income = db.Incomes.Find(id);

            if (income == null)
            {
                return HttpNotFound();
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", income.BookId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Description", income.SupplierId);
            return View(income);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Income income)
        {
            if (ModelState.IsValid)
            {
                db.Entry(income).State = EntityState.Modified;
                
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", income.BookId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Description", income.SupplierId);
            return View(income);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Income income = db.Incomes.Find(id);

            if (income == null)
            {
                return HttpNotFound();
            }

            return View(income);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Income income = db.Incomes.Find(id);
            db.Incomes.Remove(income);

            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(income);
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
