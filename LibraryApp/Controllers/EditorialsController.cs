using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LibraryApp.Models;
using System;
using LibraryApp.Classes;

namespace LibraryApp.Controllers
{
    [Authorize(Roles = "Admin")]

    public class EditorialsController : Controller
    {
        private LibraryAppContext db;

        public EditorialsController()
        {
            db = new LibraryAppContext();
        }

        public ActionResult Index()
        {
            return View(db.Editorials.OrderBy(e => e.Description).ToList());
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
        public ActionResult Create(EditorialView editorialView)
        {
            bool photoExist = true;
            string count = string.Empty;
            int i = 0;

            if (ModelState.IsValid)
            {
                string picture = string.Empty;
                string folder = "~/Content/Photos/Editorials";

                if (editorialView.PhotoFile != null)
                {
                    while (photoExist)
                    {
                        i++;
                        count = Convert.ToString(i);

                        photoExist = System.IO.File.Exists(Server.MapPath($"{folder}/{count}{editorialView.PhotoFile.FileName}"));
                    }

                    if (count == "0")
                    {
                        count = string.Empty;
                    }

                    picture = FilesHelper.UploadPhoto(editorialView.PhotoFile, folder, count);
                    picture = string.Format($"{folder}/{count}{picture}");
                }
                else
                {
                    picture = "Default.gif";
                    folder = "~/Content/Photos/Editorials";
                    picture = string.Format($"{folder}/{picture}");
                }

                Editorial editorial = ToEditorial(editorialView);
                editorial.Photo = picture;
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

            return View(editorialView);
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

            return View(ToEditorialView(editorial));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditorialView editorialView)
        {
            bool photoExist = true;
            string count = string.Empty;
            string lastPhoto = editorialView.Photo;
            int i = 0;

            if (ModelState.IsValid)
            {
                string picture = editorialView.Photo;
                string folder = "~/Content/Photos/Editorials";

                if (editorialView.PhotoFile != null)
                {
                    while (photoExist)
                    {
                        i++;
                        count = Convert.ToString(i);
                        photoExist = System.IO.File.Exists(Server.MapPath($"{folder}/{count}{editorialView.PhotoFile.FileName}"));
                    }

                    if (count == "0")
                    {
                        count = string.Empty;
                    }

                    picture = FilesHelper.UploadPhoto(editorialView.PhotoFile, folder, count);
                    picture = string.Format($"{folder}/{count}{picture}");
                }

                Editorial editorial = ToEditorial(editorialView);
                editorial.Photo = picture;

                if (lastPhoto != picture && lastPhoto != "~/Content/Photos/Editorials/Default.gif")
                {
                    DeletePhoto(lastPhoto);
                }

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

            return View(editorialView);
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

            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(editorial);
        }

        public ActionResult DeletePhoto(int? id)
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

            return View(ToEditorialView(editorial));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoto(EditorialView editorialView)
        {
            string picture = "Default.gif";
            string folder = "~/Content/Photos/Editorials";

            picture = string.Format($"{folder}/{picture}");

            Editorial editorial = ToEditorial(editorialView);
            editorial.Photo = picture;

            db.Entry(editorial).State = EntityState.Modified;

            try
            {
                DeletePhoto(editorialView.Photo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(ToEditorialView(editorial));
        }

        private Editorial ToEditorial(EditorialView editorialView)
        {
            return new Editorial
            {
                Description = editorialView.Description,
                EditorialId = editorialView.EditorialId,
                Photo = editorialView.Photo,
            };
        }

        private EditorialView ToEditorialView(Editorial editorial)
        {
            return new EditorialView
            {
                Description = editorial.Description,
                EditorialId = editorial.EditorialId,
                Photo = editorial.Photo,
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
