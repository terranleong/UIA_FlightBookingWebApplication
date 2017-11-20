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
    public class Route_ReservationController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: Flights/Details/5
        public ActionResult Book(string id)
        {
            if (UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] == null)
            {
                TempData["HasLoginUser"] = false;
                return RedirectToAction("ViewAvailableFlight", "Flights");

            }

            string resID = (from a in db.Route_Reservation
                        orderby a.resID descending
                        select a.resID).FirstOrDefault();

            string[] tokens = null;
            string finalResID = null;

            if (resID != null)
            {
                //tokens = resID.Split(new[] { "Res" }, StringSplitOptions.None);
                tokens = resID.Split(new [] { "Res" }, StringSplitOptions.None);
                int previousResID = Int32.Parse(tokens[1]);
                int currentResID = previousResID + 1;
                finalResID = "Res" + currentResID;
            }

            string flightID = id;
            string type = "Oneway";
            int custID = UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0].custID;
            string custIDs = "" + custID;

            //TempData["ResID"] = finalResID;
            ////TempData["ResID"] = finalResID;
            //TempData["FlightID"] = flightID;
            //TempData["Type"] = type;
            //TempData["CustID"] = custIDs;
      
            Route_Reservation rr = new Route_Reservation();
            rr.resID = finalResID;
            rr.flightID = flightID;
            rr.type = type;
            rr.customer = custID;

            db.Route_Reservation.Add(rr);
            db.SaveChanges();

            return RedirectToAction("ViewAvailableFlight", "Flights");
            //db.Flights.Add(flight);
            //db.SaveChanges();
            //return RedirectToAction("Index");


            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Flight flight = db.Flights.Find(id);
            //if (flight == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(flight);
            return RedirectToAction("ViewAvailableFlight", "Flights");
        }

        // GET: Route_Reservation
        public ActionResult Index()
        {
            var route_Reservation = db.Route_Reservation.Include(r => r.Customer1).Include(r => r.Flight);
            return View(route_Reservation.ToList());
        }

        // GET: Route_Reservation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_Reservation route_Reservation = db.Route_Reservation.Find(id);
            if (route_Reservation == null)
            {
                return HttpNotFound();
            }
            return View(route_Reservation);
        }

        // GET: Route_Reservation/Create
        public ActionResult Create()
        {
            ViewBag.customer = new SelectList(db.Customers, "custID", "firstName");
            ViewBag.flightID = new SelectList(db.Flights, "flightID", "aircID");
            return View();
        }

        // POST: Route_Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "recordID,resID,flightID,type,customer")] Route_Reservation route_Reservation)
        {
            if (ModelState.IsValid)
            {
                db.Route_Reservation.Add(route_Reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer = new SelectList(db.Customers, "custID", "firstName", route_Reservation.customer);
            ViewBag.flightID = new SelectList(db.Flights, "flightID", "aircID", route_Reservation.flightID);
            return View(route_Reservation);
        }

        // GET: Route_Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_Reservation route_Reservation = db.Route_Reservation.Find(id);
            if (route_Reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer = new SelectList(db.Customers, "custID", "firstName", route_Reservation.customer);
            ViewBag.flightID = new SelectList(db.Flights, "flightID", "aircID", route_Reservation.flightID);
            return View(route_Reservation);
        }

        // POST: Route_Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "recordID,resID,flightID,type,customer")] Route_Reservation route_Reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(route_Reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer = new SelectList(db.Customers, "custID", "firstName", route_Reservation.customer);
            ViewBag.flightID = new SelectList(db.Flights, "flightID", "aircID", route_Reservation.flightID);
            return View(route_Reservation);
        }

        // GET: Route_Reservation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Route_Reservation route_Reservation = db.Route_Reservation.Find(id);
            if (route_Reservation == null)
            {
                return HttpNotFound();
            }
            return View(route_Reservation);
        }

        // POST: Route_Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Route_Reservation route_Reservation = db.Route_Reservation.Find(id);
            db.Route_Reservation.Remove(route_Reservation);
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
