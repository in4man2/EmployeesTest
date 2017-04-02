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
    //  [RoutePrefix("api/options")]
    public class OptionsController : ApiController
    {
        private EFModel1 db = new EFModel1();

        // GET: api/options/JobTitles
        [Route("api/options/JobTitles")]
        [HttpGet]
        public IQueryable<JobTitleEntity> JobTitles()
        {
            return db.JobTitleEntities;
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