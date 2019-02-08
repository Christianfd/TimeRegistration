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
    public class TimeRegistrationsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: TimeRegistrations
        public ActionResult Index()
        {
            var timeRegistrationViewModel = new TimeRegistrationViewModel(db.VI_TimeRegistration.ToList());
            return View(timeRegistrationViewModel);
        }

        // GET: TimeRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_TimeRegistration timeRegistrationView = db.VI_TimeRegistration.SingleOrDefault(m => m.PK_Id == id);
            TimeRegistrationViewModel timeRegistration = new TimeRegistrationViewModel(timeRegistrationView);
            if (timeRegistration == null)
            {
                return HttpNotFound();
            }
            return View(timeRegistration);
        }

        // GET: TimeRegistrations/Create
        public ActionResult Create()
        {
            ViewBag.TestValue = 10;
            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name");
            ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number");
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name");
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name");
            return View();
        }

     

        // POST: TimeRegistrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId,FK_OrderId,FK_TaskId,time,Date,Comment")] TimeRegistrationViewModel timeRegistration)
        {
            timeRegistration.DateEntry = DateTime.Now;
            if (ModelState.IsValid)
            {
                if (timeRegistration.Comment == null) { timeRegistration.Comment = "No Comment"; }
             
                db.SP_AddTimeRegistration(timeRegistration.FK_UserId, timeRegistration.FK_ProjectId,timeRegistration.FK_OrderId, timeRegistration.FK_TaskId, timeRegistration.Time, timeRegistration.Date, DateTime.Now, timeRegistration.Comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", timeRegistration.FK_ProjectId);
            ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", timeRegistration.FK_OrderId);
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistration.FK_UserId);
            return View(timeRegistration);
        }

        // GET: TimeRegistrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_TimeRegistration timeRegistration = db.VI_TimeRegistration.SingleOrDefault(m => m.PK_Id == id);
            var timeRegistrationViewModel = new TimeRegistrationViewModel(timeRegistration);
            if (timeRegistrationViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", timeRegistrationViewModel.FK_ProjectId);
            ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", timeRegistration.FK_OrderId);
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistrationViewModel.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistrationViewModel.FK_UserId);
            return View(timeRegistrationViewModel);
        }

        // POST: TimeRegistrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId,FK_OrderId,FK_TaskId,time,Date, Comment")] TimeRegistrationViewModel timeRegistration)
        {
            if (ModelState.IsValid)
            {
                db.SP_UpdateTimeRegistration(timeRegistration.PK_Id, timeRegistration.FK_UserId, timeRegistration.FK_ProjectId, timeRegistration.FK_OrderId, timeRegistration.FK_TaskId, timeRegistration.Time, timeRegistration.Date, DateTime.Now, timeRegistration.Comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        

            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", timeRegistration.FK_ProjectId);
            ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", timeRegistration.FK_OrderId);
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistration.FK_UserId);
            return View(timeRegistration);
        }

        // GET: TimeRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_TimeRegistration timeRegistration = db.VI_TimeRegistration.SingleOrDefault(m => m.PK_Id == id);
            var timeRegistrationViewModel = new TimeRegistrationViewModel(timeRegistration);
            if (timeRegistration == null)
            {
                return HttpNotFound();
            }
            return View(timeRegistrationViewModel);
        }

        // POST: TimeRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //VI_TimeRegistration timeRegistration = db.VI_TimeRegistration.SingleOrDefault(m => m.PK_Id == id);
        
            db.SP_RemoveTimeRegistration(id);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        //[HttpPost]
        //[STAThread]
        //public void ClipboardMethod()
        //{
        //    String Clipboard;
        //    Clipboard.SetText("boo yah!", TextDataFormat.Html);
        //}

        [HttpGet]
        public ActionResult AjaxDyanimcDropDown(int dropDownKey, string dropDownId)
        {
            var key = (int)dropDownKey;
            var id = (string)dropDownId;
            
            if (id == "FK_OrderId")
            {
                var ProjectList = new SelectList(db.VI_Projects.Where(m => m.FK_OrderNumber == key), "PK_Id", "Name");
                return Json(ProjectList, JsonRequestBehavior.AllowGet);
            }

            if (id == "FK_UserId")
            {
                var OrderNumberList = new SelectList(db.VI_UserAssignment.Where(m => m.FK_UserId == key), "FK_OrderNumber", "Number");
                return Json(OrderNumberList, JsonRequestBehavior.AllowGet);
            }

            return null;
         
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
