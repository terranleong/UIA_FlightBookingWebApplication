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
    public class AircraftsController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: Aircrafts
        public ActionResult Index()
        {
            var aircraft = db.Aircraft.Include(a => a.Airline).Include(a => a.SeatGroup);
            return View(aircraft.ToList());
        }

        // GET: Aircrafts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aircraft aircraft = db.Aircraft.Find(id);
            if (aircraft == null)
            {
                return HttpNotFound();
            }
            return View(aircraft);
        }

        // GET: Aircrafts/Create
        public ActionResult Create()
        {
            ViewBag.airlID = new SelectList(db.Airlines, "airlID", "airlName");
            ViewBag.sgID = new SelectList(db.SeatGroups, "sgID", "flightSize");
            return View();
        }

        // POST: Aircrafts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aircID,airlID,sgID,fbgID")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                db.Aircraft.Add(aircraft);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.airlID = new SelectList(db.Airlines, "airlID", "airlName", aircraft.airlID);
            ViewBag.sgID = new SelectList(db.SeatGroups, "sgID", "flightSize", aircraft.sgID);
            return View(aircraft);
        }

        // GET: Aircrafts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aircraft aircraft = db.Aircraft.Find(id);
            if (aircraft == null)
            {
                return HttpNotFound();
            }
            ViewBag.airlID = new SelectList(db.Airlines, "airlID", "airlName", aircraft.airlID);
            ViewBag.sgID = new SelectList(db.SeatGroups, "sgID", "flightSize", aircraft.sgID);
            return View(aircraft);
        }

        // POST: Aircrafts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aircID,airlID,sgID,fbgID")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aircraft).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.airlID = new SelectList(db.Airlines, "airlID", "airlName", aircraft.airlID);
            ViewBag.sgID = new SelectList(db.SeatGroups, "sgID", "flightSize", aircraft.sgID);
            return View(aircraft);
        }

        // GET: Aircrafts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aircraft aircraft = db.Aircraft.Find(id);
            if (aircraft == null)
            {
                return HttpNotFound();
            }
            return View(aircraft);
        }

        // POST: Aircrafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Aircraft aircraft = db.Aircraft.Find(id);
            db.Aircraft.Remove(aircraft);
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
