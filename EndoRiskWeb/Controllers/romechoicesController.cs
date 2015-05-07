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
    public class romechoicesController : Controller
    {
        private endoriskContext db = new endoriskContext();

        // GET: romechoices
        public ActionResult Index()
        {
            return View(db.romechoices.ToList());
        }

        // GET: romechoices/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romechoice romechoice = db.romechoices.Find(id);
            if (romechoice == null)
            {
                return HttpNotFound();
            }
            return View(romechoice);
        }

        // GET: romechoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: romechoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRomeChoices,romeChoice1,romeOption,value")] romechoice romechoice)
        {
            if (ModelState.IsValid)
            {
                db.romechoices.Add(romechoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(romechoice);
        }

        // GET: romechoices/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romechoice romechoice = db.romechoices.Find(id);
            if (romechoice == null)
            {
                return HttpNotFound();
            }
            return View(romechoice);
        }

        // POST: romechoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRomeChoices,romeChoice1,romeOption,value")] romechoice romechoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(romechoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(romechoice);
        }

        // GET: romechoices/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romechoice romechoice = db.romechoices.Find(id);
            if (romechoice == null)
            {
                return HttpNotFound();
            }
            return View(romechoice);
        }

        // POST: romechoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            romechoice romechoice = db.romechoices.Find(id);
            db.romechoices.Remove(romechoice);
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
