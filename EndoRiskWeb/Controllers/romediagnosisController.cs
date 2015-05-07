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
    public class romediagnosisController : Controller
    {
        private endoriskContext db = new endoriskContext();

        // GET: romediagnosis
        public ActionResult Index()
        {
            return View(db.romediagnosis.ToList());
        }

        // GET: romediagnosis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romediagnosi romediagnosi = db.romediagnosis.Find(id);
            if (romediagnosi == null)
            {
                return HttpNotFound();
            }
            return View(romediagnosi);
        }

        // GET: romediagnosis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: romediagnosis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDiagnosis,disease,diagnosis,rome")] romediagnosi romediagnosi)
        {
            if (ModelState.IsValid)
            {
                db.romediagnosis.Add(romediagnosi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(romediagnosi);
        }

        // GET: romediagnosis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romediagnosi romediagnosi = db.romediagnosis.Find(id);
            if (romediagnosi == null)
            {
                return HttpNotFound();
            }
            return View(romediagnosi);
        }

        // POST: romediagnosis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDiagnosis,disease,diagnosis,rome")] romediagnosi romediagnosi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(romediagnosi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(romediagnosi);
        }

        // GET: romediagnosis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romediagnosi romediagnosi = db.romediagnosis.Find(id);
            if (romediagnosi == null)
            {
                return HttpNotFound();
            }
            return View(romediagnosi);
        }

        // POST: romediagnosis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            romediagnosi romediagnosi = db.romediagnosis.Find(id);
            db.romediagnosis.Remove(romediagnosi);
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
