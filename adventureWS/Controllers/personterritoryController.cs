using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using adventureWS.Models;

namespace adventureWS.Controllers
{
    public class personterritoryController : ApiController
    {
        private Models.adventureworks2022Entities db = new Models.adventureworks2022Entities();

        //GET
        public IQueryable<personalterritory> GetPersonalterritories()
        {
            return db.personalterritory;
        }

        //GET
        public IHttpActionResult GetPersonal (int id)
        {
            personalterritory personalterritory = db.personalterritory.Find(id);
            if (personalterritory == null)
            {
                return NotFound();
            }
            return Ok(personalterritory);
        }

        //
        public IHttpActionResult PutTerritory (int id, personalterritory personalterritory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != personalterritory.businessEntityId)
            {
                return BadRequest();
            }

            db.Entry(personalterritory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException ex)
            {
                if (!personalTerritoryExists(id))
                {
                    return NotFound();
                } else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult PostPersonalTerritory (personalterritory personalterritory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.personalterritory.Add(personalterritory);
            try
            {
                db.SaveChanges();
            } catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.InnerException;
                if (innerException != null)
                {
                    return BadRequest(innerException.Message);
                } else
                {
                    return BadRequest(ex.Message);
                }
            }
            return CreatedAtRoute("DefaultApi", new {id = personalterritory.businessEntityId }, personalterritory);
        }

        public IHttpActionResult DeletePersonalTerritory(int id)
        {
            personalterritory personalterritory = db.personalterritory.Find(id);
            if (personalterritory == null)
            {
                return NotFound();
            }
            db.personalterritory.Remove(personalterritory);
            db.SaveChanges();

            return Ok(personalterritory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool personalTerritoryExists (int id)
        {
            return db.personalterritory.Count(e => e.businessEntityId == id) > 0;
        }
    }
}
