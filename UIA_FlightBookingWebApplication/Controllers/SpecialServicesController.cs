using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UIA_FlightBookingWebApplication.Models;

namespace UIA_FlightBookingWebApplication.Controllers
{
    public class SpecialServicesController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: SpecialServices
        public ActionResult Index()
        {
            return View(db.SpecialServices.ToList());
        }

        // GET: SpecialServices/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialService specialService = db.SpecialServices.Find(id);
            if (specialService == null)
            {
                return HttpNotFound();
            }
            return View(specialService);
        }

        // GET: SpecialServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpecialServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ssID,ssName,ssFare")] SpecialService specialService)
        {
            if (ModelState.IsValid)
            {
                db.SpecialServices.Add(specialService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(specialService);
        }

        // GET: SpecialServices/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialService specialService = db.SpecialServices.Find(id);
            if (specialService == null)
            {
                return HttpNotFound();
            }
            return View(specialService);
        }

        // POST: SpecialServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ssID,ssName,ssFare")] SpecialService specialService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(specialService);
        }

        // GET: SpecialServices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SpecialService specialService = db.SpecialServices.Find(id);
            if (specialService == null)
            {
                return HttpNotFound();
            }
            return View(specialService);
        }

        // POST: SpecialServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SpecialService specialService = db.SpecialServices.Find(id);
            db.SpecialServices.Remove(specialService);
            db.SaveChanges();
            return RedirectToAction("Index");
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
