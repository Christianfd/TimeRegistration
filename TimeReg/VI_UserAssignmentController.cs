using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace TimeReg
{
    public class VI_UserAssignmentController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: VI_UserAssignment
        public ActionResult Index()
        {
            return View(db.VI_UserAssignment.ToList());
        }

        // GET: VI_UserAssignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_UserAssignment vI_UserAssignment = db.VI_UserAssignment.Find(id);
            if (vI_UserAssignment == null)
            {
                return HttpNotFound();
            }
            return View(vI_UserAssignment);
        }

        // GET: VI_UserAssignment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VI_UserAssignment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId,UserName,NK_ZId,ProjectName")] VI_UserAssignment vI_UserAssignment)
        {
            if (ModelState.IsValid)
            {
                db.VI_UserAssignment.Add(vI_UserAssignment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vI_UserAssignment);
        }

        // GET: VI_UserAssignment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_UserAssignment vI_UserAssignment = db.VI_UserAssignment.Find(id);
            if (vI_UserAssignment == null)
            {
                return HttpNotFound();
            }
            return View(vI_UserAssignment);
        }

        // POST: VI_UserAssignment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId,UserName,NK_ZId,ProjectName")] VI_UserAssignment vI_UserAssignment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vI_UserAssignment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vI_UserAssignment);
        }

        // GET: VI_UserAssignment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_UserAssignment vI_UserAssignment = db.VI_UserAssignment.Find(id);
            if (vI_UserAssignment == null)
            {
                return HttpNotFound();
            }
            return View(vI_UserAssignment);
        }

        // POST: VI_UserAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VI_UserAssignment vI_UserAssignment = db.VI_UserAssignment.Find(id);
            db.VI_UserAssignment.Remove(vI_UserAssignment);
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
