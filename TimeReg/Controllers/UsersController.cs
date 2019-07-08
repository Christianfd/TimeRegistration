using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeReg;
using TimeReg.Tools;
using TimeReg.ViewModels;
using TimeReg.ViewModels.ViewModelTools;

namespace TimeReg.Controllers
{
	public class UsersController : Controller
	{
		private WebToolEntities1 db = new WebToolEntities1();

		// GET: Users
		public ActionResult Index()
		{
			return View(db.VI_Users.ToList());
		}

		// GET: Users/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

            //Gets the current week number
            var weekNo = CalendarHelper.GetWeekNr();


            //Below Query is an extended version of a below query, this one divides it per week and then per project and then it chooses all projects within the current week.
            var resultWeekly = db.Database.SqlQuery<WeeklyTimeViewModel>("SELECT cast(datepart(wk, date) as int) Week, year(date) as Year, sum(Time) as [TotalTime],FK_ProjectId,Name FROM	[TimeManagement].[VI_TimeRegistration] Join TimeManagement.VI_Projects on TimeManagement.VI_TimeRegistration.FK_ProjectId = TimeManagement.VI_Projects.PK_Id WHERE FK_UserId = @p0 AND datepart(wk, date) = @p1 AND year(date) = @p2	group by datepart(wk, date), year(date), FK_ProjectId, Name", id, weekNo, DateTime.Now.Year);
            var ResultWeekly = resultWeekly.ToList();

            if (ResultWeekly != null)
            {
                ViewBag.WeeklyTimePerProject = ResultWeekly;
            }
            else
            {
                ViewBag.WeeklyTimePerProject = new WeeklyTimeViewModel { Week = weekNo, Year = DateTime.Now.Year, TotalTime = 0 };
            }

            //Below is a conversion from VI_UserTimePerProject to UserTimePerProjectViewModel to avoid using ViewBags on dynamic expressions
            var users = (from viUserTimePerProject in db.VI_UserTimePerProject.Where(m => m.PK_Id == id)
                                    select new UserTimePerProjectViewModel()
                                    {
                                        PK_Id = viUserTimePerProject.PK_Id,
                                        UserName = viUserTimePerProject.UserName,
                                        NK_ZId = viUserTimePerProject.NK_ZId,
                                        Name = viUserTimePerProject.Name,
                                        timeSum = viUserTimePerProject.timeSum,
                                        FK_ProjectId = viUserTimePerProject.FK_ProjectId
                                        
                                      
        }).OrderByDescending(m => m.timeSum).ToList();


            //Bit of a whacky approach for flow control but it should work fine.
            //Below does a check to see if the above List is empty, as it will be if there is no Time Registration entries by the user.
            try
            {
                if (users.SingleOrDefault().UserName == null)
                {
                    users = (from viUsers in db.VI_Users.Where(m => m.PK_Id == id)
                             select new UserTimePerProjectViewModel()
                             {
                                 PK_Id = viUsers.PK_Id,
                                 UserName = viUsers.NK_Name,
                                 NK_ZId = viUsers.NK_ZId,
                                 Name = "No time records",
                                 timeSum = 0,
                                 FK_ProjectId = 0
                             }).ToList();
                };
            } catch
            {
                    users = (from viUsers in db.VI_Users.Where(m => m.PK_Id == id)
                             select new UserTimePerProjectViewModel()
                             {
                                 PK_Id = viUsers.PK_Id,
                                 UserName = viUsers.NK_Name,
                                 NK_ZId = viUsers.NK_ZId,
                                 Name = "No time records",
                                 timeSum = 0,
                                 FK_ProjectId = 0
                             }).ToList();
            }
       


            //var users =  db.VI_UserTimePerProject.Where(m => m.PK_Id == id).ToList() ;
			// VI_Users users = db.VI_Users.SingleOrDefault(m => m.PK_Id == id);
			//UsersViewModel usersViewModel = new UsersViewModel(users);
			if (users == null)
			{
				return HttpNotFound();
			}

		
            //Below Query divides all time spent in different weeks and then selects the current week. Second line is bc a queri is not properly run before being used elsewhere, so otherwise this would return null
			var result = db.Database.SqlQuery<WeeklyTimeViewModel>("SELECT	cast(datepart(wk, date) as int) Week, year(date) as Year, sum(Time) as [TotalTime] FROM	[TimeManagement].[VI_TimeRegistration] WHERE FK_UserId = @p0 AND datepart(wk, date) = @p1 AND year(date) = @p2	group by datepart(wk, date), year(date)", id, weekNo, DateTime.Now.Year).ToList();
			var firstResult = result.FirstOrDefault();
			if (firstResult != null) {
                //Sets the viewbag to the first result.
				ViewBag.WeeklyTime = firstResult;
			} else
			{
				ViewBag.WeeklyTime = new WeeklyTimeViewModel { Week = weekNo, Year = DateTime.Now.Year, TotalTime = 0 };
			}


       

            return View(users);
		}

		// GET: Users/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Users/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "PK_Id,NK_Name,NK_ZId")] UsersViewModel users)
		{
			if (ModelState.IsValid)
			{
				try { 
				db.SP_AddUser(users.NK_Name, users.NK_ZId);
				db.SaveChanges();
				return RedirectToAction("Index");
				}
				catch(Exception ex)
				{
					ViewBag.Exception = ex;
					return View("Error", new HandleErrorInfo(ex, "Users","Create"));
				}
			}
			ModelState.AddModelError("", "Error");
			return View(users);
		}

		// GET: Users/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			VI_Users users = db.VI_Users.SingleOrDefault(m => m.PK_Id == id);
			UsersViewModel usersViewModel = new UsersViewModel(users);
			if (users == null)
			{
				return HttpNotFound();
			}
			return View(usersViewModel);
		}

		// POST: Users/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "PK_Id,NK_Name,NK_ZId")] UsersViewModel users)
		{
			if (ModelState.IsValid)
			{
				db.SP_UpdateUser(users.PK_Id, users.NK_Name, users.NK_ZId);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(users);
		}

		// GET: Users/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			VI_Users users = db.VI_Users.SingleOrDefault(m => m.PK_Id == id);

			if (users == null)
			{
				return HttpNotFound();
			}
			return View(users);
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
		   
			db.SP_RemoveUser(id);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		public ActionResult Account()
		{

			return View();
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
