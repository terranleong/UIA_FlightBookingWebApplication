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
    public class TicketCancellationLogsController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: TicketCancellationLogs
        public ActionResult Index()
        {
            var ticketCancellationLogs = db.TicketCancellationLogs.Include(t => t.Passenger);
            return View(ticketCancellationLogs.ToList());
        }

        // GET: TicketCancellationLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCancellationLog ticketCancellationLog = db.TicketCancellationLogs.Find(id);
            if (ticketCancellationLog == null)
            {
                return HttpNotFound();
            }
            return View(ticketCancellationLog);
        }

        // GET: TicketCancellationLogs/Create
        public ActionResult Create()
        {
            ViewBag.passport = new SelectList(db.Passengers, "passport", "firstName");
            return View();
        }

        // POST: TicketCancellationLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "logID,ticID,passport,passengerName,status,resID,charge,dateTimes,operation")] TicketCancellationLog ticketCancellationLog)
        {
            if (ModelState.IsValid)
            {
                db.TicketCancellationLogs.Add(ticketCancellationLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.passport = new SelectList(db.Passengers, "passport", "firstName", ticketCancellationLog.passport);
            return View(ticketCancellationLog);
        }

        // GET: TicketCancellationLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCancellationLog ticketCancellationLog = db.TicketCancellationLogs.Find(id);
            if (ticketCancellationLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.passport = new SelectList(db.Passengers, "passport", "firstName", ticketCancellationLog.passport);
            return View(ticketCancellationLog);
        }

        // POST: TicketCancellationLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "logID,ticID,passport,passengerName,status,resID,charge,dateTimes,operation")] TicketCancellationLog ticketCancellationLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketCancellationLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.passport = new SelectList(db.Passengers, "passport", "firstName", ticketCancellationLog.passport);
            return View(ticketCancellationLog);
        }

        // GET: TicketCancellationLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCancellationLog ticketCancellationLog = db.TicketCancellationLogs.Find(id);
            if (ticketCancellationLog == null)
            {
                return HttpNotFound();
            }
            return View(ticketCancellationLog);
        }

        // POST: TicketCancellationLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketCancellationLog ticketCancellationLog = db.TicketCancellationLogs.Find(id);
            db.TicketCancellationLogs.Remove(ticketCancellationLog);
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
