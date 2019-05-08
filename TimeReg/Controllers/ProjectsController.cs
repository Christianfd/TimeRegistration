﻿using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TimeReg.ViewModels;

namespace TimeReg.Controllers
{
    public class ProjectsController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: Projects
        public ActionResult Index()
        {
            ViewBag.CurrentController = "Projects";
            return View();
        }

        // GET: Projects
        public ActionResult IndexTable()
        {

            return PartialView("_IndexTable", db.VI_Projects.ToList());
        }
        // GET: Projects/Details/5
        public ActionResult DynamicDetails(string CRUD, int? PK_Id)
        {
            if (PK_Id == null | CRUD != "Details")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projects = db.VI_Projects.SingleOrDefault(M => M.PK_Id == PK_Id);
                         
            if (projects == null)
            {
                return HttpNotFound();
            }
            //This needs work, as it takes all current comments instead of only the needed comments
            //This view model is created to pass the comments to the view along with the project details.
            //The Comments also needs an orderby function at some point and mabye a user list.

            var projectsViewModel = new ProjectDetailsViewModel()
                                                {
                                                    VIProjects = projects,
                                                    VIComments = db.VI_Comments.OrderByDescending(m => m.WeekNr),
                                                    VITimeSpentPerUser = db.VI_UserTimePerProject.Where(m => m.FK_ProjectId == PK_Id)
                                                };

            return PartialView("_DynamicDetails", projectsViewModel);
        }

        // GET: Projects/Create
        public ActionResult DynamicCreate()
        {
            
            

            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users.OrderBy(x => x.PK_Id), "PK_Id", "NK_Name");

            ViewBag.FK_OrderNumber = new SelectList((from c in db.VI_OrderNumber
                                                 select new
                                                 {
                                                     ID_Value = c.PK_Id,
                                                     NumberAndTitle = c.Number + " :: " + c.Title
                                                 }), "ID_Value", "NumberAndTitle");

