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
    public class TicketChangesLogsController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: TicketChangesLogs
        public ActionResult Index()
        {
            var ticketChangesLogs = db.TicketChangesLogs.Include(t => t.Passenger).Include(t => t.Ticket);
            return View(ticketChangesLogs.ToList());
        }

        // GET: TicketChangesLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketChangesLog ticketChangesLog = db.TicketChangesLogs.Find(id);
            if (ticketChangesLog == null)
            {
                return HttpNotFound();
            }
            return View(ticketChangesLog);
        }

        // GET: TicketChangesLogs/Create
        public ActionResult Create()
        {
            ViewBag.passport = new SelectList(db.Passengers, "passport", "firstName");
            ViewBag.ticID = new SelectList(db.Tickets, "ticID", "passport");
            return View();
        }

        // POST: TicketChangesLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "logID,ticID,passport,passengerName,status,resID,charge,dateTimes,operation")] TicketChangesLog ticketChangesLog)
        {
            if (ModelState.IsValid)
            {
                db.TicketChangesLogs.Add(ticketChangesLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.passport = new SelectList(db.Passengers, "passport", "firstName", ticketChangesLog.passport);
            ViewBag.ticID = new SelectList(db.Tickets, "ticID", "passport", ticketChangesLog.ticID);
            return View(ticketChangesLog);
        }

        // GET: TicketChangesLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketChangesLog ticketChangesLog = db.TicketChangesLogs.Find(id);
            if (ticketChangesLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.passport = new SelectList(db.Passengers, "passport", "firstName", ticketChangesLog.passport);
            ViewBag.ticID = new SelectList(db.Tickets, "ticID", "passport", ticketChangesLog.ticID);
            return View(ticketChangesLog);
        }

        // POST: TicketChangesLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "logID,ticID,passport,passengerName,status,resID,charge,dateTimes,operation")] TicketChangesLog ticketChangesLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketChangesLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.passport = new SelectList(db.Passengers, "passport", "firstName", ticketChangesLog.passport);
            ViewBag.ticID = new SelectList(db.Tickets, "ticID", "passport", ticketChangesLog.ticID);
            return View(ticketChangesLog);
        }

        // GET: TicketChangesLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketChangesLog ticketChangesLog = db.TicketChangesLogs.Find(id);
            if (ticketChangesLog == null)
            {
                return HttpNotFound();
            }
            return View(ticketChangesLog);
        }

        // POST: TicketChangesLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketChangesLog ticketChangesLog = db.TicketChangesLogs.Find(id);
            db.TicketChangesLogs.Remove(ticketChangesLog);
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
