using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeReg;
using TimeReg.ViewModels;

namespace TimeReg.Controllers
{
    public class ProjectsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: Projects
        public ActionResult Index()
        {
            
            return View(db.VI_Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projects = db.VI_Projects.SingleOrDefault(M => M.PK_Id == id);
                         
            if (projects == null)
            {
                return HttpNotFound();
            }
            //This needs work, as it takes all current comments instead of only the needed comments
            //This view model is created to pass the comments to the view along with the project details.
            //The Comments also needs an orderby function at some point and mabye a user list.

            var projectsViewModel = new ProjectDetailsViewModel() { VIProjects = projects, VIComments = db.VI_Comments.OrderByDescending(m => m.WeekNr), VITimeSpentPerUser = db.VI_UserTimePerProject.Where(m => m.FK_ProjectId == id) };
            return View(projectsViewModel);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            
            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name");
            ViewBag.FK_OrderNumber = new SelectList(db.VI_OrderNumber, "PK_Id", "Number");
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,Name,FK_Ordernumber,TimeEstimation,FK_ProjectLeader,Scope,FK_TimeType")] ProjectsViewModel projects)
        {
            if (ModelState.IsValid)
            {
         
                db.SP_AddProject(projects.Name,projects.FK_OrderNumber,projects.TimeEstimation, projects.FK_ProjectLeader,projects.Scope, projects.FK_TimeType);
                //db.Projects.Add(projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name", projects.FK_ProjectLeader);
            ViewBag.FK_OrderNumber = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", projects.FK_OrderNumber);
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name", projects.FK_TimeType);

            return View(projects);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            VI_Projects projects = db.VI_Projects.SingleOrDefault(m => m.PK_Id == id);
            var projectsViewModel = new ProjectsViewModel() {PK_Id = projects.PK_Id, Scope = projects.Scope, FK_ProjectLeader = projects.FK_ProjectLeader, TimeEstimation = projects.TimeEstimation, FK_OrderNumber = projects.FK_OrderNumber, Name = projects.Name, UserName = projects.UserName, FK_TimeType = (int)projects.FK_TimeType };
            if (projects == null)
            {
                return HttpNotFound();
            }


            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name", projectsViewModel.FK_ProjectLeader);
            ViewBag.FK_OrderNumber = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", projectsViewModel.FK_OrderNumber);
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name", projectsViewModel.FK_TimeType);

            return View(projectsViewModel);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,Name,FK_OrderNumber,TimeEstimation,FK_ProjectLeader, Scope, FK_TimeType")] ProjectsViewModel projects)
        {
            if (ModelState.IsValid)
            {
                db.SP_UpdateProject(projects.PK_Id, projects.Name, projects.FK_OrderNumber, projects.TimeEstimation, projects.FK_ProjectLeader, projects.Scope, projects.FK_TimeType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PK_Id = new SelectList(db.Comments, "PK_Id", "Text", projects.PK_Id);
            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name");
            ViewBag.FK_OrderNumber = new SelectList(db.VI_OrderNumber, "PK_Id", "Number");
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name");
            return View(projects);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var projects = db.VI_Projects.SingleOrDefault(m => m.PK_Id == id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var projects = db.VI_Projects.SingleOrDefault(m => m.PK_Id == id);
            db.SP_RemoveProject(id);
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
