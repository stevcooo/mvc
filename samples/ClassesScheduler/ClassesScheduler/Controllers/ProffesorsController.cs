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
    public class ProffesorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Proffesors
        public ActionResult Index()
        {
            return View(db.Proffesors.ToList());
        }

        // GET: Proffesors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proffesor proffesor = db.Proffesors.Find(id);
            if (proffesor == null)
            {
                return HttpNotFound();
            }
            return View(proffesor);
        }

        // GET: Proffesors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proffesors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName")] Proffesor proffesor)
        {
            if (ModelState.IsValid)
            {
                db.Proffesors.Add(proffesor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(proffesor);
        }

        // GET: Proffesors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proffesor proffesor = db.Proffesors.Find(id);
            if (proffesor == null)
            {
                return HttpNotFound();
            }
            return View(proffesor);
        }

        // POST: Proffesors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName")] Proffesor proffesor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proffesor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(proffesor);
        }

        // GET: Proffesors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proffesor proffesor = db.Proffesors.Find(id);
            if (proffesor == null)
            {
                return HttpNotFound();
            }
            return View(proffesor);
        }

        // POST: Proffesors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proffesor proffesor = db.Proffesors.Find(id);
            db.Proffesors.Remove(proffesor);
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
