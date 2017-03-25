﻿using System.Data.Entity;
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
        private LibraryAppContext db = new LibraryAppContext();
        private string folder = "~/Content/Photos/Writers/";

        public ActionResult Index()
        {
            return View(db.Writers.ToList());
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Writer writer)
        {
            if (ModelState.IsValid)
            {
                var picture = string.Empty;

                if (writer.PhotoFile != null)
                {
                    picture = FilesHelper.UploadPhoto(writer.PhotoFile, folder, Convert.ToString(writer.Name));
                }

                picture = string.Format($"{folder}/{writer.Name}.jpg");

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
                        ModelState.AddModelError(string.Empty, "El/la escritor/a no puede ser guardado/a porque existe uno/a con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            return View(writer);
        }

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

            return View(writer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Writer writer)
        {
            if (ModelState.IsValid)
            {
                var picture = string.Empty;

                if (writer.PhotoFile != null)
                {
                    picture = FilesHelper.UploadPhoto(writer.PhotoFile, folder, writer.Name);
                }

                picture = string.Format($"{folder}/{picture}/{writer.Name}.jpg");

                writer.Photo = picture;
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
                        ModelState.AddModelError(string.Empty, "El/la escritor/a no puede ser guardado/a porque existe uno/a con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }

            return View(writer);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Writer writer = db.Writers.Find(id);
            db.Writers.Remove(writer);

            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);              
            }

            return View(writer);
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
