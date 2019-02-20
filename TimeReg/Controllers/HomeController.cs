using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TimeReg.ViewModels;

namespace TimeReg.Controllers
{
    public class HomeController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCheck(NewUserViewModel model)
        {
            string message;
            if (model.First_Name != null && model.Last_Name != null)
            {
                var Name = model.First_Name + " " + model.Last_Name;
                var Auth = User.Identity.GetUserName();
                try
                {
                    db.SP_AddNewUser(Name, Auth);
                    message = "Successful";
                    return Json(message,JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    message = e.Message; 
                    return Json(model, JsonRequestBehavior.AllowGet);
                }
            } else
            {
                return PartialView("_NewUserPartial", model);
            }
            message = "Unsuccessful";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UserCheck(string ZId)
        {
            string message = "";
            var Auth = User.Identity.GetUserName();

            try
            {
                //var user = db.VI_Users.Where(m => m.NK_ZId == ZId).SingleOrDefault();
                var user = db.VI_Users.Where(m => m.NK_ZId == ZId).SingleOrDefault();
                if (user == null)
                {
                    return PartialView("_NewUserPartial");
                }
                else
                {
                    message = "true";
                }
                
            } catch {
                message = "Unable to Access database";
            }

            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}