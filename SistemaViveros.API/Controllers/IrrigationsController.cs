using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SistemaViveros.API.Models;
using SistemaViveros.Common.Models;

namespace SistemaViveros.API.Controllers
{
    public class IrrigationsController : ApiController
    {
        private BackendDbContext db = new BackendDbContext();

        // GET: api/Irrigations
        public IQueryable<Irrigation> GetIrrigations()
        {
            return db.Irrigations.Where(i => i.WallId.Equals(19));
        }



        // GET: api/Irrigations/5
        [ResponseType(typeof(Irrigation))]
        public async Task<IHttpActionResult> GetIrrigation(int id)
        {
            Irrigation irrigation = await db.Irrigations.FindAsync(id);
            if (irrigation == null)
            {
                return NotFound();
            }

            return Ok(irrigation);
        }

        // PUT: api/Irrigations/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIrrigation(int id, Irrigation irrigation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != irrigation.IrrigationId)
            {
                return BadRequest();
            }

            db.Entry(irrigation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IrrigationExists(id))
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

        // POST: api/Irrigations
        [ResponseType(typeof(Irrigation))]
        public async Task<IHttpActionResult> PostIrrigation(Irrigation irrigation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Irrigations.Add(irrigation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = irrigation.IrrigationId }, irrigation);
        }

        // DELETE: api/Irrigations/5
        [ResponseType(typeof(Irrigation))]
        public async Task<IHttpActionResult> DeleteIrrigation(int id)
        {
            Irrigation irrigation = await db.Irrigations.FindAsync(id);
            if (irrigation == null)
            {
                return NotFound();
            }

            db.Irrigations.Remove(irrigation);
            await db.SaveChangesAsync();

            return Ok(irrigation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IrrigationExists(int id)
        {
            return db.Irrigations.Count(e => e.IrrigationId == id) > 0;
        }
    }
}