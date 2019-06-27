using Microsoft.AspNet.Identity;
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
using TimeReg.Tools;

namespace TimeReg.Controllers
{
    public class CommentsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: Comments
        public ActionResult Index()
        {
            ViewBag.CurrentController = "Comments";
            return View();
        }


        public ActionResult IndexTable()
        {
            //Created so the user can only see his/her own comments
            var windowsAuth = User.Identity.GetUserName();
            var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;

            return PartialView("_IndexTable", db.VI_Comments.Where(m => m.FK_User == windowsAuthId).ToList());
        }

        // GET: Comments/Details/5
        public ActionResult DynamicDetails(string CRUD, int? PK_Id)
        {
            if (PK_Id == null | CRUD != "Details")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_Comments comments = db.VI_Comments.SingleOrDefault(m => m.PK_Id == PK_Id);
            if (comments == null)
            {
                return HttpNotFound();
            }

            //Initializing Objects for the view bags. - These are used to get the value of the view bags
            var ProjectsBag = db.VI_Projects.SingleOrDefault(m => m.PK_Id == comments.FK_ProjectId);
            var UserBag = db.VI_Users.SingleOrDefault(m => m.PK_Id == comments.FK_User);

            //Creating Viewbag and getting the needed values
            ViewBag.ProjectName = ProjectsBag.Name;
            ViewBag.UserName = UserBag.NK_Name;
            return PartialView("_DynamicDetails", comments);
        }

