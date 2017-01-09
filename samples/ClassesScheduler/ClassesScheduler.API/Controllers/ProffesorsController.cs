using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ClassesScheduler.API.Models;
using ClassesScheduler.Models;
using System.Collections.Generic;

namespace ClassesScheduler.API.Controllers
{
    public class ProffesorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Proffesors
        public IQueryable<Proffesor> GetProffesors()
        {
            return db.Proffesors;
        }

        [HttpGet]
        [Route("api/proffesors")]
        [ResponseType(typeof(ICollection<Proffesor>))]
        public async Task<IHttpActionResult> GetAllProffesors()
        {
            var proffesors = await db.Proffesors.ToListAsync();
            if (proffesors == null)
            {
                return NotFound();
            }

            return Ok(proffesors);
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