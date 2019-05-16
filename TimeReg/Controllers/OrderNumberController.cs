using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimeReg.ViewModels;

namespace TimeReg.Controllers
{
    public class OrderNumberController : Controller
    {
        private WebToolEntities1 db = new WebToolEntities1();

        // GET: OrderNumber
        public ActionResult Index()
        {
            ViewBag.CurrentController = "OrderNumber";
            return View();
        }

        // GET: OrderNumber
        public ActionResult IndexTable()
        {
            return PartialView("_IndexTable",db.VI_OrderNumber.ToList());
        }

        // GET: OrderNumber/Details/5
        public ActionResult DynamicDetails(string CRUD, int? PK_Id)
        {
            if (CRUD != "Details" | PK_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_OrderNumber vI_OrderNumber = db.VI_OrderNumber.SingleOrDefault(m => m.PK_Id == PK_Id);
            var orderNumberViewModel = new OrderNumberViewModel(vI_OrderNumber);
            if (orderNumberViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DynamicDetails", orderNumberViewModel);
        }

        // GET: OrderNumber/Create
        public ActionResult DynamicCreate()
        {

           
            return PartialView("_DynamicCreate");
        }

        // POST: OrderNumber/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicCreate([Bind(Include = "PK_Id,Number,FK_RequestOrg,FK_Requester,FK_CustomerRef,Title")] OrderNumberViewModel orderNumberViewModel)
        {
            if (ModelState.IsValid)
            {
                db.SP_AddOrderNumber(orderNumberViewModel.Number, orderNumberViewModel.Title);
                db.SaveChanges();

                string removed = "Add Confirmed";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }

              return PartialView("_DynamicCreate", orderNumberViewModel);
        }

        // GET: OrderNumber/Edit/5
        public ActionResult DynamicEdit(string CRUD, int? PK_Id)
        {
            if ( CRUD != "Edit" | PK_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } else if (PK_Id <= 17)
            {

                string removed = "Not allowed to edit the standard Order Numbers";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }
            VI_OrderNumber vI_OrderNumber = db.VI_OrderNumber.SingleOrDefault(m => m.PK_Id == PK_Id);
            var orderNumberViewModel = new OrderNumberViewModel(vI_OrderNumber);
            if (orderNumberViewModel == null)
            {
                return HttpNotFound();
            }

          
            return PartialView("_DynamicEdit",orderNumberViewModel);
        }

        // POST: OrderNumber/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DynamicEdit([Bind(Include = "PK_Id,Number,Title")] OrderNumberViewModel orderNumberViewModel)
        {
            if (ModelState.IsValid && orderNumberViewModel.PK_Id > 17)
            {
                db.SP_UpdateOrderNumber(orderNumberViewModel.PK_Id, orderNumberViewModel.Number, orderNumberViewModel.Title);
                db.SaveChanges();

                string removed = "Edit Confirmed";
                return Json(removed, JsonRequestBehavior.AllowGet);
            }

          
            return PartialView("_DynamicEdit", orderNumberViewModel);
        }

        // GET: OrderNumber/Delete/5
        public ActionResult DynamicDelete(string CRUD, int? PK_Id)
        {
            if (CRUD != "Delete" | PK_Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } else if (PK_Id <= 17)
            {
                string removed = "Not allowed to delete the standard Order Numbers";
                return Json(removed, JsonRequestBehavior.AllowGet);

            }
            VI_OrderNumber vI_OrderNumber = db.VI_OrderNumber.SingleOrDefault(m => m.PK_Id == PK_Id);
            var orderNumberViewModel = new OrderNumberViewModel(vI_OrderNumber);
            if (orderNumberViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DynamicDelete", orderNumberViewModel);
        }

        // POST: OrderNumber/Delete/5
        [HttpPost, ActionName("DynamicDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(OrderNumberViewModel model)
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
                        db.SP_RemoveOrderNumber(PK_Id);
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




































        // GET: OrderNumber/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_OrderNumber vI_OrderNumber = db.VI_OrderNumber.SingleOrDefault(m => m.PK_Id == id);
            var orderNumberViewModel = new OrderNumberViewModel (vI_OrderNumber);
            if (orderNumberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(orderNumberViewModel);
        }

        // GET: OrderNumber/Create
        public ActionResult Create()
        {

            ViewBag.FK_RequestOrg = new SelectList(db.VI_RequestOrg.OrderBy(x => x.PK_Id), "PK_Id", "Organization");
            ViewBag.FK_Requester = new SelectList(db.VI_Requester.OrderBy(x => x.PK_Id), "PK_Id", "Name");
            ViewBag.FK_CustomerRef = new SelectList(db.VI_CustomerRef.OrderBy(x => x.PK_Id), "PK_Id", "Name");
            return View();
        }

        // POST: OrderNumber/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK_Id,Number,FK_RequestOrg,FK_Requester,FK_CustomerRef,Title")] OrderNumberViewModel orderNumberViewModel)
        {
            if (ModelState.IsValid)
            {
                db.SP_AddOrderNumber(orderNumberViewModel.Number, orderNumberViewModel.Title);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.FK_RequestOrg = new SelectList(db.VI_RequestOrg.OrderBy(x => x.PK_Id), "PK_Id", "Organization", orderNumberViewModel.FK_RequestOrg);
            //ViewBag.FK_Requester = new SelectList(db.VI_Requester.OrderBy(x => x.PK_Id), "PK_Id", "Name", orderNumberViewModel.FK_Requester);
            //ViewBag.FK_CustomerRef = new SelectList(db.VI_CustomerRef.OrderBy(x => x.PK_Id), "PK_Id", "Name", orderNumberViewModel.FK_CustomerRef);
            return View(orderNumberViewModel);
        }

        // GET: OrderNumber/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_OrderNumber vI_OrderNumber = db.VI_OrderNumber.SingleOrDefault(m => m.PK_Id == id);
            var orderNumberViewModel = new OrderNumberViewModel (vI_OrderNumber);
            if (orderNumberViewModel == null)
            {
                return HttpNotFound();
            }

            //ViewBag.FK_RequestOrg = new SelectList(db.VI_RequestOrg.OrderBy(x => x.PK_Id), "PK_Id", "Organization", orderNumberViewModel.FK_RequestOrg);
            //ViewBag.FK_Requester = new SelectList(db.VI_Requester.OrderBy(x => x.PK_Id), "PK_Id", "Name", orderNumberViewModel.FK_Requester);
            //ViewBag.FK_CustomerRef = new SelectList(db.VI_CustomerRef.OrderBy(x => x.PK_Id), "PK_Id", "Name", orderNumberViewModel.FK_CustomerRef);

            return View(orderNumberViewModel);
        }

        // POST: OrderNumber/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK_Id,Number,FK_RequestOrg,FK_Requester,FK_CustomerRef,Title")] OrderNumberViewModel orderNumberViewModel)
        {
            if (ModelState.IsValid)
            {
                db.SP_UpdateOrderNumber(orderNumberViewModel.PK_Id, orderNumberViewModel.Number, orderNumberViewModel.Title);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.FK_RequestOrg = new SelectList(db.VI_RequestOrg.OrderBy(x => x.PK_Id), "PK_Id", "Organization", orderNumberViewModel.FK_RequestOrg);
            //ViewBag.FK_Requester = new SelectList(db.VI_Requester.OrderBy(x => x.PK_Id), "PK_Id", "Name", orderNumberViewModel.FK_Requester);
            //ViewBag.FK_CustomerRef = new SelectList(db.VI_CustomerRef.OrderBy(x => x.PK_Id), "PK_Id", "Name", orderNumberViewModel.FK_CustomerRef);

            return View(orderNumberViewModel);
        }

        // GET: OrderNumber/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VI_OrderNumber vI_OrderNumber = db.VI_OrderNumber.SingleOrDefault(m => m.PK_Id == id);
            var orderNumberViewModel = new OrderNumberViewModel (vI_OrderNumber);
            if (orderNumberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(orderNumberViewModel);
        }

        //// POST: OrderNumber/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    VI_OrderNumber vI_OrderNumber = db.VI_OrderNumber.SingleOrDefault(m => m.PK_Id == id);
        //    var orderNumberViewModel = new OrderNumberViewModel (vI_OrderNumber);
        //    db.SP_RemoveOrderNumber(orderNumberViewModel.PK_Id);
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
