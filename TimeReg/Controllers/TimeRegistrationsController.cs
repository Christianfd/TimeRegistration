using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeReg;

namespace TimeReg.Controllers
{
    public class TimeRegistrationsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: TimeRegistrations
        public ActionResult Index()
        {
            var timeRegistration = db.TimeRegistration.Include(t => t.Projects).Include(t => t.TaskType).Include(t => t.Users);
            return View(timeRegistration.ToList());
        }

        // GET: TimeRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeRegistration timeRegistration = db.TimeRegistration.Find(id);
            if (timeRegistration == null)
            {
                return HttpNotFound();
            }
            return View(timeRegistration);
        }

        // GET: TimeRegistrations/Create
        public ActionResult Create()
        {
            ViewBag.FK_ProjectId = new SelectList(db.Projects, "PK_Id", "Name");
            ViewBag.FK_TaskId = new SelectList(db.TaskType, "PK_Id", "Name");
            ViewBag.FK_UserId = new SelectList(db.Users, "PK_Id", "NK_Name");
            return View();
        }

     

        // POST: TimeRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId,FK_TaskId,time,Date,DateEntry")] TimeRegistration timeRegistration)
        {
            if (ModelState.IsValid)
            {
                db.TimeRegistration.Add(timeRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_ProjectId = new SelectList(db.Projects, "PK_Id", "Name", timeRegistration.FK_ProjectId);
            ViewBag.FK_TaskId = new SelectList(db.TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.Users, "PK_Id", "NK_Name", timeRegistration.FK_UserId);
            return View(timeRegistration);
        }

        // GET: TimeRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeRegistration timeRegistration = db.TimeRegistration.Find(id);
            if (timeRegistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_ProjectId = new SelectList(db.Projects, "PK_Id", "Name", timeRegistration.FK_ProjectId);
            ViewBag.FK_TaskId = new SelectList(db.TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.Users, "PK_Id", "NK_Name", timeRegistration.FK_UserId);
            return View(timeRegistration);
        }

        // POST: TimeRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId,FK_TaskId,time,Date,DateEntry")] TimeRegistration timeRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_ProjectId = new SelectList(db.Projects, "PK_Id", "Name", timeRegistration.FK_ProjectId);
            ViewBag.FK_TaskId = new SelectList(db.TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.Users, "PK_Id", "NK_Name", timeRegistration.FK_UserId);
            return View(timeRegistration);
        }

        // GET: TimeRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeRegistration timeRegistration = db.TimeRegistration.Find(id);
            if (timeRegistration == null)
            {
                return HttpNotFound();
            }
            return View(timeRegistration);
        }

        // POST: TimeRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeRegistration timeRegistration = db.TimeRegistration.Find(id);
            db.TimeRegistration.Remove(timeRegistration);
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