            ViewBag.FK_TimeType = new SelectList(db.TimeType.OrderBy(x => x.PK_Id), "PK_Id", "Name");
            ViewBag.FK_Country = new SelectList(db.Country.OrderBy(x => x.PK_Id), "PK_Id", "CountryName");
            ViewBag.FK_PlatformOrProduct = new SelectList(db.PlatformOrProduct.OrderBy(x => x.PK_Id), "PK_Id", "ProductName");
            ViewBag.FK_Turbine = new SelectList(db.Turbine.OrderBy(x => x.PK_Id), "PK_Id", "TurbineName");
            return PartialView("_DynamicCreate");
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicCreate([Bind(Include = "PK_Id,Name,FK_Ordernumber,TimeEstimation,FK_ProjectLeader,Scope,FK_TimeType,SiteOrVersion,FK_Country,FK_PlatformOrProduct,FK_Turbine,ProjectComment")] ProjectsViewModel projects)
        {
            if (ModelState.IsValid)
            {
         
                db.SP_AddProject(
                    projects.Name,
                    projects.FK_OrderNumber,
                    projects.TimeEstimation, 
                    projects.FK_ProjectLeader,
                    projects.Scope, 
                    projects.FK_TimeType,
                    projects.SiteOrVersion,
                    projects.FK_Country,
                    projects.FK_PlatformOrProduct,
                    projects.FK_Turbine,
                    projects.ProjectComment);

                db.SaveChanges();

                string removed = "Add Confirmed";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }


            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name", projects.FK_ProjectLeader);
            ViewBag.FK_OrderNumber = new SelectList((from c in db.VI_OrderNumber
                                                     select new
                                                     {
                                                         ID_Value = c.PK_Id,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", projects.FK_OrderNumber);
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name", projects.FK_TimeType);
            ViewBag.FK_Country = new SelectList(db.Country, "PK_Id", "CountryName", projects.FK_Country);
            ViewBag.FK_PlatformOrProduct = new SelectList(db.PlatformOrProduct, "PK_Id", "ProductName", projects.FK_PlatformOrProduct);
            ViewBag.FK_Turbine = new SelectList(db.Turbine, "PK_Id", "TurbineName", projects.FK_Turbine);

            return PartialView("_DynamicCreate", projects);
        }

        // GET: Projects/Edit/5
        public ActionResult DynamicEdit(string CRUD, int? PK_Id)
        {
            if (PK_Id == null | CRUD != "Edit")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (PK_Id <= 17)
            {
                string removed = "Not allowed to delete the standard Order Numbers";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }

            VI_Projects projects = db.VI_Projects.SingleOrDefault(m => m.PK_Id == PK_Id);
            var projectsViewModel = new ProjectsViewModel()
            {
                PK_Id = projects.PK_Id,
                Scope = projects.Scope,
                FK_ProjectLeader = projects.FK_ProjectLeader,
                TimeEstimation = projects.TimeEstimation,
                FK_OrderNumber = projects.FK_OrderNumber,
                Name = projects.Name,
                UserName = projects.UserName,
                FK_TimeType = (int)projects.FK_TimeType,
                SiteOrVersion = projects.SiteOrVersion,
                FK_Country = projects.FK_Country,
                FK_PlatformOrProduct = projects.FK_PlatformOrProduct,
                FK_Turbine = projects.FK_Turbine,
                ProjectComment = projects.ProjectComment
            };

            if (projects == null)
            {
                return HttpNotFound();
            }


            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name", projectsViewModel.FK_ProjectLeader);
            ViewBag.FK_OrderNumber = new SelectList((from c in db.VI_OrderNumber
                                                     select new
                                                     {
                                                         ID_Value = c.PK_Id,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", projects.FK_OrderNumber);
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name", projectsViewModel.FK_TimeType);
            ViewBag.FK_Country = new SelectList(db.Country, "PK_Id", "CountryName", projectsViewModel.FK_Country);
            ViewBag.FK_PlatformOrProduct = new SelectList(db.PlatformOrProduct, "PK_Id", "ProductName", projectsViewModel.FK_PlatformOrProduct);
            ViewBag.FK_Turbine = new SelectList(db.Turbine, "PK_Id", "TurbineName", projectsViewModel.FK_Turbine);

            return PartialView("_DynamicEdit", projectsViewModel);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicEdit([Bind(Include = "PK_Id,Name,FK_Ordernumber,TimeEstimation,FK_ProjectLeader,Scope,FK_TimeType,SiteOrVersion,FK_Country,FK_PlatformOrProduct,FK_Turbine,ProjectComment")] ProjectsViewModel projects)
        {
            if (ModelState.IsValid && projects.PK_Id > 17)
            {
                db.SP_UpdateProject(
                    projects.PK_Id,
                    projects.Name,
                    projects.FK_OrderNumber,
                    projects.TimeEstimation,
                    projects.FK_ProjectLeader,
                    projects.Scope,
                    projects.FK_TimeType,
                    projects.SiteOrVersion,
                    projects.FK_Country,
                    projects.FK_PlatformOrProduct,
                    projects.FK_Turbine,
                    projects.ProjectComment);

                db.SaveChanges();

                string removed = "Edit Confirmed";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }
            //A set of ViewBags containing the nessecary Lists for creating a project - Not sure why I did the PK_Id, nor if they're nessecary here. as it's the edit post.
            ViewBag.PK_Id = new SelectList(db.Comments, "PK_Id", "Text", projects.PK_Id);
            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name", projects.FK_ProjectLeader);
            ViewBag.FK_OrderNumber = new SelectList((from c in db.VI_OrderNumber
                                                     select new
                                                     {
                                                         ID_Value = c.PK_Id,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", projects.FK_OrderNumber);
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name", projects.FK_TimeType);
            ViewBag.FK_Country = new SelectList(db.Country, "PK_Id", "CountryName", projects.FK_Country);
            ViewBag.FK_PlatformOrProduct = new SelectList(db.PlatformOrProduct, "PK_Id", "ProductName", projects.FK_PlatformOrProduct);
            ViewBag.FK_Turbine = new SelectList(db.Turbine, "PK_Id", "TurbineName", projects.FK_Turbine);

            return PartialView("_DynamicEdit", projects);
        }

        // GET: Projects/Delete/5
        public ActionResult DynamicDelete(string CRUD, int? PK_Id)
        {
            if (PK_Id == null | CRUD != "Delete")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (PK_Id <= 17)
            {
                string removed = "Not allowed to delete the standard Order Numbers";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }


            var projects = db.VI_Projects.SingleOrDefault(m => m.PK_Id == PK_Id);
            if (projects == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DynamicDelete",projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("DynamicDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ProjectsViewModel model)
        {
            if (model != null)
            {
                int PK_Id = model.PK_Id;

                //Making sure default Project doesn't get changed or deleted
                if (PK_Id <= 17)
                {
                    string removed = "Not allowed to delete the standard Order Numbers";
                    return Json(removed, JsonRequestBehavior.AllowGet);
                } 
                else if (PK_Id > 17)
                {
                    try
                    {
                        db.SP_RemoveProject(PK_Id);
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
                

            }

            return PartialView("_DynamicDelete", model);
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

            var projectsViewModel = new ProjectDetailsViewModel()
                                                {
                                                    VIProjects = projects,
                                                    VIComments = db.VI_Comments.OrderByDescending(m => m.WeekNr),
                                                    VITimeSpentPerUser = db.VI_UserTimePerProject.Where(m => m.FK_ProjectId == id)
                                                };

            return View(projectsViewModel);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            
            

            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users.OrderBy(x => x.PK_Id), "PK_Id", "NK_Name");

            ViewBag.FK_OrderNumber = new SelectList((from c in db.VI_OrderNumber
                                                 select new
                                                 {
                                                     ID_Value = c.PK_Id,
                                                     NumberAndTitle = c.Number + " :: " + c.Title
                                                 }), "ID_Value", "NumberAndTitle");

            ViewBag.FK_TimeType = new SelectList(db.TimeType.OrderBy(x => x.PK_Id), "PK_Id", "Name");
            ViewBag.FK_Country = new SelectList(db.Country.OrderBy(x => x.PK_Id), "PK_Id", "CountryName");
            ViewBag.FK_PlatformOrProduct = new SelectList(db.PlatformOrProduct.OrderBy(x => x.PK_Id), "PK_Id", "ProductName");
            ViewBag.FK_Turbine = new SelectList(db.Turbine.OrderBy(x => x.PK_Id), "PK_Id", "TurbineName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,Name,FK_Ordernumber,TimeEstimation,FK_ProjectLeader,Scope,FK_TimeType,SiteOrVersion,FK_Country,FK_PlatformOrProduct,FK_Turbine,ProjectComment")] ProjectsViewModel projects)
        {
            if (ModelState.IsValid)
            {
         
                db.SP_AddProject(
                    projects.Name,
                    projects.FK_OrderNumber,
                    projects.TimeEstimation, 
                    projects.FK_ProjectLeader,
                    projects.Scope, 
                    projects.FK_TimeType,
                    projects.SiteOrVersion,
                    projects.FK_Country,
                    projects.FK_PlatformOrProduct,
                    projects.FK_Turbine,
                    projects.ProjectComment);

                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name", projects.FK_ProjectLeader);
            ViewBag.FK_OrderNumber = new SelectList((from c in db.VI_OrderNumber
                                                     select new
                                                     {
                                                         ID_Value = c.PK_Id,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", projects.FK_OrderNumber);
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name", projects.FK_TimeType);
            ViewBag.FK_Country = new SelectList(db.Country, "PK_Id", "CountryName", projects.FK_Country);
            ViewBag.FK_PlatformOrProduct = new SelectList(db.PlatformOrProduct, "PK_Id", "ProductName", projects.FK_PlatformOrProduct);
            ViewBag.FK_Turbine = new SelectList(db.Turbine, "PK_Id", "TurbineName", projects.FK_Turbine);

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
            var projectsViewModel = new ProjectsViewModel()
            {
                PK_Id = projects.PK_Id,
                Scope = projects.Scope,
                FK_ProjectLeader = projects.FK_ProjectLeader,
                TimeEstimation = projects.TimeEstimation,
                FK_OrderNumber = projects.FK_OrderNumber,
                Name = projects.Name,
                UserName = projects.UserName,
                FK_TimeType = (int)projects.FK_TimeType,
                SiteOrVersion = projects.SiteOrVersion,
                FK_Country = projects.FK_Country,
                FK_PlatformOrProduct = projects.FK_PlatformOrProduct,
                FK_Turbine = projects.FK_Turbine,
                ProjectComment = projects.ProjectComment
            };

            if (projects == null)
            {
                return HttpNotFound();
            }


            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name", projectsViewModel.FK_ProjectLeader);
            ViewBag.FK_OrderNumber = new SelectList((from c in db.VI_OrderNumber
                                                     select new
                                                     {
                                                         ID_Value = c.PK_Id,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", projects.FK_OrderNumber);
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name", projectsViewModel.FK_TimeType);
            ViewBag.FK_Country = new SelectList(db.Country, "PK_Id", "CountryName", projectsViewModel.FK_Country);
            ViewBag.FK_PlatformOrProduct = new SelectList(db.PlatformOrProduct, "PK_Id", "ProductName", projectsViewModel.FK_PlatformOrProduct);
            ViewBag.FK_Turbine = new SelectList(db.Turbine, "PK_Id", "TurbineName", projectsViewModel.FK_Turbine);

            return View(projectsViewModel);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,Name,FK_Ordernumber,TimeEstimation,FK_ProjectLeader,Scope,FK_TimeType,SiteOrVersion,FK_Country,FK_PlatformOrProduct,FK_Turbine,ProjectComment")] ProjectsViewModel projects)
        {
            if (ModelState.IsValid)
            {
                db.SP_UpdateProject(
                    projects.PK_Id,
                    projects.Name,
                    projects.FK_OrderNumber,
                    projects.TimeEstimation,
                    projects.FK_ProjectLeader,
                    projects.Scope,
                    projects.FK_TimeType,
                    projects.SiteOrVersion,
                    projects.FK_Country,
                    projects.FK_PlatformOrProduct,
                    projects.FK_Turbine,
                    projects.ProjectComment);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //A set of ViewBags containing the nessecary Lists for creating a project - Not sure why I did the PK_Id, nor if they're nessecary here. as it's the edit post.
            ViewBag.PK_Id = new SelectList(db.Comments, "PK_Id", "Text", projects.PK_Id);
            ViewBag.FK_ProjectLeader = new SelectList(db.VI_Users, "PK_Id", "NK_Name", projects.FK_ProjectLeader);
            ViewBag.FK_OrderNumber = new SelectList((from c in db.VI_OrderNumber
                                                     select new
                                                     {
                                                         ID_Value = c.PK_Id,
                                                         NumberAndTitle = c.Number + " :: " + c.Title
                                                     }), "ID_Value", "NumberAndTitle", projects.FK_OrderNumber);
            ViewBag.FK_TimeType = new SelectList(db.TimeType, "PK_Id", "Name", projects.FK_TimeType);
            ViewBag.FK_Country = new SelectList(db.Country, "PK_Id", "CountryName", projects.FK_Country);
            ViewBag.FK_PlatformOrProduct = new SelectList(db.VI_PlatformOrProduct, "PK_Id", "ProductName", projects.FK_PlatformOrProduct);
            ViewBag.FK_Turbine = new SelectList(db.Turbine, "PK_Id", "TurbineName", projects.FK_Turbine);

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
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    var projects = db.VI_Projects.SingleOrDefault(m => m.PK_Id == id);
        //    db.SP_RemoveProject(id);
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
