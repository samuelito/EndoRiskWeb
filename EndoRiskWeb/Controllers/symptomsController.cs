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
    /*
   * Documentation: Samuel Feliciano
   * Controller for the symptoms
   */
    public class symptomsController : Controller
    {
        private endoriskContext db = new endoriskContext();

        /*
         * Index shows the view for the symptoms
         * returns the view a list of the symptoms from the database
         */
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    return View(db.symptoms.ToList());
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }

            
        }

        /*
           * This method show the view to create a new symptom
           * The view present fields to create a new symptom
           */
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    return View();
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }            
        }

        /*
          * POST Method for creating and adding the new symptom to database
          * Validates AntiForgeryToken to avoid attacks on the user end.
          * Parameter: 
          *  Use the bind method to bind the inputs to the variable symptom
          * Return:
          *  Redirects to the Index view of the symptoms
          */
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSymptom,symptom1,abbr")] symptom symptom)
        {
            if (ModelState.IsValid)
            {
                db.symptoms.Add(symptom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(symptom);
        }

        /*
         * Edit a symptom
         * Check if the id is null returns a Bad Request
         * 
         * If id is not null, Find the id for the symptom in the database
         * return a request not found if the symptom is not in database
         * 
         * return: View of the symptom to edit
         */
        public ActionResult Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    symptom symptom = db.symptoms.Find(id);
                    if (symptom == null)
                    {
                        return HttpNotFound();
                    }
                    return View(symptom);
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            
        }

        /*
         * POST for Edit
         * Binds the elements from the input with the new variable of symptom
         * If the binding of the inputs with the object model is valid
         *    use db.Entry to change the state of the current model
         *    then save changes to the database
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSymptom,symptom1,abbr")] symptom symptom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(symptom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(symptom);
        }

        /*
         * Delete view for the symptom to remove
         * returns a view with the symptom if found on the database
         */
        public ActionResult Delete(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    symptom symptom = db.symptoms.Find(id);
                    if (symptom == null)
                    {
                        return HttpNotFound();
                    }
                    return View(symptom);
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }            
        }

        /*
         * Delete the selected symptom  
         * Parameter: id for the symptom
         * Use Remove to delete the symptom
         * Return: Redirect to the Index of the symptom List
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            symptom symptom = db.symptoms.Find(id);
            db.symptoms.Remove(symptom);
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
