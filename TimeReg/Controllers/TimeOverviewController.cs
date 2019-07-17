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
	public class TimeOverviewController : Controller
	{
		private WebToolEntities1 db = new WebToolEntities1();

		// GET: Users
		public ActionResult Index()
		{
			return View(db.VI_Users.ToList());
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
