using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryApp.Models;

namespace LibraryApp.Controllers
{
    public class BookSellersController : Controller
    {
        private LibraryAppContext db = new LibraryAppContext();

        // GET: BookSellers
        public ActionResult Index()
        {
            var bookSellers = db.BookSellers.Include(b => b.Book);
            return View(bookSellers.ToList());
        }

        // GET: BookSellers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookSeller bookSeller = db.BookSellers.Find(id);
            if (bookSeller == null)
            {
                return HttpNotFound();
            }
            return View(bookSeller);
        }

        // GET: BookSellers/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            return View();
        }

        // POST: BookSellers/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookSellerId,BookId,Quantity,Avaliable")] BookSeller bookSeller)
        {
            if (ModelState.IsValid)
            {
                db.BookSellers.Add(bookSeller);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", bookSeller.BookId);
            return View(bookSeller);
        }

        // GET: BookSellers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookSeller bookSeller = db.BookSellers.Find(id);
            if (bookSeller == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", bookSeller.BookId);
            return View(bookSeller);
        }

        // POST: BookSellers/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookSellerId,BookId,Quantity,Avaliable")] BookSeller bookSeller)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookSeller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", bookSeller.BookId);
            return View(bookSeller);
        }

        // GET: BookSellers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookSeller bookSeller = db.BookSellers.Find(id);
            if (bookSeller == null)
            {
                return HttpNotFound();
            }
            return View(bookSeller);
        }

        // POST: BookSellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookSeller bookSeller = db.BookSellers.Find(id);
            db.BookSellers.Remove(bookSeller);
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
