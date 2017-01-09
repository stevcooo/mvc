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
using ClassesScheduler.API.Models;
using ClassesScheduler.Models;
using ClassesScheduler.Enums;

namespace ClassesScheduler.API.Controllers
{
    public class SemestarSchedulesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SemestarSchedules
        public IQueryable<SemestarSchedule> GetSemestarSchedules()
        {
            return db.SemestarSchedules;
        }

        // GET: api/SemestarSchedules/5
        [HttpGet]
        [Route("api/semestarSchedules")]
        [ResponseType(typeof(ICollection<SemestarSchedule>))]        
        public async Task<IHttpActionResult> GetAllSemestarSchedule()
        {
            var semestarSchedules = await db.SemestarSchedules.ToListAsync();
            if (semestarSchedules == null)
            {
                return NotFound();
            }

            return Ok(semestarSchedules);
        }

        [HttpGet]
        [Route("api/semestarSchedules/{year}")]
        [ResponseType(typeof(ICollection<SemestarSchedule>))]
        public async Task<IHttpActionResult> GetSemestarScheduleForYear(int year)
        {
            var semestarSchedules = await db.SemestarSchedules.Where(t=>t.Year == year).ToListAsync();
            if (semestarSchedules == null)
            {
                return NotFound();
            }

            return Ok(semestarSchedules);
        }

        [HttpGet]
        [Route("api/semestarSchedules/{year}/{semesterType}")]
        [ResponseType(typeof(ICollection<SemestarSchedule>))]
        public async Task<IHttpActionResult> GetSemestarScheduleForYear(int year, Semester semesterType)
        {
            var semestarSchedules = await db.SemestarSchedules.Where(t => t.Year == year && t.SemesterType == semesterType).ToListAsync();
            if (semestarSchedules == null)
            {
                return NotFound();
            }

            return Ok(semestarSchedules);
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