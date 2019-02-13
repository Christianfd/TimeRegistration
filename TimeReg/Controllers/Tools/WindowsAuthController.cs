using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeReg.Controllers.Tools
{
    public class WindowsAuthController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();
       

        [ChildActionOnly]
        public ActionResult UserId(string ZId)
        {
            
            return PartialView();
        }

        public ActionResult UserPartial(string ZId) {

           
            var user = db.VI_Users.Where(m => m.NK_ZId == ZId).SingleOrDefault();
            ViewBag.UserName = user.NK_Name;
            ViewBag.UserId = user.PK_Id;
            return PartialView("_WindowsAuthPartial");
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