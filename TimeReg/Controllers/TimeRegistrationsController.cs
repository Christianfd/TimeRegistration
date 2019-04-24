using Microsoft.AspNet.Identity;
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
    public class TimeRegistrationsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: TimeRegistrations
        public ActionResult Index()
        {
            var timeRegistrationViewModel = new TimeRegistrationViewModel(db.VI_TimeRegistration.OrderByDescending(m => m.Date).ToList());
            ViewBag.CurrentController = "TimeRegistrations";
            return View(timeRegistrationViewModel);
        }



        //Dynamic Page Content is being added below:
        //Below is WiP Code for creating a more dynamic Time Registration page
        [HttpGet]
        public ActionResult IndexTable(Nullable<int> id)
        {
            TimeRegistrationViewModel timeRegistrationViewModel;

            if (id == null)
            {
                var user = User.Identity.GetUserName();
                id = db.VI_Users.Where(m => m.NK_ZId == user).SingleOrDefault().PK_Id;

            }

            try
            {
                timeRegistrationViewModel = new TimeRegistrationViewModel(db.VI_TimeRegistration.Where(m => m.FK_UserId == id).OrderByDescending(m => m.Date).ToList());

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

            return PartialView("_IndexTable", timeRegistrationViewModel);

        }

        [HttpGet]
        public ActionResult AjaxDyanimcDropDown(int dropDownKey, string dropDownId)
        {
            var key = (int)dropDownKey;
            var id = (string)dropDownId;
            
            if (id == "FK_OrderId")
            {
                SelectList ProjectList = new SelectList(db.VI_Projects.Where(m => m.FK_OrderNumber == key), "PK_Id", "Name");



                //Check to see if the choosen order number has any projects. If not they get access to all projects
                if (ProjectList.Count() >= 1)
                {
                    return Json(ProjectList, JsonRequestBehavior.AllowGet);
                }
                else {
                    ProjectList = new SelectList(db.VI_Projects, "PK_Id", "Name");
                    return Json(ProjectList, JsonRequestBehavior.AllowGet);
                }

            }

            if (id == "FK_UserId")
            {
                //var OrderNumberList = new SelectList(db.VI_UserAssignment.Where(m => m.FK_UserId == key), "FK_OrderNumber", "Number");

                var OrderNumberList = new SelectList((from c in db.VI_UserAssignment.Where(m => m.FK_UserId == key)
                                                     select new
                                                     {
                                                         ID_Value = c.FK_OrderNumber,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle");

                //Check to see if this user as any assigned projects/ordernumbers. If not they get access to all order numbers
                if (OrderNumberList.Count() >= 1)
                {
                    return Json(OrderNumberList, JsonRequestBehavior.AllowGet);

                }
                else {
                    OrderNumberList = new SelectList((from c in db.VI_OrderNumber
                                    select new
                                    {
                                        ID_Value = c.PK_Id,
                                        NumberAndTitle = c.Number + " :: " + c.Title
                                    }), "ID_Value", "NumberAndTitle");
                    return Json(OrderNumberList, JsonRequestBehavior.AllowGet);
                }


            }

            return null;
         
        }


    



        // GET: ProjectAndOrderTools/Details/5
        [HttpGet]
        public ActionResult DynamicDetails(string CRUD, int PK_Id)
        {

            if (CRUD != "Details")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeRegistrationViewModel model = new TimeRegistrationViewModel ( db.VI_TimeRegistration.Where(m => m.PK_Id == PK_Id).SingleOrDefault());
            return PartialView("_DynamicDetails", model);
        }


        public ActionResult DynamicCreate()
        {

            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name");
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name");

            //Auto Selects the user id & corresponding OrderNumber based on windows authentification
            try
            {
                var windowsAuth = User.Identity.GetUserName();
                var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", windowsAuthId);
                //ViewBag.FK_OrderId = new SelectList(db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId), "FK_OrderNumber", "Number");

                ViewBag.FK_OrderId = new SelectList((from c in db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId)
                                                      select new
                                                      {
                                                          ID_Value = c.FK_OrderNumber,
                                                          NumberAndTitle = c.Number + " :: " + c.Title
                                                      }), "ID_Value", "NumberAndTitle");
            }

            catch
            {
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name");

                //ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number");

                //Below is an attempt to create a List of ordernumber where it uses both the number and the title
                ViewBag.FK_OrderId = new SelectList((from c in db.VI_OrderNumber
                                                     select new
                                                     {
                                                         ID_Value = c.PK_Id,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle");
            }

            return PartialView("_DynamicCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicCreate([Bind(Include = "PK_Id,FK_UserId,FK_ProjectId,FK_OrderId,FK_TaskId,time,Date, Comment")] TimeRegistrationViewModel timeRegistration)
        {


            //If it's able to create a new entry it'll return an empty view(So you can keep creating new entries.
            if (ModelState.IsValid)
            {
          if (timeRegistration != null)
                {
                    try
                    {
                        if (timeRegistration.Comment == null) { timeRegistration.Comment = "No Comment"; }

                        db.SP_AddTimeRegistration(timeRegistration.FK_UserId, timeRegistration.FK_ProjectId, timeRegistration.FK_OrderId, timeRegistration.FK_TaskId, timeRegistration.Time, timeRegistration.Date, DateTime.Now, timeRegistration.Comment);
                        db.SaveChanges();
                        //var messageModel = new TimeRegistrationViewModel { Message = "Your Entry has been added" };

                        //Emptying the viewmodel, currently not in use
                        timeRegistration = new TimeRegistrationViewModel { };

                        //Returns The string "add confirmed" so we can clear the form and it's ready to be reused
                        string removed = "Add Confirmed";
                        return Json(removed, JsonRequestBehavior.AllowGet);
                    }
                    catch (DbEntityValidationException e)
                    {
                        //IIRC to double tjec which part of the model is causing errors. and what type of error it is.
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

                    //Copy paste of the below one as well. Populates the Dynamic Create window with the correct Lists as if a new is created.
                    ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name");
                    ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name");

                    //Auto Selects the user id & corresponding OrderNumber based on windows authentification
                    try
                    {
                        var windowsAuth = User.Identity.GetUserName();
                        var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;
                        ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", windowsAuthId);
                        //ViewBag.FK_OrderId = new SelectList(db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId), "FK_OrderNumber", "Number");

                        ViewBag.FK_OrderId = new SelectList((from c in db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId)
                                                             select new
                                                             {
                                                                 ID_Value = c.FK_OrderNumber,
                                                                 NumberAndTitle = c.Number + " :: " + c.Title
                                                             }), "ID_Value", "NumberAndTitle");
                    }

                    catch
                    {
                        ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name");

                        //ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number");

                        //Below is an attempt to create a List of ordernumber where it uses both the number and the title
                        ViewBag.FK_OrderId = new SelectList((from c in db.VI_OrderNumber
                                                             select new
                                                             {
                                                                 ID_Value = c.PK_Id,
                                                                 NumberAndTitle = c.Number + " :: " + c.Title
                                                             }), "ID_Value", "NumberAndTitle");
                    }


                    //Passes the empty viewmodel to clean up previous data.
                    return PartialView("_DynamicCreate");
                }
            }

            //Else it'll return the model, so you can fix whatever error occured

            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", timeRegistration.FK_ProjectId);
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);
            
            //Auto Selects the user id & corresponding OrderNumber based on windows authentification
            try
            {
                var windowsAuth = User.Identity.GetUserName();
                var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", windowsAuthId);
                //ViewBag.FK_OrderId = new SelectList(db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId), "FK_OrderNumber", "Number");

                ViewBag.FK_OrderId = new SelectList((from c in db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId)
                                                     select new
                                                     {
                                                         ID_Value = c.FK_OrderNumber,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", timeRegistration.FK_OrderId);
            }

            catch
            {
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistration.FK_UserId);

                //ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number");

                //Below is an attempt to create a List of ordernumber where it uses both the number and the title
                ViewBag.FK_OrderId = new SelectList((from c in db.VI_OrderNumber
                                                     select new
                                                     {
                                                         ID_Value = c.PK_Id,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", timeRegistration.FK_OrderId);
            }

            return PartialView("_DynamicCreate", timeRegistration);
        }


        [HttpGet]
        public ActionResult DynamicEdit(string CRUD, int PK_Id)
        {
          
            if (PK_Id == 0 || CRUD == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeRegistrationViewModel timeRegistrationViewModel = new TimeRegistrationViewModel(db.VI_TimeRegistration.Where(m => m.PK_Id == PK_Id).SingleOrDefault());

            if (timeRegistrationViewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistrationViewModel.FK_TaskId);

            try
            {
                var windowsAuth = User.Identity.GetUserName();
                var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", windowsAuthId);
                ViewBag.FK_OrderId = new SelectList((from c in db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId)
                                                     select new
                                                     {
                                                         ID_Value = c.FK_OrderNumber,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", timeRegistrationViewModel.FK_OrderId);
                ViewBag.FK_ProjectId = new SelectList(db.VI_Projects.Where(m => m.FK_OrderNumber == timeRegistrationViewModel.FK_OrderId), "PK_Id", "Name", timeRegistrationViewModel.FK_ProjectId);
            }

            catch
            {
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistrationViewModel.PK_Id);
                ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", timeRegistrationViewModel.FK_OrderId);
                ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", timeRegistrationViewModel.FK_ProjectId);


            }
            return PartialView("_DynamicEdit", timeRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicEdit(TimeRegistrationViewModel timeRegistration)
        {
        
            var PK_Id = timeRegistration.PK_Id;

            if (PK_Id == 0 || timeRegistration == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                try
                {


                    db.SP_UpdateTimeRegistration(timeRegistration.PK_Id, timeRegistration.FK_UserId, timeRegistration.FK_ProjectId, timeRegistration.FK_OrderId, timeRegistration.FK_TaskId, timeRegistration.Time, timeRegistration.Date, DateTime.Now, timeRegistration.Comment);
                    db.SaveChanges();
                    string removed = "Edit Confirmed";
                    return Json(removed, JsonRequestBehavior.AllowGet);
                } catch
                {

                }
            }


            if (timeRegistration == null)
            {
                return HttpNotFound();
            }

            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);

            try
            {
                var windowsAuth = User.Identity.GetUserName();
                var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", windowsAuthId);
                ViewBag.FK_OrderId = new SelectList((from c in db.VI_UserAssignment.Where(m => m.FK_UserId == windowsAuthId)
                                                     select new
                                                     {
                                                         ID_Value = c.FK_OrderNumber,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", timeRegistration.FK_OrderId);
                ViewBag.FK_ProjectId = new SelectList(db.VI_Projects.Where(m => m.FK_OrderNumber == timeRegistration.FK_OrderId), "PK_Id", "Name", timeRegistration.FK_ProjectId);
            }

            catch
            {
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistration.PK_Id);
                ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", timeRegistration.FK_OrderId);
                ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", timeRegistration.FK_ProjectId);


            }

            return PartialView("_DynamicEdit", timeRegistration);

        }

        // GET: ProjectAndOrderTools/Delete/5
        [HttpGet]
        public ActionResult DynamicDelete(string CRUD, int PK_Id)
        {
            if (PK_Id == 0 || CRUD != "Delete" )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeRegistrationViewModel timeRegistrationViewModel = new TimeRegistrationViewModel(db.VI_TimeRegistration.Where(m => m.PK_Id == PK_Id).SingleOrDefault());
            if (timeRegistrationViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DynamicDelete", timeRegistrationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicDelete(TimeRegistrationViewModel model)
        {

            if (model != null)
            {
                int PK_Id = model.PK_Id;

                try
                {
                    db.SP_RemoveTimeRegistration(PK_Id);
                    db.SaveChanges();
                    string removed = "Delete Confirmed";
                    return Json(removed, JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    string removed = "Could Not Delete";
                    return Json(removed, JsonRequestBehavior.AllowGet);
                }
               
            }

            return PartialView("_DynamicDelete", model);
        }








































        //Static CRUD pages below:


        public ActionResult IndexDynamicEdition()
        {
            var timeRegistrationViewModel = new TimeRegistrationViewModel(db.VI_TimeRegistration.OrderByDescending(m => m.Date).ToList());
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
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name");

            //ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number");

            //Below is an attempt to create a List of ordernumber where it uses both the number and the title
            ViewBag.FK_OrderId = new SelectList((from c in db.VI_OrderNumber
                                                 select new
                                                 {
                                                     ID_Value = c.PK_Id,
                                                     NumberAndTitle = c.Number + " :: " + c.Title
                                                 }), "ID_Value", "NumberAndTitle");



            //Logic For only being able to see one self
            //Futher Logic is needed to make it so superusers and above can select other users
            try
            {

                var userZID = User.Identity.GetUserName();
                ViewBag.FK_UserId = new SelectList(db.VI_Users.Where(m => m.NK_ZId == userZID), "PK_Id", "NK_Name");
            }
            catch
            {
                //Needs better catch logic
                ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name");
            }

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

                db.SP_AddTimeRegistration(timeRegistration.FK_UserId, timeRegistration.FK_ProjectId, timeRegistration.FK_OrderId, timeRegistration.FK_TaskId, timeRegistration.Time, timeRegistration.Date, DateTime.Now, timeRegistration.Comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", timeRegistration.FK_ProjectId);
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistration.FK_UserId);

            //ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", timeRegistration.FK_OrderId);

            //Below is an attempt to create a List of ordernumber where it uses both the number and the title
            ViewBag.FK_OrderId = new SelectList((from c in db.VI_OrderNumber
                                                 select new
                                                 {
                                                     ID_Value = c.PK_Id,
                                                     NumberAndTitle = c.Number + " :: " + c.Title
                                                 }), "ID_Value", "NumberAndTitle", timeRegistration.FK_OrderId);

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
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistrationViewModel.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistrationViewModel.FK_UserId);

            //ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", timeRegistration.FK_OrderId);

            //Below is an attempt to create a List of ordernumber where it uses both the number and the title
            ViewBag.FK_OrderId = new SelectList((from c in db.VI_OrderNumber
                                                 select new
                                                 {
                                                     ID_Value = c.PK_Id,
                                                     NumberAndTitle = c.Number + " :: " + c.Title
                                                 }), "ID_Value", "NumberAndTitle", timeRegistration.FK_OrderId);

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
            ViewBag.FK_TaskId = new SelectList(db.VI_TaskType, "PK_Id", "Name", timeRegistration.FK_TaskId);
            ViewBag.FK_UserId = new SelectList(db.VI_Users, "PK_Id", "NK_Name", timeRegistration.FK_UserId);

            //ViewBag.FK_OrderId = new SelectList(db.VI_OrderNumber, "PK_Id", "Number", timeRegistration.FK_OrderId);

            //Below is an attempt to create a List of ordernumber where it uses both the number and the title
            ViewBag.FK_OrderId = new SelectList((from c in db.VI_OrderNumber
                                                 select new
                                                 {
                                                     ID_Value = c.PK_Id,
                                                     NumberAndTitle = c.Number + " :: " + c.Title
                                                 }), "ID_Value", "NumberAndTitle", timeRegistration.FK_OrderId);

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






























