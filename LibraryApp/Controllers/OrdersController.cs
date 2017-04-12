using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryApp.Models;
using System;
using LibraryApp.Classes;

namespace LibraryApp.Controllers
{
    public class OrdersController : Controller
    {
        private LibraryAppContext db;

        public OrdersController()
        {
            db = new LibraryAppContext();
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Index()
        {
            var orders = db.Orders.OrderBy(o => o.OrderDate).Include(o => o.Book).Include(o => o.OrderStatus);
            return View(orders.ToList());
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        [Authorize]

        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title");
            ViewBag.WishStatusId = new SelectList(db.OrderStatus, "WishStatusId", "Description");
            return View();
        }

        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                string body = "Hola, su orden ha sido completada sastifactoriamente,le estaremos respondiendo en las próximas horas cuando puede pasar a buscar el libro.";
                string subject = "Library App";

                order.UserName = User.Identity.Name;
                order.WishStatusId = 3;
                order.OrderDate = DateTime.Now;
                order.StartDate = DateTime.Now;
                order.FinishDate = DateTime.Now;

                db.Orders.Add(order);

                try
                {
                    db.SaveChanges();
                    MailHelper.SendMail(User.Identity.Name, subject, body);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", order.BookId);
            ViewBag.WishStatusId = new SelectList(db.OrderStatus, "WishStatusId", "Description", order.WishStatusId);
            return RedirectToAction("Books", "Index", new { area = "" });
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", order.BookId);
            ViewBag.WishStatusId = new SelectList(db.OrderStatus, "WishStatusId", "Description", order.WishStatusId);
            return View(order);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                int status = order.WishStatusId;

                if (status == 3)
                {
                    order.WishStatusId = 1;

                    Random rnd = new Random();
                    int code = rnd.Next(1000, 99999);
                    string body = $"Hola, su orden ha sido aceptada puede pasar a buscar su libro el día: {order.StartDate} con el código: {code}. Debe devolverlo el día: {order.FinishDate}.";
                    string subject = "Library App";
                    MailHelper.SendMail(User.Identity.Name, subject, body);
                }
                else if (status == 1)
                {
                    order.WishStatusId = 4;
                }
                else if (status == 4)
                {
                    order.WishStatusId = 5;
                }

                db.Entry(order).State = EntityState.Modified;

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

            ViewBag.BookId = new SelectList(db.Books, "BookId", "Title", order.BookId);
            ViewBag.WishStatusId = new SelectList(db.OrderStatus, "WishStatusId", "Description", order.WishStatusId);
            return View(order);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);

            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(order);
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
