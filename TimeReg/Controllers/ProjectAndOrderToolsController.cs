using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeReg;

namespace TimeReg.Controllers
{
    public class ProjectAndOrderToolsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: ProjectAndOrderTools
        public ActionResult Index()
        {
            return View(db.VI_ProjectAndOrderTools.ToList());
        }

        // GET: ProjectAndOrderTools/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_ProjectAndOrderTools vI_ProjectAndOrderTools = db.VI_ProjectAndOrderTools.Find(id);
            if (vI_ProjectAndOrderTools == null)
            {
                return HttpNotFound();
            }
            return View(vI_ProjectAndOrderTools);
        }

        // GET: ProjectAndOrderTools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectAndOrderTools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Organization,TimeTypeName,TaskTypeName,CustomerRefName,RequesterName,TurbineName,ProductName")] VI_ProjectAndOrderTools vI_ProjectAndOrderTools)
        {
            if (ModelState.IsValid)
            {
                //Check with Mik if this approach is "safe"
                if (vI_ProjectAndOrderTools.Organization != null )
                {
                    var org = new RequestOrg { Organization = vI_ProjectAndOrderTools.Organization };
                    db.RequestOrg.Add(org);
                }
                if (vI_ProjectAndOrderTools.TimeTypeName != null)
                {
                    var timeType = new TimeType { Name = vI_ProjectAndOrderTools.TimeTypeName };
                    db.TimeType.Add(timeType);
                }
                if (vI_ProjectAndOrderTools.TaskTypeName != null)
                {
                    var taskType = new TaskType { Name = vI_ProjectAndOrderTools.TaskTypeName };
                    db.TaskType.Add(taskType);

                }
                if (vI_ProjectAndOrderTools.CustomerRefName != null)
                {
                    var customerReference = new CustomerRef { Name = vI_ProjectAndOrderTools.CustomerRefName };
                    db.CustomerRef.Add(customerReference);
                }
                if (vI_ProjectAndOrderTools.RequesterName != null)
                {
                    var requester = new Requester { Name = vI_ProjectAndOrderTools.RequesterName };
                    db.Requester.Add(requester);
                }
                if (vI_ProjectAndOrderTools.TurbineName != null)
                {
                    var turbineEntity = new Turbine { TurbineName = vI_ProjectAndOrderTools.TurbineName };
                    db.Turbine.Add(turbineEntity);
                }
                if (vI_ProjectAndOrderTools.ProductName != null)
                {
                    var platformOrProduct = new PlatformOrProduct { ProductName = vI_ProjectAndOrderTools.ProductName };
                    db.PlatformOrProduct.Add(platformOrProduct);

                }

                //db.VI_ProjectAndOrderTools.Add(vI_ProjectAndOrderTools);
                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges

                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State); 
                        foreach (var ve in eve.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }

            return View(vI_ProjectAndOrderTools);
        }

        // GET: ProjectAndOrderTools/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_ProjectAndOrderTools vI_ProjectAndOrderTools = db.VI_ProjectAndOrderTools.Find(id);
            if (vI_ProjectAndOrderTools == null)
            {
                return HttpNotFound();
            }
            return View(vI_ProjectAndOrderTools);
        }

        // POST: ProjectAndOrderTools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Organization,TimeTypeName,TaskTypeName,CustomerRefName,RequesterName,TurbineName,ProductName")] VI_ProjectAndOrderTools vI_ProjectAndOrderTools)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vI_ProjectAndOrderTools).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vI_ProjectAndOrderTools);
        }

        // GET: ProjectAndOrderTools/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_ProjectAndOrderTools vI_ProjectAndOrderTools = db.VI_ProjectAndOrderTools.Find(id);
            if (vI_ProjectAndOrderTools == null)
            {
                return HttpNotFound();
            }
            return View(vI_ProjectAndOrderTools);
        }

        // POST: ProjectAndOrderTools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            VI_ProjectAndOrderTools vI_ProjectAndOrderTools = db.VI_ProjectAndOrderTools.Find(id);
            db.VI_ProjectAndOrderTools.Remove(vI_ProjectAndOrderTools);
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
