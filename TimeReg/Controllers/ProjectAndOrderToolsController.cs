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
using TimeReg.ViewModels;

namespace TimeReg.Controllers
{
    public class ProjectAndOrderToolsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: ProjectAndOrderTools
        public ActionResult Index()
        {

            return View(db.VI_ProjectAndOrderToolsUnion.ToList());
        }

        [HttpGet]
        public ActionResult UnionIndexTable()
        {
            ViewBag.Requester = db.VI_ProjectAndOrderToolsUnion.Where(m => m.Type == "Requester").ToList();
            ViewBag.RequestOrg = db.VI_ProjectAndOrderToolsUnion.Where(m => m.Type == "RequestOrg").ToList();
            ViewBag.CustomerRef = db.VI_ProjectAndOrderToolsUnion.Where(m => m.Type == "CustomerRef").ToList();
            ViewBag.PlatformOrProduct = db.VI_ProjectAndOrderToolsUnion.Where(m => m.Type == "PlatformOrProduct").ToList();
            ViewBag.Turbine = db.VI_ProjectAndOrderToolsUnion.Where(m => m.Type == "Turbine").ToList();
            ViewBag.TimeType = db.VI_ProjectAndOrderToolsUnion.Where(m => m.Type == "TimeType").ToList();
            ViewBag.TaskType = db.VI_ProjectAndOrderToolsUnion.Where(m => m.Type == "TaskType").ToList();
            var MaxInt = new int[]
            {
                ((IEnumerable<dynamic>)ViewBag.Requester).Count(),
                ((IEnumerable<dynamic>)ViewBag.RequestOrg).Count(),
                ((IEnumerable<dynamic>)ViewBag.PlatformOrProduct).Count(),
                ((IEnumerable<dynamic>)ViewBag.Turbine).Count(),
                ((IEnumerable<dynamic>)ViewBag.TimeType).Count(),
                ((IEnumerable<dynamic>)ViewBag.TaskType).Count()
            };

            ViewBag.MaxInt = MaxInt.Max();
            return PartialView("_UnionIndexTable", db.VI_ProjectAndOrderToolsUnion.ToList());
        }

        // GET: ProjectAndOrderTools/Details/5
        [HttpGet]
        public ActionResult UnionDetails(string CRUD, string type, int PK_Id)
        {

            if (type == null || CRUD != "Details")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectOrderUnionViewModel model = GetViewModelData(type, PK_Id);
            return PartialView("_UnionDetails", model);
        }

