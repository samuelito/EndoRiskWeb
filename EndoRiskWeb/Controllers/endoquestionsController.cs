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
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    return View(db.endoquestions.ToList());
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            //Returns in the index view, the list of questions for endometriosis
            
        }

        /*
        * Details for the question with id in parameter
        * If the question is found, returns the view with the question details
        */
        public ActionResult Details(long? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
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

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }            
        }

        /*
          * This method show the view to create a new question
          * The view present fields to create a new question
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
          * POST Method for creating and adding the new question to database
          * Validates AntiForgeryToken to avoid attacks on the user end.
          * Parameter: 
          *  Use the bind method to bind the inputs to the variable endoquestion
          * Return:
          *  Redirects to the Index view of the questions
          */
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

        /*
         * Edit an question
         * Check if the id is null returns a Bad Request
         * 
         * If id is not null, Find the id for the question in the database
         * return a request not found if the question is not in database
         * 
         * return: View of the question to edit
         */
        public ActionResult Edit(long? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
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

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
         
        }

        

        /*
        * POST for Edit
        * Binds the elements from the input with the new variable of question
        * If the binding of the inputs with the object model is valid
        *    use db.Entry to change the state of the current model
        *    then save changes to the database
        */
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

        /*
        * Delete view for the question to remove
        * returns a view with the question if found on the database
        */
        public ActionResult Delete(long? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
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

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            
        }

        /*
         * Delete the selected question
         * Parameter: id for the question
         * Use Remove to delete the question
         * Return: Redirect to the Index of the question List
         */
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
