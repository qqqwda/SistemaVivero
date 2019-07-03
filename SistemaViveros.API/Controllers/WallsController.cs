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
    public class WallsController : ApiController
    {
        private BackendDbContext db = new BackendDbContext();

        // GET: api/Walls
        public IQueryable<Wall> GetWalls()
        {
            return db.Walls;
        }

        // GET: api/Walls/5
        [ResponseType(typeof(Wall))]
        public async Task<IHttpActionResult> GetWall(int id)
        {
            Wall wall = await db.Walls.FindAsync(id);
            if (wall == null)
            {
                return NotFound();
            }

            return Ok(wall);
        }

        // PUT: api/Walls/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWall(int id, Wall wall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wall.WallId)
            {
                return BadRequest();
            }

            db.Entry(wall).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WallExists(id))
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

        // POST: api/Walls
        [ResponseType(typeof(Wall))]
        public async Task<IHttpActionResult> PostWall(Wall wall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Walls.Add(wall);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = wall.WallId }, wall);
        }

        // DELETE: api/Walls/5
        [ResponseType(typeof(Wall))]
        public async Task<IHttpActionResult> DeleteWall(int id)
        {
            Wall wall = await db.Walls.FindAsync(id);
            if (wall == null)
            {
                return NotFound();
            }

            db.Walls.Remove(wall);
            await db.SaveChangesAsync();

            return Ok(wall);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WallExists(int id)
        {
            return db.Walls.Count(e => e.WallId == id) > 0;
        }
    }
}