        // GET: ProjectAndOrderTools/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult UnionCreate()
        {
            return PartialView("_UnionCreate");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnionCreate([Bind(Include = "Organization,TimeTypeName,TaskTypeName,CustomerRefName,RequesterName,TurbineName,ProductName")] VI_ProjectAndOrderTools vI_ProjectAndOrderTools)
        {
            if (ModelState.IsValid)
            {
                //Check with Mik if this approach is "safe"
                if (vI_ProjectAndOrderTools.Organization != null)
                {
                    var org = new RequestOrg { Organization = vI_ProjectAndOrderTools.Organization };
                    db.SP_AddRequestOrg(org.Organization);
                }
                if (vI_ProjectAndOrderTools.TimeTypeName != null)
                {
                    var timeType = new TimeType { Name = vI_ProjectAndOrderTools.TimeTypeName };
                    db.SP_AddTimeType(timeType.Name);
                }
                if (vI_ProjectAndOrderTools.TaskTypeName != null)
                {
                    var taskType = new TaskType { Name = vI_ProjectAndOrderTools.TaskTypeName };
                    db.SP_AddTaskType(taskType.Name);

                }
                if (vI_ProjectAndOrderTools.CustomerRefName != null)
                {
                    var customerReference = new CustomerRef { Name = vI_ProjectAndOrderTools.CustomerRefName };
                    db.SP_AddCustomerRef(customerReference.Name);
                }
                if (vI_ProjectAndOrderTools.RequesterName != null)
                {
                    var requester = new Requester { Name = vI_ProjectAndOrderTools.RequesterName };
                    db.SP_AddRequester(requester.Name);
                }
                if (vI_ProjectAndOrderTools.TurbineName != null)
                {
                    var turbineEntity = new Turbine { TurbineName = vI_ProjectAndOrderTools.TurbineName };
                    db.SP_AddTurbine(turbineEntity.TurbineName);
                }
                if (vI_ProjectAndOrderTools.ProductName != null)
                {
                    var platformOrProduct = new PlatformOrProduct { ProductName = vI_ProjectAndOrderTools.ProductName };
                    db.SP_AddPlatformOrProduct(platformOrProduct.ProductName);
                }

                try
                {
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
                return PartialView("_UnionCreate");
            }

            return PartialView("_UnionCreate", vI_ProjectAndOrderTools);
        }


        [HttpGet]
        public ActionResult UnionEdit(string CRUD, string type, int PK_Id)
        {
            ProjectOrderUnionViewModel projectOrderUnionViewModel = new ProjectOrderUnionViewModel { };
            if (PK_Id == 0 || type == null || CRUD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            projectOrderUnionViewModel = GetViewModelData(type, PK_Id);

            if (projectOrderUnionViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_UnionEdit", projectOrderUnionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnionEdit(ProjectOrderUnionViewModel model)
        {
            var name = model.Name;
            var type = model.Type;
            var PK_Id = model.PK_Id;

            ProjectOrderUnionViewModel projectOrderUnionViewModel = new ProjectOrderUnionViewModel { };
            if (PK_Id == 0 || type == null || model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            if (ModelState.IsValid)
            {
                switch (type)
                {
                    case "Requester":
                        db.SP_UpdateRequester(PK_Id, model.Name);
                        break;
                    case "RequestOrg":
                        db.SP_UpdateRequestOrg(PK_Id, model.Name);                       
                        break;
                    case "CustomerRef":
                        db.SP_UpdateCustomerRef(PK_Id, model.Name);
                        break;
                    case "PlatformOrProduct":
                        db.SP_UpdatePlatformOrProduct(PK_Id, model.Name);
                        break;
                    case "Turbine":
                        db.SP_UpdateTurbine(PK_Id, model.Name);
                        break;
                    case "TimeType":
                        db.SP_UpdateTimeType(PK_Id, model.Name);
                        break;
                    case "TaskType":
                        db.SP_UpdateTaskType(PK_Id, model.Name);
                        break;
                    default:
                        break;
                }
            }

            projectOrderUnionViewModel = GetViewModelData(type, PK_Id);

            if (projectOrderUnionViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_UnionEdit", projectOrderUnionViewModel);

        }

        // GET: ProjectAndOrderTools/Delete/5
        [HttpGet]
        public ActionResult UnionDelete(string CRUD, int PK_Id, string type)
        {
            if (PK_Id == 0 || CRUD != "Delete" || type == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectOrderUnionViewModel projectOrderUnionViewModel = GetViewModelData(type,PK_Id);
            if (projectOrderUnionViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_UnionDelete", projectOrderUnionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnionDelete (ProjectOrderUnionViewModel model)
        {
            int PK_Id = model.PK_Id;

            if (model != null)
            {
                switch (model.Type)
                {
                    case "Requester":
                        db.SP_RemoveRequester(PK_Id);
                        break;
                    case "RequestOrg":
                        db.SP_RemoveRequestOrg(PK_Id);
                        break;
                    case "CustomerRef":
                        db.SP_RemoveCustomerRef(PK_Id);
                        break;
                    case "PlatformOrProduct":
                        db.SP_RemovePlatformOrProduct(PK_Id);
                        break;
                    case "Turbine":
                        db.SP_RemoveTurbine(PK_Id);
                        break;
                    case "TimeType":
                        db.SP_RemoveTimeType(PK_Id);
                        break;
                    case "TaskType":
                        db.SP_RemoveTaskType(PK_Id);
                        break;
                    default:
                        return PartialView("_DeleteError");
                        break;

                }
                string removed = "Delete Confirmed";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }
           
            return PartialView("_UnionDelete", model);
        }



        //// POST: ProjectAndOrderTools/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    VI_ProjectAndOrderTools vI_ProjectAndOrderTools = db.VI_ProjectAndOrderTools.Find(id);
        //    db.VI_ProjectAndOrderTools.Remove(vI_ProjectAndOrderTools);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ProjectOrderUnionViewModel GetViewModelData(string type, int PK_Id)
        {
            ProjectOrderUnionViewModel projectOrderUnionViewModel = new ProjectOrderUnionViewModel() { };
            //Would like some clean-up here.
            switch (type)
            {
                case "Requester":
                    var vI_Request = db.VI_Requester.SingleOrDefault(m => m.PK_Id == PK_Id);
                    projectOrderUnionViewModel.Name = vI_Request.Name;
                    projectOrderUnionViewModel.Type = type;
                    projectOrderUnionViewModel.PK_Id = vI_Request.PK_Id;
                    return projectOrderUnionViewModel;
                    break;
                case "RequestOrg":
                    var vI_RequestOrg = db.VI_RequestOrg.SingleOrDefault(m => m.PK_Id == PK_Id);
                    projectOrderUnionViewModel.Name = vI_RequestOrg.Organization;
                    projectOrderUnionViewModel.Type = type;
                    projectOrderUnionViewModel.PK_Id = vI_RequestOrg.PK_Id;
                    return projectOrderUnionViewModel;
                    break;
                case "CustomerRef":
                    var vI_CustomerRef = db.VI_CustomerRef.SingleOrDefault(m => m.PK_Id == PK_Id);
                    projectOrderUnionViewModel.Name = vI_CustomerRef.Name;
                    projectOrderUnionViewModel.Type = type;
                    projectOrderUnionViewModel.PK_Id = vI_CustomerRef.PK_Id;
                    return projectOrderUnionViewModel;
                    break;
                case "PlatformOrProduct":
                    var vI_PlatformOrProduct = db.VI_PlatformOrProduct.SingleOrDefault(m => m.PK_Id == PK_Id);
                    projectOrderUnionViewModel.Name = vI_PlatformOrProduct.ProductName;
                    projectOrderUnionViewModel.Type = type;
                    projectOrderUnionViewModel.PK_Id = vI_PlatformOrProduct.PK_Id;
                    return projectOrderUnionViewModel;
                    break;
                case "Turbine":
                    var vI_Turbine = db.VI_Turbine.SingleOrDefault(m => m.PK_Id == PK_Id);
                    projectOrderUnionViewModel.Name = vI_Turbine.TurbineName;
                    projectOrderUnionViewModel.Type = type;
                    projectOrderUnionViewModel.PK_Id = vI_Turbine.PK_Id;
                    return projectOrderUnionViewModel; 
                    break;
                case "TimeType":
                    var vI_TimeType = db.VI_TimeType.SingleOrDefault(m => m.PK_Id == PK_Id);
                    projectOrderUnionViewModel.Name = vI_TimeType.Name;
                    projectOrderUnionViewModel.Type = type;
                    projectOrderUnionViewModel.PK_Id = vI_TimeType.PK_Id;
                    return projectOrderUnionViewModel;
                    break;
                case "TaskType":
                    var vI_TaskType = db.VI_TaskType.SingleOrDefault(m => m.PK_Id == PK_Id);
                    projectOrderUnionViewModel.Name = vI_TaskType.Name;
                    projectOrderUnionViewModel.Type = type;
                    projectOrderUnionViewModel.PK_Id = vI_TaskType.PK_Id;
                    return projectOrderUnionViewModel;
                    break;
                default:
                    return projectOrderUnionViewModel = null;
                    break;
            }
        }


        //// GET: ProjectAndOrderTools/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    VI_ProjectAndOrderTools vI_ProjectAndOrderTools = db.VI_ProjectAndOrderTools.Find(id);
        //    if (vI_ProjectAndOrderTools == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(vI_ProjectAndOrderTools);
        //}


        //Old Actions

        //// POST: ProjectAndOrderTools/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Organization,TimeTypeName,TaskTypeName,CustomerRefName,RequesterName,TurbineName,ProductName")] VI_ProjectAndOrderTools vI_ProjectAndOrderTools)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Check with Mik if this approach is "safe"
        //        if (vI_ProjectAndOrderTools.Organization != null)
        //        {
        //            var org = new RequestOrg { Organization = vI_ProjectAndOrderTools.Organization };
        //            db.SP_AddRequestOrg(org.Organization);
        //        }
        //        if (vI_ProjectAndOrderTools.TimeTypeName != null)
        //        {
        //            var timeType = new TimeType { Name = vI_ProjectAndOrderTools.TimeTypeName };
        //            db.SP_AddTimeType(timeType.Name);
        //        }
        //        if (vI_ProjectAndOrderTools.TaskTypeName != null)
        //        {
        //            var taskType = new TaskType { Name = vI_ProjectAndOrderTools.TaskTypeName };
        //            db.SP_AddTaskType(taskType.Name);

        //        }
        //        if (vI_ProjectAndOrderTools.CustomerRefName != null)
        //        {
        //            var customerReference = new CustomerRef { Name = vI_ProjectAndOrderTools.CustomerRefName };
        //            db.SP_AddCustomerRef(customerReference.Name);
        //        }
        //        if (vI_ProjectAndOrderTools.RequesterName != null)
        //        {
        //            var requester = new Requester { Name = vI_ProjectAndOrderTools.RequesterName };
        //            db.SP_AddRequester(requester.Name);
        //        }
        //        if (vI_ProjectAndOrderTools.TurbineName != null)
        //        {
        //            var turbineEntity = new Turbine { TurbineName = vI_ProjectAndOrderTools.TurbineName };
        //            db.SP_AddTurbine(turbineEntity.TurbineName);
        //        }
        //        if (vI_ProjectAndOrderTools.ProductName != null)
        //        {
        //            var platformOrProduct = new PlatformOrProduct { ProductName = vI_ProjectAndOrderTools.ProductName };
        //            db.SP_AddPlatformOrProduct(platformOrProduct.ProductName);
        //        }

        //        //db.VI_ProjectAndOrderTools.Add(vI_ProjectAndOrderTools);
        //        try
        //        {
        //            // Your code...
        //            // Could also be before try if you know the exception occurs in SaveChanges

        //            db.SaveChanges();
        //        }
        //        catch (DbEntityValidationException e)
        //        {
        //            foreach (var eve in e.EntityValidationErrors)
        //            {
        //                System.Diagnostics.Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //                foreach (var ve in eve.ValidationErrors)
        //                {
        //                    System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                        ve.PropertyName, ve.ErrorMessage);
        //                }
        //            }
        //            throw;
        //        }
        //        return RedirectToAction("Index");
        //    }

        //    return PartialView("_UnionCreate", vI_ProjectAndOrderTools);
        //}

        //// POST: ProjectAndOrderTools/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////Needs to be changed to have parameter ProjectAndOrderToolsViewModel
        //public ActionResult UnionEdit([Bind(Include = "Organization,TimeTypeName,TaskTypeName,CustomerRefName,RequesterName,TurbineName,ProductName")] VI_ProjectAndOrderTools vI_ProjectAndOrderTools)
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //    return View(vI_ProjectAndOrderTools);
        //}


        //// POST: ProjectAndOrderTools/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////Needs to be changed to have parameter ProjectAndOrderToolsViewModel
        //public ActionResult Edit([Bind(Include = "Organization,TimeTypeName,TaskTypeName,CustomerRefName,RequesterName,TurbineName,ProductName")] VI_ProjectAndOrderTools vI_ProjectAndOrderTools)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(vI_ProjectAndOrderTools).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(vI_ProjectAndOrderTools);
        //}

    }
}
