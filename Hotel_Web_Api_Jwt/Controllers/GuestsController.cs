using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Include;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Hotel_Web_Api_Jwt.Models;
using ThenInclude.EF6;

namespace Hotel_Web_Api_Jwt.Controllers
{
    public class GuestsController : ApiController
    {
        private GuestDb db = new GuestDb();

        // GET: api/Guests
        public IHttpActionResult GetGuests()
        {
            var data = db.Guests
                .Including(r => r.reservations)
                .ThenInclude(s => s.Room).ToList();

            return Ok(data);
        }

        // GET: api/Guests/5
        [ResponseType(typeof(Guest))]
        public IHttpActionResult GetGuest(int id)
        {
            Guest guest = db.Guests
                .Including(r => r.reservations)
                .ThenInclude(s => s.Room)
                .FirstOrDefault(e => e.Id == id);
            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        // PUT: api/Guests/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGuest(int id, Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guest.Id)
            {
                return BadRequest();
            }
            db.Reservations.RemoveRange(db.Reservations.Where(i => i.GuestID == guest.Id));

            foreach (var item in guest.reservations)
            {
                item.GuestID = guest.Id;
                db.Reservations.Add(item);
            }
            db.Entry(guest).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Update Success");
        }

        // POST: api/Guests
        [ResponseType(typeof(Guest))]
        public async Task<IHttpActionResult> PostGuest(Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Guests.Add(guest);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = guest.Id }, guest);
        }

        // DELETE: api/Guests/5
        [ResponseType(typeof(Guest))]
        public async Task<IHttpActionResult> DeleteGuest(int id)
        {
            Guest guest = await db.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            db.Guests.Remove(guest);
            await db.SaveChangesAsync();

            return Ok(guest);
        }
        [ResponseType(typeof(string))]
        [HttpPost]
        [Route("~/Guests/UploadImage")]
        public IHttpActionResult UploadImage()
        {

            var upload = HttpContext.Current.Request.Files.Count > 0 ?
        HttpContext.Current.Request.Files[0] : null;


            if (upload is null) return BadRequest();


            string ImageUrl = "/Images/" + Guid.NewGuid() + Path.GetExtension(upload.FileName);


            upload.SaveAs(HttpContext.Current.Server.MapPath(ImageUrl));

            return Ok(ImageUrl);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuestExists(int id)
        {
            return db.Guests.Count(e => e.Id == id) > 0;
        }
    }
}