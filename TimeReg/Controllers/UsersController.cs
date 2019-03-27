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
			VI_Users users = db.VI_Users.SingleOrDefault(m => m.PK_Id == id);
			UsersViewModel usersViewModel = new UsersViewModel(users);
			if (users == null)
			{
				return HttpNotFound();
			}

            //Gets the current week
            var weekNo = CalendarHelper.GetWeekNr();


			var result = db.Database.SqlQuery<WeeklyTimeViewModel>("SELECT	cast(datepart(wk, date) as int) Week, year(date) as Year, sum(Time) as [TotalTime] FROM	[TimeManagement].[TimeRegistration] WHERE FK_UserId = @p0 AND datepart(wk, date) = @p1 AND year(date) = @p2	group by datepart(wk, date), year(date)", id, weekNo, DateTime.Now.Year).ToList();
			var firstResult = result.FirstOrDefault();
			if (firstResult != null) {
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
