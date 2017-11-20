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
    public class SeatGroupsController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: SeatGroups
        public ActionResult Index()
        {
            return View(db.SeatGroups.ToList());
        }

        // GET: SeatGroups/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatGroup seatGroup = db.SeatGroups.Find(id);
            if (seatGroup == null)
            {
                return HttpNotFound();
            }
            return View(seatGroup);
        }

        // GET: SeatGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeatGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sgID,flightSize,totalFC,totalBC,totalPEC,totalEC")] SeatGroup seatGroup)
        {
            if (ModelState.IsValid)
            {
                db.SeatGroups.Add(seatGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seatGroup);
        }

        // GET: SeatGroups/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatGroup seatGroup = db.SeatGroups.Find(id);
            if (seatGroup == null)
            {
                return HttpNotFound();
            }
            return View(seatGroup);
        }

        // POST: SeatGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sgID,flightSize,totalFC,totalBC,totalPEC,totalEC")] SeatGroup seatGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seatGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seatGroup);
        }

        // GET: SeatGroups/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatGroup seatGroup = db.SeatGroups.Find(id);
            if (seatGroup == null)
            {
                return HttpNotFound();
            }
            return View(seatGroup);
        }

        // POST: SeatGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SeatGroup seatGroup = db.SeatGroups.Find(id);
            db.SeatGroups.Remove(seatGroup);
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
