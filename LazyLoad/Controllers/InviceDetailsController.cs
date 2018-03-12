using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Models;

namespace LazyLoad.Controllers
{
    public class InviceDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InviceDetails
        public ActionResult Index()
        {
            var inviceDetails = db.InviceDetails.Include(i => i.Invoices);
            return View(inviceDetails.ToList());
        }

        // GET: InviceDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InviceDetails inviceDetails = db.InviceDetails.Find(id);
            if (inviceDetails == null)
            {
                return HttpNotFound();
            }
            return View(inviceDetails);
        }

        // GET: InviceDetails/Create
        public ActionResult Create()
        {
            ViewBag.InvoicesID = new SelectList(db.Invoices, "ID", "ID");
            return View();
        }

        // POST: InviceDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductName,Amount,InvoicesID")] InviceDetails inviceDetails)
        {
            if (ModelState.IsValid)
            {
                db.InviceDetails.Add(inviceDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoicesID = new SelectList(db.Invoices, "ID", "ID", inviceDetails.InvoicesID);
            return View(inviceDetails);
        }

        // GET: InviceDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InviceDetails inviceDetails = db.InviceDetails.Find(id);
            if (inviceDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoicesID = new SelectList(db.Invoices, "ID", "ID", inviceDetails.InvoicesID);
            return View(inviceDetails);
        }

        // POST: InviceDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductName,Amount,InvoicesID")] InviceDetails inviceDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inviceDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoicesID = new SelectList(db.Invoices, "ID", "ID", inviceDetails.InvoicesID);
            return View(inviceDetails);
        }

        // GET: InviceDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InviceDetails inviceDetails = db.InviceDetails.Find(id);
            if (inviceDetails == null)
            {
                return HttpNotFound();
            }
            return View(inviceDetails);
        }

        // POST: InviceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InviceDetails inviceDetails = db.InviceDetails.Find(id);
            db.InviceDetails.Remove(inviceDetails);
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
