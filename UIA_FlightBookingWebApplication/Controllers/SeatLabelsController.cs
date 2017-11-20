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
    public class SeatLabelsController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: SeatLabels
        public ActionResult Index()
        {
            var seatLabels = db.SeatLabels.Include(s => s.Class).Include(s => s.SeatGroup);
            return View(seatLabels.ToList());
        }

        // GET: SeatLabels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatLabel seatLabel = db.SeatLabels.Find(id);
            if (seatLabel == null)
            {
                return HttpNotFound();
            }
            return View(seatLabel);
        }

        // GET: SeatLabels/Create
        public ActionResult Create()
        {
            ViewBag.claID = new SelectList(db.Classes, "claID", "claName");
            ViewBag.sgID = new SelectList(db.SeatGroups, "sgID", "flightSize");
            return View();
        }

        // POST: SeatLabels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "seatID,sgID,claID")] SeatLabel seatLabel)
        {
            if (ModelState.IsValid)
            {
                db.SeatLabels.Add(seatLabel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.claID = new SelectList(db.Classes, "claID", "claName", seatLabel.claID);
            ViewBag.sgID = new SelectList(db.SeatGroups, "sgID", "flightSize", seatLabel.sgID);
            return View(seatLabel);
        }

        // GET: SeatLabels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatLabel seatLabel = db.SeatLabels.Find(id);
            if (seatLabel == null)
            {
                return HttpNotFound();
            }
            ViewBag.claID = new SelectList(db.Classes, "claID", "claName", seatLabel.claID);
            ViewBag.sgID = new SelectList(db.SeatGroups, "sgID", "flightSize", seatLabel.sgID);
            return View(seatLabel);
        }

        // POST: SeatLabels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "seatID,sgID,claID")] SeatLabel seatLabel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seatLabel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.claID = new SelectList(db.Classes, "claID", "claName", seatLabel.claID);
            ViewBag.sgID = new SelectList(db.SeatGroups, "sgID", "flightSize", seatLabel.sgID);
            return View(seatLabel);
        }

        // GET: SeatLabels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SeatLabel seatLabel = db.SeatLabels.Find(id);
            if (seatLabel == null)
            {
                return HttpNotFound();
            }
            return View(seatLabel);
        }

        // POST: SeatLabels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SeatLabel seatLabel = db.SeatLabels.Find(id);
            db.SeatLabels.Remove(seatLabel);
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
