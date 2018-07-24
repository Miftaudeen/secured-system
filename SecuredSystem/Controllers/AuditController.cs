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
    public class AuditController : Controller
    {
        private SecuredSystemContext db = new SecuredSystemContext();

        // GET: /Audit/
        public ActionResult Index()
        {
            return View(db.AuditTables.ToList());
        }

        // GET: /Audit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditTable audittable = db.AuditTables.Find(id);
            if (audittable == null)
            {
                return HttpNotFound();
            }
            return View(audittable);
        }

        // GET: /Audit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Audit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AuditId,Admin,Customer,Field,InitialValue,FinalValue,TimeStamp")] AuditTable audittable)
        {
            if (ModelState.IsValid)
            {
                db.AuditTables.Add(audittable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(audittable);
        }

        // GET: /Audit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditTable audittable = db.AuditTables.Find(id);
            if (audittable == null)
            {
                return HttpNotFound();
            }
            return View(audittable);
        }

        // POST: /Audit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AuditId,Admin,Customer,Field,InitialValue,FinalValue,TimeStamp")] AuditTable audittable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(audittable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(audittable);
        }

        // GET: /Audit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditTable audittable = db.AuditTables.Find(id);
            if (audittable == null)
            {
                return HttpNotFound();
            }
            return View(audittable);
        }

        // POST: /Audit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AuditTable audittable = db.AuditTables.Find(id);
            db.AuditTables.Remove(audittable);
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
