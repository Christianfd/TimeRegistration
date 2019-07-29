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
using System.Globalization;
using System.Threading;

namespace TimeReg.Controllers
{
	public class TimeOverviewController : Controller
	{
		private WebToolEntities1 db = new WebToolEntities1();

		// GET: Users
		public ActionResult Index()
		{
            //Below is the viewbags and such.
            //Gets the current week number
            var weekNo = CalendarHelper.GetWeekNr();

            /* 
             * Used to set ViewBags used by the date range picker to set a proper starting range. 
             * xxxDateController is used by the View
             * while xxxDateSQL is used in the SQL query due to mismatching formats..
             * startDateController and endDateController is also used to get the weekly time registere 
             */
            var input = DateTime.Now.Date;
            int delta = DayOfWeek.Monday - input.DayOfWeek;
            if (delta > 0)
                delta -= 7;
            var startDateController = input.AddDays(delta).Date.ToString("dd/MM/yyyy");
            var endDateController = input.AddDays(6).Date.ToString("dd/MM/yyyy");
            var startDateSQL = input.AddDays(delta).Date.ToString("yyyy/MM/dd");
            var endDateSQL = input.AddDays(6).Date.ToString("yyyy/MM/dd");
            ViewBag.startDateController = startDateController;
            ViewBag.endDateController = endDateController;

            return View(db.VI_Users.ToList());
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
                endDateController = startDateController.AddDays(6);
                ViewBag.startDateController = startDateController;
                ViewBag.endDateController = endDateController;
            }
            else
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
