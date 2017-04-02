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
using Employees.Models;

namespace Employees.Controllers
{
  //  [RoutePrefix("api/EmployeeEntities")]
    public class EmployeeEntitiesController : ApiController
    {
        private EFModel1 db = new EFModel1();

        // GET: api/EmployeeEntities
        [Route("api/employeeentities")]
        [HttpGet]
        public IQueryable<EmployeeEntity> GetEmployeeEntities()
        {
            return db.EmployeeEntities.Include("jobTitleEntity");
        }

        // GET: api/EmployeeEntities/5
        [HttpGet]
        [Route("api/employeeentities/{id}")]
        [ResponseType(typeof(EmployeeEntity))]
        public async Task<IHttpActionResult> GetEmployeeEntity(Guid id)
        {
            EmployeeEntity classEntity = await db.EmployeeEntities.FindAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            return Ok(classEntity);
        }

        // PUT: api/EmployeeEntities/5
        [HttpPut]
        [Route("api/employeeentities/{id}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployeeEntity(Guid id, EmployeeEntity classEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            classEntity.ID = id;
            
            db.Entry(classEntity).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeEntityExists(id))
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

        // POST: api/EmployeeEntities
        [HttpPost]
        [Route("api/employeeentities")]
        [ResponseType(typeof(EmployeeEntity))]
        public async Task<IHttpActionResult> PostEmployeeEntity(EmployeeEntity classEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            classEntity.JobTitleEntity = db.JobTitleEntities.FirstOrDefault(d => d.ID == classEntity.JobTitleEntityId);
            
            db.EmployeeEntities.Add(classEntity);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeEntityExists(classEntity.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(classEntity.ID); //CreatedAtRoute("DefaultApi", new { id = classEntity.ID }, classEntity);
        }

        // DELETE: api/EmployeeEntities/5
        [HttpDelete]
        [Route("api/employeeentities/{id}")]
        [ResponseType(typeof(EmployeeEntity))]
        public async Task<IHttpActionResult> DeleteEmployeeEntity(Guid id)
        {
            EmployeeEntity classEntity = await db.EmployeeEntities.FindAsync(id);
            if (classEntity == null)
            {
                return NotFound();
            }

            db.EmployeeEntities.Remove(classEntity);
            await db.SaveChangesAsync();

            return Ok(classEntity);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeEntityExists(Guid id)
        {
            return db.EmployeeEntities.Count(e => e.ID == id) > 0;
        }
    }
}