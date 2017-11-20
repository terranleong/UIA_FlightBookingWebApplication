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
    public class FarebaseGroupsController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: FarebaseGroups
        public ActionResult Index()
        {
            var farebaseGroups = db.FarebaseGroups.Include(f => f.Farebase);
            return View(farebaseGroups.ToList());
        }

        // GET: FarebaseGroups/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarebaseGroup farebaseGroup = db.FarebaseGroups.Find(id);
            if (farebaseGroup == null)
            {
                return HttpNotFound();
            }
            return View(farebaseGroup);
        }

        // GET: FarebaseGroups/Create
        public ActionResult Create()
        {
            ViewBag.fbID = new SelectList(db.Farebases, "fbID", "airlID");
            return View();
        }

        // POST: FarebaseGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fbgID,fbID")] FarebaseGroup farebaseGroup)
        {
            if (ModelState.IsValid)
            {
                db.FarebaseGroups.Add(farebaseGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fbID = new SelectList(db.Farebases, "fbID", "airlID", farebaseGroup.fbID);
            return View(farebaseGroup);
        }

        // GET: FarebaseGroups/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarebaseGroup farebaseGroup = db.FarebaseGroups.Find(id);
            if (farebaseGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.fbID = new SelectList(db.Farebases, "fbID", "airlID", farebaseGroup.fbID);
            return View(farebaseGroup);
        }

        // POST: FarebaseGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fbgID,fbID")] FarebaseGroup farebaseGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(farebaseGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fbID = new SelectList(db.Farebases, "fbID", "airlID", farebaseGroup.fbID);
            return View(farebaseGroup);
        }

        // GET: FarebaseGroups/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FarebaseGroup farebaseGroup = db.FarebaseGroups.Find(id);
            if (farebaseGroup == null)
            {
                return HttpNotFound();
            }
            return View(farebaseGroup);
        }

        // POST: FarebaseGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FarebaseGroup farebaseGroup = db.FarebaseGroups.Find(id);
            db.FarebaseGroups.Remove(farebaseGroup);
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