        // GET: Comments/Create
        public ActionResult DynamicCreate()
        {
            
            var year = DateTime.Now.Year;
            var weekNo = CalendarHelper.GetWeekNr();
            var comments = new CommentsViewModel { Year = year, WeekNr = weekNo };
            //Created so the user can default to him
            try
            {
                var windowsAuth = User.Identity.GetUserName();
                var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;
                ViewBag.FK_User = new SelectList(db.VI_Users, "PK_Id", "NK_Name", windowsAuthId);
                ViewBag.Year = year;
                ViewBag.WeekNr = weekNo;
            }
            catch
            {
                ViewBag.FK_User = new SelectList(db.VI_Users, "PK_Id", "NK_Name", "Please Select User");
            }



            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", "Please Select Project");
            return PartialView("_DynamicCreate", comments);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicCreate([Bind(Include = "PK_Id,WeekNr,Year,Text,FK_ProjectId,FK_User")] CommentsViewModel comments)
        {
            if (ModelState.IsValid)
            {
                db.SP_AddComment(comments.WeekNr, comments.Year, comments.Text, comments.FK_ProjectId, comments.FK_User);
                db.SaveChanges();

                string removed = "Add Confirmed";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }

            ViewBag.FK_User = new SelectList(db.VI_Users, "PK_Id", "NK_Name", comments.FK_User);
            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", comments.FK_ProjectId);
            return PartialView("_DynamicCreate", comments);
        }

        // GET: Comments/Edit/5
        public ActionResult DynamicEdit(string CRUD, int? PK_Id)
        {
            if (PK_Id == null | CRUD != "Edit")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_Comments comments = db.VI_Comments.SingleOrDefault(m => m.PK_Id == PK_Id);
            var commentsViewModel = new CommentsViewModel() { PK_Id = comments.PK_Id, WeekNr = comments.WeekNr, Year = comments.Year, Text = comments.Text, FK_ProjectId = comments.FK_ProjectId, FK_User = comments.FK_User };
            if (commentsViewModel == null)
            {
                return HttpNotFound();
            }

            //Creating Viewbags to use as lists where default value is the current selected projectId and User Id
            ViewBag.ProjectName = new SelectList(db.VI_Projects, "PK_Id", "Name", commentsViewModel.FK_ProjectId);
            ViewBag.UserName = new SelectList(db.VI_Users, "PK_Id", "NK_Name", commentsViewModel.FK_User);
            return PartialView("_DynamicEdit",commentsViewModel);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicEdit([Bind(Include = "PK_Id,WeekNr,Year,Text,FK_ProjectId,FK_User, ProjectName, UserName")] CommentsViewModel comments)
        {
            if (ModelState.IsValid)
            {
                db.SP_UpdateComments(comments.PK_Id, comments.WeekNr, comments.Year, comments.Text, comments.ProjectName, comments.UserName);
                //The Original Scaffolded method from Entity framework
                //db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();

                string removed = "Edit Confirmed";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }
            ViewBag.ProjectName = new SelectList(db.VI_Projects, "PK_Id", "Name", comments.FK_ProjectId);
            ViewBag.UserName = new SelectList(db.VI_Users, "PK_Id", "NK_Name", comments.FK_User);
            return PartialView("_DynamicEdit",comments);
        }

        // GET: Comments/Delete/5
        public ActionResult DynamicDelete(string CRUD, int? PK_Id)
        {
            if (PK_Id == null | CRUD != "Delete")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_Comments comments = db.VI_Comments.SingleOrDefault(m => m.PK_Id == PK_Id);
            if (comments == null)
            {
                return HttpNotFound();
            }

            //Initializing Objects for the view bags. - These are used to get the value of the view bags
            var ProjectsBag = db.VI_Projects.SingleOrDefault(m => m.PK_Id == comments.FK_ProjectId);
            var UserBag = db.VI_Users.SingleOrDefault(m => m.PK_Id == comments.FK_User);

            //Creating Viewbag and getting the needed values
            ViewBag.ProjectName = ProjectsBag.Name;
            ViewBag.UserName = UserBag.NK_Name;

            return PartialView("_DynamicDelete",comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("DynamicDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(CommentsViewModel model)
        {


            if (model != null)
            {
                int PK_Id = model.PK_Id;

                try
                {
                    db.SP_RemoveComment(PK_Id);
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




































        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_Comments comments = db.VI_Comments.SingleOrDefault(m => m.PK_Id == id);
            if (comments == null)
            {
                return HttpNotFound();
            }

            //Initializing Objects for the view bags. - These are used to get the value of the view bags
            var ProjectsBag = db.VI_Projects.SingleOrDefault(m => m.PK_Id == comments.FK_ProjectId);
            var UserBag = db.VI_Users.SingleOrDefault(m => m.PK_Id == comments.FK_User);

            //Creating Viewbag and getting the needed values
            ViewBag.ProjectName = ProjectsBag.Name;
            ViewBag.UserName = UserBag.NK_Name;
            return View(comments);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {

            var year = DateTime.Now.Year;
            var weekNo = CalendarHelper.GetWeekNr();

            //Created so the user can default to him
            try
            {
                var windowsAuth = User.Identity.GetUserName();
                var windowsAuthId = db.VI_Users.Where(m => m.NK_ZId == windowsAuth).SingleOrDefault().PK_Id;
                ViewBag.FK_User = new SelectList(db.VI_Users, "PK_Id", "NK_Name", windowsAuthId);
                ViewBag.Year = year;
            }
            catch
            {
                ViewBag.FK_User = new SelectList(db.VI_Users, "PK_Id", "NK_Name", "Please Select User");
            }

          
          
            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", "Please Select Project");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,WeekNr,Year,Text,FK_ProjectId,FK_User")] CommentsViewModel comments)
        {
            if (ModelState.IsValid)
            {
                db.SP_AddComment(comments.WeekNr, comments.Year, comments.Text, comments.FK_ProjectId, comments.FK_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_User = new SelectList(db.VI_Users, "PK_Id", "NK_Name", comments.FK_User);
            ViewBag.FK_ProjectId = new SelectList(db.VI_Projects, "PK_Id", "Name", comments.FK_ProjectId);
            return View(comments);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_Comments comments = db.VI_Comments.SingleOrDefault(m => m.PK_Id == id);
            var commentsViewModel = new CommentsViewModel() { PK_Id = comments.PK_Id, WeekNr = comments.WeekNr, Year = comments.Year, Text = comments.Text, FK_ProjectId = comments.FK_ProjectId, FK_User = comments.FK_User };
            if (commentsViewModel == null)
            {
                return HttpNotFound();
            }

            //Creating Viewbags to use as lists where default value is the current selected projectId and User Id
            ViewBag.ProjectName = new SelectList(db.VI_Projects, "PK_Id", "Name", commentsViewModel.FK_ProjectId);
            ViewBag.UserName = new SelectList(db.VI_Users, "PK_Id", "NK_Name", commentsViewModel.FK_User);
            return View(commentsViewModel);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,WeekNr,Year,Text,FK_ProjectId,FK_User, ProjectName, UserName")] CommentsViewModel comments)
        {
            if (ModelState.IsValid)
            {
               db.SP_UpdateComments(comments.PK_Id, comments.WeekNr, comments.Year, comments.Text, comments.ProjectName, comments.UserName);
                //The Original Scaffolded method from Entity framework
                //db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectName = new SelectList(db.VI_Projects, "PK_Id", "Name", comments.FK_ProjectId);
            ViewBag.UserName = new SelectList(db.VI_Users, "PK_Id", "NK_Name", comments.FK_User);
            return View(comments);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_Comments comments = db.VI_Comments.SingleOrDefault(m => m.PK_Id == id);
            if (comments == null)
            {
                return HttpNotFound();
            }

            //Initializing Objects for the view bags. - These are used to get the value of the view bags
            var ProjectsBag = db.VI_Projects.SingleOrDefault(m => m.PK_Id == comments.FK_ProjectId);
            var UserBag = db.VI_Users.SingleOrDefault(m => m.PK_Id == comments.FK_User);

            //Creating Viewbag and getting the needed values
            ViewBag.ProjectName = ProjectsBag.Name;
            ViewBag.UserName = UserBag.NK_Name;

            return View(comments);
        }

        //// POST: Comments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{

        //    db.SP_RemoveComment(id);
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
    }
}
