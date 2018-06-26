using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SecuredSystem.Models;

namespace SecuredSystem.Controllers
{
    public class CustomerController : Controller
    {
        private SecuredSystemContext db = new SecuredSystemContext();

        // GET: /Customer/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "first_name_desc" : "";
            ViewBag.SurnameSortParm = sortOrder == "Surname" ? "surname_desc" : "Surname";
            var customers = from c in db.Customers
                          select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                string [] names = searchString.Split(' ');
                foreach(string name in names){
                    customers = customers.Where(
                    c => c.Surname.ToUpper().Contains(name.ToUpper()) ||
                    c.Surname.ToUpper().Contains(name.ToUpper()) ||
                    c.FirstName.ToUpper().Contains(name.ToUpper()) ||
                    c.FirstName.ToUpper().Contains(name.ToUpper())
                    );
                    if (customers.Count() != 0){
                        break;
                    }
                }
                 
            }
            switch (sortOrder)
            {
                case "first_name_desc":
                    customers = customers.OrderByDescending(c => c.FirstName);
                    break;
                case "Surname":
                    customers = customers.OrderBy(c => c.Surname);
                    break;
                case "surname_desc":
                    customers = customers.OrderByDescending(c => c.Surname);
                    break;
                default:
                    customers = customers.OrderBy(c => c.Surname);
                    break;
            }
            return View(customers.ToList());
        }

        // GET: /Customer/Details/5
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

        // GET: /Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CustomerId,Surname,FirstName,Address,PhoneNumber,Occupation,NextOfKin,AccountNumber,PIN")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.PIN = new Random().Next(0, 9999999).ToString("D7");
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: /Customer/Edit/5
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

        // POST: /Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CustomerId,Surname,FirstName,Address,PhoneNumber,Occupation,NextOfKin,AccountNumber,PIN")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: /Customer/Delete/5
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

        // POST: /Customer/Delete/5
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
