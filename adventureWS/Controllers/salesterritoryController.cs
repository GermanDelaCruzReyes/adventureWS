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
    public class salesterritoryController : ApiController
    {
        private Models.adventureworks2022Entities db = new Models.adventureworks2022Entities();

        //GET Obtener todos los registros de salesTerritory (LISTO)
        /*public IQueryable<salesterritory> GetSalesterritories()
        {
            return db.salesterritory;
        }*/

        //GET Obtener un único registro de salesTerritory por su ID (LISTO)
        public IHttpActionResult GetTerritory(int id)
        {
            salesterritory salesterritory = db.salesterritory.Find(id);
            if (salesterritory == null)
            {
                return NotFound();
            }
            return Ok(salesterritory);
        }

        //PUT
        public IHttpActionResult PutTerritory(int id, salesterritory salesterritory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesterritory.territoryId)
            {
                return BadRequest();
            }

            db.Entry(salesterritory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!salesTerritoryExists(id))
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

        //POST 
        public IHttpActionResult PostSalesTerritory(salesterritory saleTerritory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.salesterritory.Add(saleTerritory);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.InnerException;
                if (innerException != null)
                {
                    return BadRequest(innerException.Message);
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = saleTerritory.territoryId }, saleTerritory);
        }

        //DELETE Eliminar un registro de salesTerritory por su ID (LISTO)
        public IHttpActionResult DeleteSalesTerritory(int id)
        {
            Models.salesterritory salesterritory = db.salesterritory.Find(id);
            if (salesterritory == null)
            {
                return NotFound();
            }
            db.salesterritory.Remove(salesterritory);
            db.SaveChanges();

            return Ok(salesterritory);
        }

        public IHttpActionResult GetTerritorysIds()
        {
            var territoryIds = db.salesterritory.Select(s => s.territoryId).Distinct().ToList();
            return Ok(territoryIds);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //Verificar la existencia de un Territorio por su Id
        private bool salesTerritoryExists(int id)
        {
            return db.salesterritory.Count(e => e.territoryId == id) > 0;
        }

    }

}
