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

           //Possibly move this to NavBar to make a single stored procedure call instead of two in a row.
            try
            {
                var user = db.VI_Users.Where(m => m.NK_ZId == ZId).SingleOrDefault();
                if (user != null)
                {
                    ViewBag.UserName = user.NK_Name;
                    ViewBag.UserId = user.PK_Id;
                    ViewBag.Error = false;
                } else
                {
                    ViewBag.Error = true;
                }
              
            }
            catch
            {
                ViewBag.Error = true;
            }
            
            return PartialView("_WindowsAuthPartial");
        }

        public ActionResult NavBar(string ZId)
        {
            ViewBag.ZId = ZId;
            //Add stored procedure to get the rights of the current person. 
            return PartialView("_NavBar");
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