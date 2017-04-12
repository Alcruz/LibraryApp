using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryApp.Models;
using System;
using LibraryApp.Classes;

namespace LibraryApp.Controllers
{
    public class WritersController : Controller
    {
        private LibraryAppContext db;

        public WritersController()
        {
            db = new LibraryAppContext();
        }

        public ActionResult Index()
        {
            return View(db.Writers.OrderBy(w => w.Name).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Writer writer = db.Writers.Find(id);

            if (writer == null)
            {
                return HttpNotFound();
            }

            return View(writer);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WriterView writerView)
        {
            bool photoExist = true;
            string count = string.Empty;
            int i = 0;

            if (ModelState.IsValid)
            {
                string picture = string.Empty;
                string folder = "~/Content/Photos/Writers";

                if (writerView.PhotoFile != null)
                {
                    while (photoExist)
                    {
                        i++;
                        count = Convert.ToString(i);

                        photoExist = System.IO.File.Exists(Server.MapPath($"{folder}/{count}{writerView.PhotoFile.FileName}"));
                    }

                    if (count == "0")
                    {
                        count = string.Empty;
                    }

                    picture = FilesHelper.UploadPhoto(writerView.PhotoFile, folder, count);
                    picture = string.Format($"{folder}/{count}{picture}");
                }
                else
                {
                    picture = "Default.gif";
                    folder = "~/Content/Photos/Writers";
                    picture = string.Format($"{folder}/{picture}");
                }
                    
                Writer writer = ToWriter(writerView);
                writer.Photo = picture;
                db.Writers.Add(writer);

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "El escritor/a no puede ser guardado/a porque existe un/a con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            return View(writerView);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Writer writer = db.Writers.Find(id);

            if (writer == null)
            {
                return HttpNotFound();
            }

            return View(ToWriterView(writer));
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WriterView writerView)
        {
            bool photoExist = true;
            string count = string.Empty;
            string lastPhoto = writerView.Photo;
            int i = 0;

            if (ModelState.IsValid)
            {
                string picture = writerView.Photo;
                string folder = "~/Content/Photos/Writers";

                if (writerView.PhotoFile != null)
                {
                    while (photoExist)
                    {
                        i++;
                        count = Convert.ToString(i);
                        photoExist = System.IO.File.Exists(Server.MapPath($"{folder}/{count}{writerView.PhotoFile.FileName}"));
                    }

                    if (count == "0")
                    {
                        count = string.Empty;
                    }

                    picture = FilesHelper.UploadPhoto(writerView.PhotoFile, folder, count);
                    picture = string.Format($"{folder}/{count}{picture}");
                }

                Writer writer = ToWriter(writerView);
                writer.Photo = picture;

                if (lastPhoto != picture && lastPhoto != "~/Content/Photos/Writers/Default.gif")
                {
                    DeletePhoto(lastPhoto);
                }

                db.Entry(writer).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("_Index"))
                    {
                        ModelState.AddModelError(string.Empty, "El escritor/a no puede ser guardado/a porque existe un/a con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            return View(writerView);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Writer writer = db.Writers.Find(id);

            if (writer == null)
            {
                return HttpNotFound();
            }

            return View(writer);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Writer writer = db.Writers.Find(id);
            db.Writers.Remove(writer);

            try
            {
                DeletePhoto(writer.Photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(writer);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult DeletePhoto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Writer writer = db.Writers.Find(id);

            if (writer == null)
            {
                return HttpNotFound();
            }

            return View(ToWriterView(writer));
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoto(WriterView writerView)
        {
            string picture = "Default.gif";
            string folder = "~/Content/Photos/Writers";

            picture = string.Format($"{folder}/{picture}");

            Writer writer = ToWriter(writerView);
            writer.Photo = picture;

            db.Entry(writer).State = EntityState.Modified;

            try
            {
                DeletePhoto(writerView.Photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(writerView);
        }

        private Writer ToWriter(WriterView writerView)
        {
            return new Writer
            {
                Name = writerView.Name,
                Biography = writerView.Biography,
                WriterId = writerView.WriterId,
                Book = writerView.Book,
                Photo = writerView.Photo
            };
        }

        private WriterView ToWriterView(Writer writer)
        {
            return new WriterView
            {
                Name = writer.Name,
                Biography = writer.Biography,
                WriterId = writer.WriterId,
                Book = writer.Book,
                Photo = writer.Photo
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
