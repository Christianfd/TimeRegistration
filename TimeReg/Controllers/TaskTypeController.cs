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
    public class TaskTypeController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: TaskTypes
        public ActionResult Index()
        {
            var taskType = (from VI_TaskType in db.VI_TaskType
                            select new TaskTypeViewModel()
                            {
                                PK_Id = VI_TaskType.PK_Id,
                                Name = VI_TaskType.Name
                            });
           
            return View(taskType.ToList());
        }

        // GET: TaskTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //TaskType taskType = db.TaskType.Find(id); -- Replaced by below code to use sql Views for security reasons
            var taskType = db.VI_TaskType.SingleOrDefault(m => m.PK_Id == id);
            TaskTypeViewModel taskTypeViewModel = new TaskTypeViewModel(taskType.PK_Id, taskType.Name);
            if (taskType == null)
            {
                return HttpNotFound();
            }
            return View(taskTypeViewModel);
        }

        // GET: TaskTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskTypeViewModel taskType)
        {
            if (ModelState.IsValid)
            {
                db.SP_AddTaskType(taskType.Name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskType);
        }

        // GET: TaskTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TaskType taskType = db.TaskType.Find(id); -- Replaced by below code for security reasons
            var taskType = db.VI_TaskType.SingleOrDefault(m => m.PK_Id == id);
            TaskTypeViewModel taskTypeViewModel = new TaskTypeViewModel(taskType.PK_Id, taskType.Name);

            if (taskTypeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(taskTypeViewModel);
        }

        // POST: TaskTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,Name")] TaskTypeViewModel taskTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                db.SP_UpdateTaskType(taskTypeViewModel.PK_Id, taskTypeViewModel.Name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskTypeViewModel);
        }

        // GET: TaskTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //TaskType taskType = db.TaskType.Find(id); -- Replaced by below code to use sql Views for security reasons
            var taskType = db.VI_TaskType.SingleOrDefault(m => m.PK_Id == id);
            TaskTypeViewModel taskTypeViewModel = new TaskTypeViewModel(taskType.PK_Id, taskType.Name);
            if (taskType == null)
            {
                return HttpNotFound();
            }
            return View(taskTypeViewModel);
        }

        // POST: TaskTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VI_TaskType taskType = db.VI_TaskType.SingleOrDefault(m => m.PK_Id == id);
            //Not Sure if this needs more security        
            db.SP_RemoveTaskType(id);
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
