using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryApp.Models;
using System;

namespace LibraryApp.Controllers
{
    public class BookTypesController : Controller
    {
        private LibraryAppContext db = new LibraryAppContext();

        public ActionResult Index()
        {
            return View(db.BookTypes.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookType bookType = db.BookTypes.Find(id);

            if (bookType == null)
            {
                return HttpNotFound();
            }

            return View(bookType);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                db.BookTypes.Add(bookType);

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "El género no puede ser guardado porque existe uno con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            return View(bookType);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookType bookType = db.BookTypes.Find(id);

            if (bookType == null)
            {
                return HttpNotFound();
            }

            return View(bookType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookType).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "El género no puede ser guardado porque existe uno con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            return View(bookType);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BookType bookType = db.BookTypes.Find(id);

            if (bookType == null)
            {
                return HttpNotFound();
            }

            return View(bookType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookType bookType = db.BookTypes.Find(id);
            db.BookTypes.Remove(bookType);
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
