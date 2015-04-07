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
    public class endoquestionsController : Controller
    {
        private endoriskContext db = new endoriskContext();
        /*
         * Documentation: Samuel Feliciano
         * Index
         * Create
         */
        // GET: endoquestions
        public ActionResult Index()
        {   
            //Returns in the index view, the list of questions for endometriosis
            return View(db.endoquestions.ToList());
        }

        // GET: endoquestions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            endoquestion endoquestion = db.endoquestions.Find(id);
            if (endoquestion == null)
            {
                return HttpNotFound();
            }
            return View(endoquestion);
        }

        // GET: endoquestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: endoquestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idQuestion,endoQuestion1,abbr,choiceSet")] endoquestion endoquestion)
        {
            if (ModelState.IsValid)
            {
                /*
                 * Documentation: Samuel
                 * Try1: Null Pointer Exception (choiceSet may be null) -> no choice set available
                 * Fix-> By setting to empty choiceSet = "" -> for text
                 * 
                 * Try2: Verify that choiceSet is not null -> Set to empty choice set (for input text)
                 */ 
                if(endoquestion.choiceSet == null){
                    endoquestion.choiceSet = "";
                }
                db.endoquestions.Add(endoquestion);     //add the question to the database
                db.SaveChanges();                        
                return RedirectToAction("Index");       //redirect page to Index of Questions
            }

            return View(endoquestion);
        }

        // GET: endoquestions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            endoquestion endoquestion = db.endoquestions.Find(id);
            if (endoquestion == null)
            {
                return HttpNotFound();
            }
            return View(endoquestion);
        }

        // POST: endoquestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idQuestion,endoQuestion1,abbr,choiceSet")] endoquestion endoquestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endoquestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(endoquestion);
        }

        // GET: endoquestions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            endoquestion endoquestion = db.endoquestions.Find(id);
            if (endoquestion == null)
            {
                return HttpNotFound();
            }
            return View(endoquestion);
        }

        // POST: endoquestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            endoquestion endoquestion = db.endoquestions.Find(id);
            db.endoquestions.Remove(endoquestion);
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
