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
    public class UserAssignmentsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: UserAssignments
        public ActionResult Index()
        {
            var userAssignment = db.UserAssignment.Include(u => u.Projects).Include(u => u.Users);
            return View(userAssignment.ToList());
        }

        // GET: UserAssignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAssignment userAssignment = db.UserAssignment.Find(id);
            if (userAssignment == null)
            {
                return HttpNotFound();
            }
            return View(userAssignment);
        }

        // GET: UserAssignments/Create
        public ActionResult Create()
        {
            ViewBag.FK_ProjectId = new SelectList(db.Projects, "PK_Id", "Name");
            ViewBag.FK_UserId = new SelectList(db.Users, "PK_Id", "NK_Name");
            return View();
        }

        // POST: UserAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId")] UserAssignment userAssignment)
        {
            if (ModelState.IsValid)
            {
                db.UserAssignment.Add(userAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_ProjectId = new SelectList(db.Projects, "PK_Id", "Name", userAssignment.FK_ProjectId);
            ViewBag.FK_UserId = new SelectList(db.Users, "PK_Id", "NK_Name", userAssignment.FK_UserId);
            return View(userAssignment);
        }

        // GET: UserAssignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAssignment userAssignment = db.UserAssignment.Find(id);
            if (userAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_ProjectId = new SelectList(db.Projects, "PK_Id", "Name", userAssignment.FK_ProjectId);
            ViewBag.FK_UserId = new SelectList(db.Users, "PK_Id", "NK_Name", userAssignment.FK_UserId);
            return View(userAssignment);
        }

        // POST: UserAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId")] UserAssignment userAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_ProjectId = new SelectList(db.Projects, "PK_Id", "Name", userAssignment.FK_ProjectId);
            ViewBag.FK_UserId = new SelectList(db.Users, "PK_Id", "NK_Name", userAssignment.FK_UserId);
            return View(userAssignment);
        }

        // GET: UserAssignments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAssignment userAssignment = db.UserAssignment.Find(id);
            if (userAssignment == null)
            {
                return HttpNotFound();
            }
            return View(userAssignment);
        }

        // POST: UserAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAssignment userAssignment = db.UserAssignment.Find(id);
            db.UserAssignment.Remove(userAssignment);
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
