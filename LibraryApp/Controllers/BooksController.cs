using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryApp.Models;
using System;
using LibraryApp.Classes;

namespace LibraryApp.Controllers
{
    public class BooksController : Controller
    {
        private LibraryAppContext db = new LibraryAppContext();

        public ActionResult Index()
        {
            var books = db.Books.OrderBy(b => b.Title).Include(b => b.BookType).Include(b => b.Editorial).Include(b => b.Writer);
            return View(books.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            ViewBag.BookTypeId = new SelectList(db.BookTypes, "BookTypeId", "Description");
            ViewBag.EditorialId = new SelectList(db.Editorials, "EditorialId", "Description");
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "Name");
            return View();
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookView bookView)
        {
            bool photoExist = true;
            string count = string.Empty;
            int i = 0;

            if (ModelState.IsValid)
            {
                string picture = string.Empty;
                string folder = "~/Content/Photos/Books";

                if (bookView.PhotoFile != null)
                {
                    while (photoExist)
                    {
                        i++;
                        count = Convert.ToString(i);

                        photoExist = System.IO.File.Exists(Server.MapPath($"{folder}/{count}{bookView.PhotoFile.FileName}"));
                    }

                    if (count == "0")
                    {
                        count = string.Empty;
                    }

                    picture = FilesHelper.UploadPhoto(bookView.PhotoFile, folder, count);
                    picture = string.Format($"{folder}/{count}{picture}");
                }
                else
                {
                    picture = "Default.gif";
                    folder = "~/Content/Photos/Books";
                    picture = string.Format($"{folder}/{picture}");
                }

                Book book = ToBook(bookView);
                book.Photo = picture;
                db.Books.Add(book);

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("_Title_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "El libro no puede ser guardado porque existe uno con el mismo título y la misma edición.");
                    }
                    else if (ex.InnerException.InnerException.Message.Contains("_ISBN_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "El libro no puede ser guardado porque existe uno con el mismo isbn.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            ViewBag.BookTypeId = new SelectList(db.BookTypes, "BookTypeId", "Description", bookView.BookTypeId);
            ViewBag.EditorialId = new SelectList(db.Editorials, "EditorialId", "Description", bookView.EditorialId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "Name", bookView.WriterId);
            return View(bookView);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            ViewBag.BookTypeId = new SelectList(db.BookTypes, "BookTypeId", "Description", book.BookTypeId);
            ViewBag.EditorialId = new SelectList(db.Editorials, "EditorialId", "Description", book.EditorialId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "Name", book.WriterId);
            return View(ToBookView(book));
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookView bookView)
        {
            bool photoExist = true;
            string count = string.Empty;
            string lastPhoto = bookView.Photo;
            int i = 0;

            if (ModelState.IsValid)
            {
                string picture = bookView.Photo;
                string folder = "~/Content/Photos/Books";

                if (bookView.PhotoFile != null)
                {
                    while (photoExist)
                    {
                        i++;
                        count = Convert.ToString(i);
                        photoExist = System.IO.File.Exists(Server.MapPath($"{folder}/{count}{bookView.PhotoFile.FileName}"));
                    }

                    if (count == "0")
                    {
                        count = string.Empty;
                    }

                    picture = FilesHelper.UploadPhoto(bookView.PhotoFile, folder, count);
                    picture = string.Format($"{folder}/{count}{picture}");
                }

                Book book = ToBook(bookView);
                book.Photo = picture;


                if (lastPhoto != picture && lastPhoto != "~/Content/Photos/Books/Default.gif")
                {
                    DeletePhoto(lastPhoto);
                }

                db.Entry(book).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "El libro no puede ser guardado porque existe uno con el mismo título y la misma edición.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                }
            }

            ViewBag.BookTypeId = new SelectList(db.BookTypes, "BookTypeId", "Description", bookView.BookTypeId);
            ViewBag.EditorialId = new SelectList(db.Editorials, "EditorialId", "Description", bookView.EditorialId);
            ViewBag.WriterId = new SelectList(db.Writers, "WriterId", "Name", bookView.WriterId);
            return View(bookView);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);

            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(book);
        }

        public ActionResult DeletePhoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Book book = db.Books.Find(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(ToBookView(book));
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoto(BookView bookView)
        {
            string picture = "Default.gif";
            string folder = "~/Content/Photos/Books";

            picture = string.Format($"{folder}/{picture}");

            Book book = ToBook(bookView);
            book.Photo = picture;

            db.Entry(book).State = EntityState.Modified;

            try
            {
                DeletePhoto(bookView.Photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(ToBookView(book));
        }

        private Book ToBook(BookView bookView)
        {
            return new Book
            {
                BookId = bookView.BookId,
                BookTypeId = bookView.BookTypeId,
                DateOfRelease = bookView.DateOfRelease,
                Edition = bookView.Edition,
                ISBN = bookView.ISBN,
                Photo = bookView.Photo,
                Plot = bookView.Plot,
                Title = bookView.Title,
                WriterId = bookView.WriterId,
                EditorialId = bookView.EditorialId
            };
        }

        private BookView ToBookView(Book book)
        {
            return new BookView
            {
                BookId = book.BookId,
                BookTypeId = book.BookTypeId,
                DateOfRelease = book.DateOfRelease,
                Edition = book.Edition,
                EditorialId = book.EditorialId,
                ISBN = book.ISBN,
                Photo = book.Photo,
                Plot = book.Plot,
                Title = book.Title,
                WriterId = book.WriterId
            };
        }

        private void DeletePhoto(string photo)
        {
            if (System.IO.File.Exists(Server.MapPath(photo)))
            {
                System.IO.File.Delete(Server.MapPath(photo));
            }
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
