using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EndoRiskWeb.Models;

namespace EndoRiskWeb.Controllers
{
    public class romequestionsController : Controller
    {
        private endoriskContext db = new endoriskContext();

        // GET: romequestions
        public ActionResult Index()
        {
            return View(db.romequestions.ToList());
        }

        // GET: romequestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romequestion romequestion = db.romequestions.Find(id);
            if (romequestion == null)
            {
                return HttpNotFound();
            }
            return View(romequestion);
        }

        // GET: romequestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: romequestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRomeQuestion,romeQuestion1,romeChoice")] romequestion romequestion)
        {
            if (ModelState.IsValid)
            {
                db.romequestions.Add(romequestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(romequestion);
        }

        // GET: romequestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romequestion romequestion = db.romequestions.Find(id);
            if (romequestion == null)
            {
                return HttpNotFound();
            }
            return View(romequestion);
        }

        // POST: romequestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRomeQuestion,romeQuestion1,romeChoice")] romequestion romequestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(romequestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(romequestion);
        }

        // GET: romequestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romequestion romequestion = db.romequestions.Find(id);
            if (romequestion == null)
            {
                return HttpNotFound();
            }
            return View(romequestion);
        }

        // POST: romequestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            romequestion romequestion = db.romequestions.Find(id);
            db.romequestions.Remove(romequestion);
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
