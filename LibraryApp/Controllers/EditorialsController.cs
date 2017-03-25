using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryApp.Models;
using System;

namespace LibraryApp.Controllers
{
    public class EditorialsController : Controller
    {
        private LibraryAppContext db = new LibraryAppContext();

        public ActionResult Index()
        {
            return View(db.Editorials.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Editorial editorial = db.Editorials.Find(id);

            if (editorial == null)
            {
                return HttpNotFound();
            }

            return View(editorial);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Editorial editorial)
        {
            if (ModelState.IsValid)
            {
                db.Editorials.Add(editorial);

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "La editorial no puede ser guardada porque existe una con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            return View(editorial);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Editorial editorial = db.Editorials.Find(id);

            if (editorial == null)
            {
                return HttpNotFound();
            }

            return View(editorial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Editorial editorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(editorial).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "La editorial no puede ser guardada porque existe una con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            return View(editorial);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Editorial editorial = db.Editorials.Find(id);

            if (editorial == null)
            {
                return HttpNotFound();
            }

            return View(editorial);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Editorial editorial = db.Editorials.Find(id);
            db.Editorials.Remove(editorial);
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
