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
    public class TimeTypeController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: TimeType
        public ActionResult Index()
        {
            var timeType = (from VI_TimeType in db.VI_TimeType
                            select new TimeTypeViewModel()
                            {
                                PK_Id = VI_TimeType.PK_Id,
                                Name = VI_TimeType.Name
                            });

            return View(timeType.ToList());
        }

        // GET: TimeType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_TimeType vI_TimeType = db.VI_TimeType.SingleOrDefault(m => m.PK_Id == id);
            var timeTypeViewModel = new TimeTypeViewModel { PK_Id = vI_TimeType.PK_Id, Name = vI_TimeType.Name };
            if (timeTypeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(timeTypeViewModel);
        }

        // GET: TimeType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TimeType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,Name")] TimeTypeViewModel timeTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                db.SP_AddTimeType(timeTypeViewModel.Name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeTypeViewModel);
        }

        // GET: TimeType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_TimeType vI_TimeType = db.VI_TimeType.SingleOrDefault(m => m.PK_Id == id);
            var timeTypeViewModel = new TimeTypeViewModel { PK_Id = vI_TimeType.PK_Id, Name = vI_TimeType.Name };
            if (timeTypeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(timeTypeViewModel);
        }

        // POST: TimeType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,Name")] TimeTypeViewModel timeTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                db.SP_UpdateTimeType(timeTypeViewModel.PK_Id, timeTypeViewModel.Name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timeTypeViewModel);
        }

        // GET: TimeType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_TimeType vI_TimeType = db.VI_TimeType.SingleOrDefault(m => m.PK_Id == id);
            var timeTypeViewModel = new TimeTypeViewModel { PK_Id = vI_TimeType.PK_Id, Name = vI_TimeType.Name };
            if (timeTypeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(timeTypeViewModel);
        }

        // POST: TimeType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VI_TimeType vI_TimeType = db.VI_TimeType.SingleOrDefault(m => m.PK_Id == id);
            var timeTypeViewModel = new TimeTypeViewModel { PK_Id = vI_TimeType.PK_Id, Name = vI_TimeType.Name };
            db.SP_RemoveTimeType(timeTypeViewModel.PK_Id);
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
