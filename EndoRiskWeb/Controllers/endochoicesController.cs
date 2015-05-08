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
     * Controller for Endometriosis Choices
     */
    public class endochoicesController : Controller
    {
        //variable for the database context
        private endoriskContext db = new endoriskContext();

        /*
         * Index shows the view for the Endometriosis Choices
         * returns the view a list of the choices from the database
         */
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    return View(db.endochoices.ToList());
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }

            
        }
  
        /*
         * This method show the view to create a new endometriosis choice
         * The view present fields to create a new choice
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
         * POST Method for creating and adding the new endometriosos choice to database
         * Validates AntiForgeryToken to avoid attacks on the user end.
         * Parameter: 
         *  Use the bind method to bind the inputs to the variable endochoice 
         * Return:
         *  Redirects to the Index view of the Choices.
         */
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idChoice,choiceSet,choiceOption")] endochoice endochoice)
        {
            //Verifies the state of the model and the binding with the model
            //If the binding of the object model is valid, Adds the choices to the database
            //Redirect to index
            if (ModelState.IsValid)
            {
                db.endochoices.Add(endochoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(endochoice);            //If the model State is not valid
                                                //Return same view to add model. 
        }

        /*
         * Edit an endometriosis choice
         * Check if the id is null returns a Bad Request
         * 
         * If id is not null, Find the id for the choice in the database
         * return a request not found if the choice is not in database
         * 
         * return: View of the choice to edit
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

                    endochoice endochoice = db.endochoices.Find(id);

                    if (endochoice == null)
                    {
                        return HttpNotFound();
                    }
                    return View(endochoice);
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
           
        }

        /*
         * POST for Edit
         * Binds the elements from the input with the new variable of endochoice
         * If the binding of the inputs with the object model is valid
         *    use db.Entry to change the state of the current model
         *    then save changes to the database
         */
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

        
        /*
         * Delete view for the choice to remove
         * returns a view with the choice if found on the database
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

                    endochoice endochoice = db.endochoices.Find(id);

                    if (endochoice == null)
                    {
                        return HttpNotFound();
                    }
                    return View(endochoice);
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            
        }

        /*
         * Delete the selected choice
         * Parameter: id for the choice
         * Use Remove to delete the choice
         * Return: Redirect to the Index of the Choices List
         */
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
