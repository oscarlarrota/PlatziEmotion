using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmotionPlatzi.Models;

namespace EmotionPlatzi.Controllers
{
    public class EmoPicturesAPIController : ApiController
    {
        private EmotionPlatziContext db = new EmotionPlatziContext();

        // GET: api/EmoPicturesAPI
        public IQueryable<Emopicture> GetEmopictures()
        {
            return db.Emopictures;
        }

        // GET: api/EmoPicturesAPI/5
        [ResponseType(typeof(Emopicture))]
        public IHttpActionResult GetEmopicture(int id)
        {
            Emopicture emopicture = db.Emopictures.Find(id);
            if (emopicture == null)
            {
                return NotFound();
            }

            return Ok(emopicture);
        }

        // PUT: api/EmoPicturesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmopicture(int id, Emopicture emopicture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emopicture.Id)
            {
                return BadRequest();
            }

            db.Entry(emopicture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmopictureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmoPicturesAPI
        [ResponseType(typeof(Emopicture))]
        public IHttpActionResult PostEmopicture(Emopicture emopicture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Emopictures.Add(emopicture);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = emopicture.Id }, emopicture);
        }

        // DELETE: api/EmoPicturesAPI/5
        [ResponseType(typeof(Emopicture))]
        public IHttpActionResult DeleteEmopicture(int id)
        {
            Emopicture emopicture = db.Emopictures.Find(id);
            if (emopicture == null)
            {
                return NotFound();
            }

            db.Emopictures.Remove(emopicture);
            db.SaveChanges();

            return Ok(emopicture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmopictureExists(int id)
        {
            return db.Emopictures.Count(e => e.Id == id) > 0;
        }
    }
}