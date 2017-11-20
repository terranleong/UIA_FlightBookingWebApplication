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
    public class FarebasesController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: Farebases
        public ActionResult Index()
        {
            var farebases = db.Farebases.Include(f => f.Airline).Include(f => f.Class);
            return View(farebases.ToList());
        }

        // GET: Farebases/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farebase farebase = db.Farebases.Find(id);
            if (farebase == null)
            {
                return HttpNotFound();
            }
            return View(farebase);
        }

        // GET: Farebases/Create
        public ActionResult Create()
        {
            ViewBag.airlID = new SelectList(db.Airlines, "airlID", "airlName");
            ViewBag.claID = new SelectList(db.Classes, "claID", "claName");
            return View();
        }

        // POST: Farebases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fbID,airlID,claID,fbFare")] Farebase farebase)
        {
            if (ModelState.IsValid)
            {
                db.Farebases.Add(farebase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.airlID = new SelectList(db.Airlines, "airlID", "airlName", farebase.airlID);
            ViewBag.claID = new SelectList(db.Classes, "claID", "claName", farebase.claID);
            return View(farebase);
        }

        // GET: Farebases/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farebase farebase = db.Farebases.Find(id);
            if (farebase == null)
            {
                return HttpNotFound();
            }
            ViewBag.airlID = new SelectList(db.Airlines, "airlID", "airlName", farebase.airlID);
            ViewBag.claID = new SelectList(db.Classes, "claID", "claName", farebase.claID);
            return View(farebase);
        }

        // POST: Farebases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fbID,airlID,claID,fbFare")] Farebase farebase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farebase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.airlID = new SelectList(db.Airlines, "airlID", "airlName", farebase.airlID);
            ViewBag.claID = new SelectList(db.Classes, "claID", "claName", farebase.claID);
            return View(farebase);
        }

        // GET: Farebases/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Farebase farebase = db.Farebases.Find(id);
            if (farebase == null)
            {
                return HttpNotFound();
            }
            return View(farebase);
        }

        // POST: Farebases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Farebase farebase = db.Farebases.Find(id);
            db.Farebases.Remove(farebase);
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
