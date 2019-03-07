using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeReg;
using TimeReg.ViewModels;

namespace TimeReg.Controllers
{
    public class UserAssignmentsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: UserAssignments
        public ActionResult Index()
        {
            //Needs View Implementation
            var windowsAuth = User.Identity.GetUserName();
            var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;

            return View(db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId).ToList());
        }

        // GET: UserAssignments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_UserAssignment userAssignment = db.VI_UserAssignment.SingleOrDefault(m => m.PK_Id == id);
            if (userAssignment == null)
            {
                return HttpNotFound();
            }
            return View(userAssignment);
        }

        // GET: UserAssignments/Create
        public ActionResult Create()
        {
            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name");
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name");
            return View();
        }

        // POST: UserAssignments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId")] UserAssignmentViewModel userAssignment)
        {
            if (ModelState.IsValid)
            {
                try { 
                db.SP_AddUserAssignment(userAssignment.FK_UserId, userAssignment.FK_ProjectId);
                db.SaveChanges();
                return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Exception = ex;
                    ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", userAssignment.FK_ProjectId);
                    ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", userAssignment.FK_UserId);
                    ViewBag.EasyMessage = "User already assigned to that project";
                    if (((SqlException)ex.InnerException).Number == 2627)
                    {
                        
                        return View(userAssignment);
                    } else { throw ex; }
                    
                }
            }

            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", userAssignment.FK_ProjectId);
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", userAssignment.FK_UserId);
            return View(userAssignment);
        }

        // GET: UserAssignments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_UserAssignment userAssignment = db.VI_UserAssignment.SingleOrDefault(m => m.PK_Id == id);
            UserAssignmentViewModel userAssignmentViewModel = new UserAssignmentViewModel(userAssignment);
            if (userAssignment == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", userAssignmentViewModel.FK_ProjectId);
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", userAssignmentViewModel.FK_UserId);
            return View(userAssignmentViewModel);
        }

        // POST: UserAssignments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId")] UserAssignmentViewModel userAssignment)
        {
            if (ModelState.IsValid)
            {
                db.SP_UpdateUserAssignment(userAssignment.PK_Id, userAssignment.FK_UserId, userAssignment.FK_ProjectId);
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
            VI_UserAssignment userAssignment = db.VI_UserAssignment.SingleOrDefault(m => m.PK_Id == id);
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
            db.SP_RemoveUserAssignment(id);
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
