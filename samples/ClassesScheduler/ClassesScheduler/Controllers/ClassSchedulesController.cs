using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassesScheduler.Models;

namespace ClassesScheduler.Controllers
{
    public class ClassSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ClassSchedules
        public ActionResult Index()
        {
            var classSchedules = db.ClassSchedules.Include(c => c.Course).Include(c => c.SemestarSchedule);
            return View(classSchedules.ToList());
        }

        // GET: ClassSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            return View(classSchedule);
        }

        // GET: ClassSchedules/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name");

            IEnumerable<SelectListItem> semesters = db.SemestarSchedules
                                                   .Select(c => new SelectListItem
                                                   {
                                                       Value = c.Id.ToString(),                                                       
                                                       Text = c.Year.ToString() +" | "+ c.SemesterType.ToString() + " | " + c.StudyField.ToString()                                                       
                                                   });
            ViewBag.SemestarScheduleId = semesters;
            return View();
        }

        // POST: ClassSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ClassromCode,FromHour,ToHour,WeekDay,CourseId,SemestarScheduleId")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                db.ClassSchedules.Add(classSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", classSchedule.CourseId);
            ViewBag.SemestarScheduleId = new SelectList(db.SemestarSchedules, "Id", "YearOfStudy", classSchedule.SemestarScheduleId);
            return View(classSchedule);
        }

        // GET: ClassSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", classSchedule.CourseId);
            ViewBag.SemestarScheduleId = new SelectList(db.SemestarSchedules, "Id", "YearOfStudy", classSchedule.SemestarScheduleId);
            return View(classSchedule);
        }

        // POST: ClassSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ClassromCode,FromHour,ToHour,WeekDay,CourseId,SemestarScheduleId")] ClassSchedule classSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Name", classSchedule.CourseId);
            ViewBag.SemestarScheduleId = new SelectList(db.SemestarSchedules, "Id", "YearOfStudy", classSchedule.SemestarScheduleId);
            return View(classSchedule);
        }

        // GET: ClassSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            if (classSchedule == null)
            {
                return HttpNotFound();
            }
            return View(classSchedule);
        }

        // POST: ClassSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassSchedule classSchedule = db.ClassSchedules.Find(id);
            db.ClassSchedules.Remove(classSchedule);
            db.SaveChanges();
            return RedirectToAction("Index");
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
