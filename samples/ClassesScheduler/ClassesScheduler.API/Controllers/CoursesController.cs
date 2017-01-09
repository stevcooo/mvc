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

namespace ClassesScheduler.API.Controllers
{
    public class CoursesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Courses/5
        [HttpGet]
        [Route("api/courses")]
        [ResponseType(typeof(ICollection<Course>))]
        public async Task<IHttpActionResult> GetAllCourses()
        {
            var courses = await db.Courses.ToListAsync();
            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
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