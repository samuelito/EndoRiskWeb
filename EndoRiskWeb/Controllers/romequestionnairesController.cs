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
    public class romequestionnairesController : Controller
    {
        private endoriskContext db = new endoriskContext();

        // GET: romequestionnaires
        public ActionResult Index()
        {
            return View(db.romequestionnaires.ToList());
        }

        // GET: romequestionnaires/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romequestionnaire romequestionnaire = db.romequestionnaires.Find(id);
            if (romequestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(romequestionnaire);
        }

        // GET: romequestionnaires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: romequestionnaires/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idRomeQuiz,idPatient,idQuiz")] romequestionnaire romequestionnaire)
        {
            if (ModelState.IsValid)
            {
                db.romequestionnaires.Add(romequestionnaire);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(romequestionnaire);
        }

        // GET: romequestionnaires/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romequestionnaire romequestionnaire = db.romequestionnaires.Find(id);
            if (romequestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(romequestionnaire);
        }

        // POST: romequestionnaires/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idRomeQuiz,idPatient,idQuiz")] romequestionnaire romequestionnaire)
        {
            if (ModelState.IsValid)
            {
                db.Entry(romequestionnaire).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(romequestionnaire);
        }

        // GET: romequestionnaires/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            romequestionnaire romequestionnaire = db.romequestionnaires.Find(id);
            if (romequestionnaire == null)
            {
                return HttpNotFound();
            }
            return View(romequestionnaire);
        }

        // POST: romequestionnaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            romequestionnaire romequestionnaire = db.romequestionnaires.Find(id);
            db.romequestionnaires.Remove(romequestionnaire);
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
