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
    public class MealGroupsController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: MealGroups
        public ActionResult Index()
        {
            var mealGroups = db.MealGroups.Include(m => m.Meal);
            return View(mealGroups.ToList());
        }

        // GET: MealGroups/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealGroup mealGroup = db.MealGroups.Find(id);
            if (mealGroup == null)
            {
                return HttpNotFound();
            }
            return View(mealGroup);
        }

        // GET: MealGroups/Create
        public ActionResult Create()
        {
            ViewBag.mID = new SelectList(db.Meals, "mID", "mName");
            return View();
        }

        // POST: MealGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mgID,mID")] MealGroup mealGroup)
        {
            if (ModelState.IsValid)
            {
                db.MealGroups.Add(mealGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mID = new SelectList(db.Meals, "mID", "mName", mealGroup.mID);
            return View(mealGroup);
        }

        // GET: MealGroups/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealGroup mealGroup = db.MealGroups.Find(id);
            if (mealGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.mID = new SelectList(db.Meals, "mID", "mName", mealGroup.mID);
            return View(mealGroup);
        }

        // POST: MealGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mgID,mID")] MealGroup mealGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mealGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mID = new SelectList(db.Meals, "mID", "mName", mealGroup.mID);
            return View(mealGroup);
        }

        // GET: MealGroups/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MealGroup mealGroup = db.MealGroups.Find(id);
            if (mealGroup == null)
            {
                return HttpNotFound();
            }
            return View(mealGroup);
        }

        // POST: MealGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MealGroup mealGroup = db.MealGroups.Find(id);
            db.MealGroups.Remove(mealGroup);
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
