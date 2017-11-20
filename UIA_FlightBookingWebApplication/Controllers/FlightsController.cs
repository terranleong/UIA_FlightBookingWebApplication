using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UIA_FlightBookingWebApplication.Models;

namespace UIA_FlightBookingWebApplication.Controllers
{
    public class FlightsController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: Flights
        public ActionResult Index()
        {
            var flights = db.Flights.Include(f => f.Aircraft);
            return View(flights.ToList());
        }

        // GET: Flights
        public ActionResult ViewAvailableFlight(string flightDepart, string flightDest, string selectedDepartureDate)
        {
            IQueryable<Flight> flights;

            int rowCount;

            var DepartList = new List<string>();
            var DestList = new List<string>();

            var DepartQuery = from d in db.Flights
                                orderby d.source
                                select d.source;

            var DestQuery = from d in db.Flights
                            orderby d.destination
                            select d.destination;

            DepartList.AddRange(DepartQuery.Distinct());
            ViewBag.flightDepart = new SelectList(DepartList);

            DestList.AddRange(DestQuery.Distinct());
            ViewBag.flightDest = new SelectList(DestList);

            flights = from f in db.Flights
                                where f.dates > DateTime.Now
                                orderby f.dates
                          select f;

            //with condition

            //if (!string.IsNullOrEmpty(flightDepart) && !string.IsNullOrEmpty(flightDest) && !string.IsNullOrEmpty(selectedDepartureDate))
            //{

                //flights = flights.Where(a => a.source.Contains(flightDepart) && a.destination.Contains(flightDest));

                //flights = from f in db.Flights
                    //where f.dates > DateTime.Now
                          //&& f.source == flightDepart
                          //&& f.destination == flightDest
                          //&& f.dates == sdd
                          //orderby f.dates
                          //select f;

                //rowCount = flights.Count();
                //if (rowCount == 0)
                //{
                    //ViewBag.HasAvailableFlight = false;

                //}
                //return View(flights);
            //}

            if (!string.IsNullOrEmpty(flightDepart))
            {
                ViewBag.depart = flightDepart;
                flights = flights.Where(a => a.source.Contains(flightDepart));
            }

            if (!string.IsNullOrEmpty(flightDest))
            {
                ViewBag.dest = flightDest;
                flights = flights.Where(a => a.destination.Contains(flightDest));
            }

            var dd = selectedDepartureDate;
            if (!string.IsNullOrEmpty(selectedDepartureDate))
            {
                ViewBag.departd = selectedDepartureDate;

                DateTime sdd = DateTime.ParseExact(selectedDepartureDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                ViewBag.departd = sdd.Date.ToString("dd-MM-yyyy");

                flights = flights.Where(a => a.dates == sdd);

            }
    

            rowCount = flights.Count();
            if (rowCount == 0)
            {
                ViewBag.HasAvailableFlight = false;
            }

            if (TempData["HasLoginUser"] != null)
            {
                ViewBag.HasLoginUser = TempData["HasLoginUser"];
                TempData.Remove("HasLoginUser");
            }

            //test
            //if (TempData["ResID"] != null)
            //{
            //    ViewBag.ResID = TempData["ResID"];
            //    TempData.Remove("ResID");
            //}

            //if (TempData["FlightID"] != null)
            //{
            //    ViewBag.FlightID = TempData["FlightID"];
            //    TempData.Remove("FlightID");
            //}

            //if (TempData["Type"] != null)
            //{
            //    ViewBag.Type = TempData["Type"];
            //    TempData.Remove("Type");
            //}

            //if (TempData["CustID"] != null)
            //{
            //    ViewBag.CustID = TempData["CustID"];
            //    TempData.Remove("CustID");
            //}

            return View(flights);
        }

        // GET: Flights
        public ActionResult ViewCustomerBookedFlight()
        {
            if (UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] == null)
            {
                return View();
            }

            var loginCustID = UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0].custID;
            var flights = from f in db.Flights
                join b in db.Route_Reservation on f.flightID equals b.flightID
                where b.customer == loginCustID
                          orderby f.dates
                select f;

            var countRow = flights.Count();

            if (countRow == 0)
            {
                ViewBag.HasBookedFlight = false;
                //return RedirectToAction("Create", "Customers");
                return View(flights.ToList());
            }

            ViewBag.HasBookedFlight = true;
            return View(flights.ToList());

        }

        // GET: Flights/Details/5
            public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Details/5
        //public ActionResult Book(string id)
        //{
            //if (UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] == null)
            //{
                //TempData["HasLoginUser"] = false;              
                //return RedirectToAction("ViewAvailableFlight");

            //}


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
            //return RedirectToAction("ViewAvailableFlight");
        //}

        // GET: Flights/Create
        public ActionResult Create()
        {
            ViewBag.aircID = new SelectList(db.Aircraft, "aircID", "airlID");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "flightID,aircID,mgID,source,destination,fareInfant,fareChild,fareYouth,fareAdult,fareSenior,dates,times,durationHour,durationMin,timeDelayMin,mealFreq")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aircID = new SelectList(db.Aircraft, "aircID", "airlID", flight.aircID);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            ViewBag.aircID = new SelectList(db.Aircraft, "aircID", "airlID", flight.aircID);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "flightID,aircID,mgID,source,destination,fareInfant,fareChild,fareYouth,fareAdult,fareSenior,dates,times,durationHour,durationMin,timeDelayMin,mealFreq")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aircID = new SelectList(db.Aircraft, "aircID", "airlID", flight.aircID);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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
