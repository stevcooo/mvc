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
        [Route("api/semestarSchedules/{studyField}/{yearOfStudy}/{semesterType}")]
        [ResponseType(typeof(SemestarSchedule))]
        public async Task<IHttpActionResult> GetSemestarScheduleForYearAndField(StudyField studyField, string yearOfStudy, Semester semesterType)
        {
            var semestarSchedules = await db.SemestarSchedules.FirstOrDefaultAsync(t => t.YearOfStudy == yearOfStudy && t.SemesterType == semesterType && t.StudyField == studyField);
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