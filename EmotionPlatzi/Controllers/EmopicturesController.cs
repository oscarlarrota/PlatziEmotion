using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmotionPlatzi.Models;

namespace EmotionPlatzi.Controllers
{
    public class EmopicturesController : Controller
    {
        private EmotionPlatziContext db = new EmotionPlatziContext();

        // GET: Emopictures
        public ActionResult Index()
        {
            return View(db.Emopictures.ToList());
        }

        // GET: Emopictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emopicture emopicture = db.Emopictures.Find(id);
            if (emopicture == null)
            {
                return HttpNotFound();
            }
            return View(emopicture);
        }

        // GET: Emopictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Emopictures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Path")] Emopicture emopicture)
        {
            if (ModelState.IsValid)
            {
                db.Emopictures.Add(emopicture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emopicture);
        }

        // GET: Emopictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emopicture emopicture = db.Emopictures.Find(id);
            if (emopicture == null)
            {
                return HttpNotFound();
            }
            return View(emopicture);
        }

        // POST: Emopictures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Path")] Emopicture emopicture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emopicture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emopicture);
        }

        // GET: Emopictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emopicture emopicture = db.Emopictures.Find(id);
            if (emopicture == null)
            {
                return HttpNotFound();
            }
            return View(emopicture);
        }

        // POST: Emopictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emopicture emopicture = db.Emopictures.Find(id);
            db.Emopictures.Remove(emopicture);
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
