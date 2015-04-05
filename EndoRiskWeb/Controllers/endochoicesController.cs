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
    public class endochoicesController : Controller
    {
        private endoriskContext db = new endoriskContext();

        // GET: endochoices
        public ActionResult Index()
        {
            return View(db.endochoices.ToList());
        }

        // GET: endochoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            endochoice endochoice = db.endochoices.Find(id);
            if (endochoice == null)
            {
                return HttpNotFound();
            }
            return View(endochoice);
        }

        // GET: endochoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: endochoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idChoice,choiceSet,choiceOption")] endochoice endochoice)
        {
            if (ModelState.IsValid)
            {
                db.endochoices.Add(endochoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(endochoice);
        }

        // GET: endochoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            endochoice endochoice = db.endochoices.Find(id);
            if (endochoice == null)
            {
                return HttpNotFound();
            }
            return View(endochoice);
        }

        // POST: endochoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idChoice,choiceSet,choiceOption")] endochoice endochoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endochoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(endochoice);
        }

        // GET: endochoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            endochoice endochoice = db.endochoices.Find(id);
            if (endochoice == null)
            {
                return HttpNotFound();
            }
            return View(endochoice);
        }

        // POST: endochoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            endochoice endochoice = db.endochoices.Find(id);
            db.endochoices.Remove(endochoice);
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
