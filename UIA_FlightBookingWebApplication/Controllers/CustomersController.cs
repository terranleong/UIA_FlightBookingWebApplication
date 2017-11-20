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
    public class CustomersController : Controller
    {
        private FlightBookingSystemEntities db = new FlightBookingSystemEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers
        public ActionResult ViewProfile()
        {

            if (UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] == null)
            {
                ViewBag.HasLoginUser = false;
                return View();
            }

            ViewBag.HasLoginUser = true;

            var loginCustID = UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0].custID;

            var loginCust = from a in db.Customers
                            where a.custID == loginCustID
                            select a;

            return View(loginCust.ToList());

            //return View(db.Customers.ToList());
            //if (UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] == null)
            //{
            //    return View();
            //}

        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //ActionResult
        public ActionResult Create([Bind(Include = "custID,firstName,lastName,username,password")] Customer customer, string confirmedPassword)
        {
            var lookupUsername = customer.username;
            var existingUserQuery = from a in db.Customers
                                    where a.username == lookupUsername
                                    select a.username;

            var countRow = existingUserQuery.Count();

            //Customer searchCustomer = db.Customers.Find(customer.username)
            if (string.IsNullOrEmpty(confirmedPassword))
            {
                ViewBag.EmptyPw = true;
                return View();
            }

            if (customer.password != confirmedPassword)
            {
                ViewBag.PwNotSame = true;
                return View();
            }

            if (countRow != 0)
            {
                ViewBag.ExistingUser = true;
                //return RedirectToAction("Create", "Customers");
                return View();
            }


            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                //save a copy of registered customer
                UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] = customer;
                //ViewBag.ValidLoginUser = true;
                //return View();
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index");
            }

            return View();

        }

        // GET: Customers/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //ActionResult
        public ActionResult Login(string username, string password)
        {

            if (ModelState.IsValid)
            {

                var validUserQuery = from a in db.Customers
                    where a.username == username
                          && a.password == password
                    select a;

                var countRow = validUserQuery.Count();

                //not a valid user
                if (countRow == 0)
                {
                    ViewBag.InvalidUser = true;
                    return View();
                }

                //get customer from database if he / she is a valid user
                Customer loginCust = validUserQuery.FirstOrDefault();
                UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] = loginCust;                
                ViewBag.InvalidUser = false;
                return RedirectToAction("Index", "Home");
                //Customer customer = db.Customers.Find(validUserQuery);
                //UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] = customer;
                //return RedirectToAction("Index", "Home");

            }

            return View();

        }

        public ActionResult Logout()
        {
            UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0] = null;

            return RedirectToAction("Index", "Home");
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //ActionResult
        public ActionResult Edit([Bind(Include = "custID,firstName,lastName,username,password")] Customer customer, string confirmedPassword)
        {

            //Customer searchCustomer = db.Customers.Find(customer.username)
            if (string.IsNullOrEmpty(confirmedPassword))
            {
                ViewBag.EmptyPw = true;
                return View();
            }

            if (customer.password != confirmedPassword)
            {
                ViewBag.PwNotSame = true;
                return View();
            }

            var lookupUsername = customer.username;
            var loginUserCustID = UIA_FlightBookingWebApplication.Models.Customer.loginCustomer[0].custID;
            var existingUserQuery = from a in db.Customers
                where a.username == lookupUsername && a.custID != loginUserCustID
                select a;

            var countRow = existingUserQuery.Count();

            if (countRow != 0)
            {
                ViewBag.ExistingUser = true;
                //return RedirectToAction("Create", "Customers");
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");

        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
