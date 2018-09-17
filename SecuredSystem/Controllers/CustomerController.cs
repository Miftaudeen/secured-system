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
                
                    customers = customers.Where(
                    c => c.AccountNumber.Contains(searchString));
                    ViewBag.query = searchString;

            }
            /*else
            {
                return View(customers.Where( c => c.FirstName.Contains(null)));
            }
             */
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
                    customers = customers.OrderBy(c => c.FirstName);
                    break;
            }
            var cus = customers.ToList();
            for(int i=0; i<customers.Count(); i++){
                
                cus[i].SecurityQuestion = AskQuestion();
            }
            return View(cus);
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
        public string posit(int num) {
            switch (num)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }

        }
        public string AskQuestion()
        {
            int first=0, second=0, third=0;
            Random rand = new Random();
            first = rand.Next(1, 8);
            while (first == second || second==0)
            {
                second = rand.Next(1, 8);
            }
            while (first == third || second == third || third == 0) { 
                third = rand.Next(1, 8);
            }
            return "What is the " + first + posit(first) + ", " + second + posit(second) + " and " + third + posit(third) + " number of your security code?";

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

                customer.SecurityQuestion = AskQuestion();
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
                AuditTable audit = new AuditTable();
                
                audit.Admin = User.Identity.Name;
                var cus = db.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
                audit.Customer = cus.FirstName + " " + cus.Surname;
                audit.TimeStamp = DateTime.Now;
                if (!cus.FirstName.Equals(customer.FirstName) || !cus.Surname.Equals(customer.Surname))
                {
                    audit.Field = "Names";
                    audit.InitialValue = cus.FirstName + " " + cus.Surname;
                    audit.FinalValue = customer.FirstName + " " + customer.Surname;
                    db.AuditTables.Add(audit);
                }
                if (!cus.PhoneNumber.Equals(customer.PhoneNumber))
                {
                    audit.Field = "Phone number";
                    audit.InitialValue = cus.PhoneNumber;
                    audit.FinalValue = customer.PhoneNumber;
                    db.AuditTables.Add(audit);
                }
                if (!cus.NextOfKin.Equals(customer.NextOfKin))
                {
                    audit.Field = "Next of Kin";
                    audit.InitialValue = cus.NextOfKin;
                    audit.FinalValue = customer.NextOfKin;
                    db.AuditTables.Add(audit);
                }
                if (!cus.Occupation.Equals(customer.Occupation))
                {
                    audit.Field = "Occupation";
                    audit.InitialValue = cus.Occupation;
                    audit.FinalValue = customer.Occupation;
                    db.AuditTables.Add(audit);
                }
                if (!cus.Address.Equals(customer.Address))
                {
                    audit.Field = "Address";
                    audit.InitialValue = cus.Address;
                    audit.FinalValue = customer.Address;
                    db.AuditTables.Add(audit);
                }
                if (!cus.AccountNumber.Equals(customer.AccountNumber))
                {
                    audit.Field = "Account Number";
                    audit.InitialValue = cus.AccountNumber;
                    audit.FinalValue = customer.AccountNumber;
                    db.AuditTables.Add(audit);
                }
                db.Entry(cus).CurrentValues.SetValues(customer);
                
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
