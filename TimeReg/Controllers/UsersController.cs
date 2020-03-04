using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
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

            

            //Below is a conversion from VI_UserTimePerProject to UserTimePerProjectViewModel to avoid using ViewBags on dynamic expressions
            var users = (from viUserTimePerProject in db.VI_UserTimePerProject.Where(m => m.PK_Id == id)
                                    select new UserTimePerProjectViewModel()
                                    {
                                        PK_Id = viUserTimePerProject.PK_Id,
                                        UserName = viUserTimePerProject.UserName,
                                        NK_ZId = viUserTimePerProject.NK_ZId,
                                        timeSum = viUserTimePerProject.timeSum,
                                        FK_ProjectId = viUserTimePerProject.FK_ProjectId,
                                        OrderNumber = viUserTimePerProject.Number,
                                        Name = viUserTimePerProject.Name


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
                                 OrderNumber = "No time records",
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
                                 OrderNumber = "No time records",
                                 timeSum = 0,
                                 FK_ProjectId = 0,
                                 Name = "No time records"
                             }).ToList();
            }
       


            //var users =  db.VI_UserTimePerProject.Where(m => m.PK_Id == id).ToList() ;
			// VI_Users users = db.VI_Users.SingleOrDefault(m => m.PK_Id == id);
			//UsersViewModel usersViewModel = new UsersViewModel(users);
			if (users == null)
			{
				return HttpNotFound();
			}
            //Below is the viewbags and such.
            //Gets the current week number
            var weekNo = CalendarHelper.GetWeekNr();


            /* 
             * Used to set ViewBags used by the date range picker to set a proper starting range. 
             * xxxDateController is used by the View
             * while xxxDateSQL is used in the SQL query due to mismatching formats..
             * startDateController and endDateController is also used to get the weekly time registere
             * Delta and delta + 6 respectively gets the beginning and end of week dates.
             *  
             */
            var input = DateTime.Now.Date;
            int delta = DayOfWeek.Monday - input.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            var startDateController = input.AddDays(delta).Date.ToString("dd/MM/yyyy");
            var endDateController = input.AddDays(delta + 6).Date.ToString("dd/MM/yyyy");
            var startDateSQL = input.AddDays(delta).Date.ToString("yyyy/MM/dd");
            var endDateSQL = input.AddDays(delta + 6).Date.ToString("yyyy/MM/dd");
            ViewBag.startDateController = startDateController;
            ViewBag.endDateController = endDateController;

            //Below Query summarizes and uses two dates to define the scope where the Total time value is wanted. In this case an week from Monday to Sunday.
            //"firstResult" is because a query is not properly run before being used elsewhere, so w/o it, the result would return null
            var result = db.Database.SqlQuery<WeeklyTimeViewModel>(@"SELECT	[UserName], sum(Time) as [TotalTime] 
                                                                     FROM	[TimeManagement].[VI_TimeRegistration] 
                                                                     WHERE FK_UserId = @p0 AND [Date] BETWEEN @p1 AND @p2
                                                                     group by [UserName]", id, startDateSQL, endDateSQL).ToList();
			var firstResult = result.FirstOrDefault();
			if (firstResult != null) {
                //Sets the viewbag to the first result.
				ViewBag.WeeklyTime = firstResult;
                ViewBag.WeeklyTime.Week = weekNo;
			} else
			{
				ViewBag.WeeklyTime = new WeeklyTimeViewModel { Week = weekNo, UserName = users.First().UserName, TotalTime = 0 };
			}




            //Below query gets a list contaning the time registered per day on a specific user.
            var weekDayTimeTable = db.Database.SqlQuery<WeekDayTimeTable>(@"SELECT	 DATENAME(dw,[Date]) as [Date], sum(Time) as [TotalTime] 
                                                                     FROM	[TimeManagement].[VI_TimeRegistration] 
                                                                     WHERE FK_UserId = @p0 AND [Date] BETWEEN @p1 AND @p2
                                                                     group by [Date]", id, startDateSQL, endDateSQL).ToList();

            //This dictionary is created and then subsquetly added an entry for each day, so we can default to 0 rather easily.
            Dictionary<string, double> WeekDayTimeTableDict = new Dictionary<string, double>();
            WeekDayTimeTableDict.Add("Monday", 0);
            WeekDayTimeTableDict.Add("Tuesday", 0);
            WeekDayTimeTableDict.Add("Wednesday", 0);
            WeekDayTimeTableDict.Add("Thursday", 0);
            WeekDayTimeTableDict.Add("Friday", 0);
            WeekDayTimeTableDict.Add("Saturday", 0);
            WeekDayTimeTableDict.Add("Sunday", 0);

            
            //Loop over the results and update the dictionary values based on the key from the above query
            foreach (var data in weekDayTimeTable)
            {
                try
                {
                    WeekDayTimeTableDict[data.Date] = data.TotalTime;
                }
                catch
                {
                    Console.WriteLine("Could not insert data.Date: " + data.Date);
                    Console.WriteLine("With data.TotalTime value of: " + data.TotalTime);
                }
            }


            //Assign the results to a ViewBag to be used in the razor page
            ViewBag.WeekDayTimeTableDict = WeekDayTimeTableDict;


            //testing culture
            CultureInfo culture1 = CultureInfo.CurrentCulture;
            CultureInfo culture2 = Thread.CurrentThread.CurrentCulture;
            ViewBag.culture1 = culture1;
            ViewBag.culture2 = culture2;

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

        [HttpGet]
        public ActionResult TimeTable(int id, DateTime? startDate, DateTime? endDate)
        {
            DateTime startDateController;
            DateTime endDateController;
            if (startDate == null || endDate == null)
            {
                DateTime input = DateTime.Now.Date;
                int delta = DayOfWeek.Monday - input.DayOfWeek;
                if (delta > 0)
                    delta -= 7;
                startDateController = input.AddDays(delta);
                endDateController = startDateController.AddDays(delta + 6);
                ViewBag.startDateController = startDateController;
                ViewBag.endDateController = endDateController;
            } else
            {
                startDateController = (DateTime)startDate;
                endDateController = (DateTime)endDate;
            }
            //Below is a conversion from VI_UserTimePerProject to UserTimePerProjectViewModel to avoid using ViewBags on dynamic expressions
            var users = (from viUserTimePerProject in db.VI_UserTimePerProject.Where(m => m.PK_Id == id && m.Date >= startDateController && m.Date <= endDateController)
                         select new UserTimePerProjectViewModel()
                         {
                             PK_Id = viUserTimePerProject.PK_Id,
                             UserName = viUserTimePerProject.UserName,
                             NK_ZId = viUserTimePerProject.NK_ZId,
                             OrderNumber = viUserTimePerProject.Number,
                             timeSum = viUserTimePerProject.timeSum,
                             FK_ProjectId = viUserTimePerProject.FK_ProjectId,
                             Date = viUserTimePerProject.Date,
                             Name = viUserTimePerProject.Name

                         }).OrderByDescending(m => m.timeSum).ToList();

            if (users == null)
            {
                return HttpNotFound();
            }
         
            return PartialView("_TimeTable", users);

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
