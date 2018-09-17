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
    public class ComplaintController : Controller
    {
        private SecuredSystemContext db = new SecuredSystemContext();

        // GET: /Complaint/
        public ActionResult Index()
        {
            var complaints = db.Complaints.Include(c => c.Customer);
            return View(complaints.ToList());
        }

        // GET: /Complaint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }
        //Customer customer;
        // GET: /Complaint/Create
        public ActionResult Create()
        {
            //customer = db.Customers.Find(cusId);
            //Customer customer = db.Customers.Find(id);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerId", "Surname");
                        
            //ViewBag.customer = customer;
            return View();
        }

        // POST: /Complaint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ComplaintID,CustomerID,description,Flag")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                /*
                 * if (!customer.Equals(null))
                {
                    complaint.CustomerID = customer.CustomerId;
                }
                 */
                db.Complaints.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Details", "Customer", new { id = complaint.CustomerID });
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerId", "AccountNumber", new { id = complaint.CustomerID });
            return View(complaint);
        }

        // GET: /Complaint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerId", "Surname", complaint.CustomerID);
            return View(complaint);
        }

        // POST: /Complaint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ComplaintID,CustomerID,description,status")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerId", "Surname", complaint.CustomerID);
            
            return View(complaint);
        }

        // GET: /Complaint/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // POST: /Complaint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            db.Complaints.Remove(complaint);
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
