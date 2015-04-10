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
    * Controller for the Administrators
    */
    public class administratorsController : Controller
    {
        private endoriskContext db = new endoriskContext();

        /*
         * Index shows the view for the Adminsitrators
         * returns the view a list of the administrators from the database
         */
        public ActionResult Index()
        {
            return View(db.administrators.ToList());
        }

        /*
         * Details for the administrator with id in parameter
         * If the administrator is found, returns the view with the administrators details
         */
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            administrator administrator = db.administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        /*
          * This method show the view to create a new administrator
          * The view present fields to create a new admin
          */
        public ActionResult Create()
        {
            return View();
        }

        /*
          * POST Method for creating and adding the new administrator to database
          * Validates AntiForgeryToken to avoid attacks on the user end.
          * Parameter: 
          *  Use the bind method to bind the inputs to the variable administrator
          * Return:
          *  Redirects to the Index view of the adminsitrators
          */
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAdmin,email,password,firstname,lastname,subadmin")] administrator administrator)
        {
            using (endoriskContext a = new endoriskContext())
            {
                //Verifies the state of the model and the binding with the model
                //If the binding of the object model is valid, Adds the administrator to the database
                //Redirect to index
                if (ModelState.IsValid)
                {
                    a.administrators.Add(administrator);        //Add to database
                    a.SaveChanges();
                    return RedirectToAction("Index");          
                }

                return View(administrator);                 //View wirh the administrator
            }
        }

        /*
         * Edit an administrator
         * Check if the id is null returns a Bad Request
         * 
         * If id is not null, Find the id for the administrator in the database
         * return a request not found if the admin is not in database
         * 
         * return: View of the admin to edit
         */
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            administrator administrator = db.administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        /*
         * POST for Edit
         * Binds the elements from the input with the new variable of administrator
         * If the binding of the inputs with the object model is valid
         *    use db.Entry to change the state of the current model
         *    then save changes to the database
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAdmin,email,password,firstname,lastname,subadmin")] administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administrator);
        }


        /*
         * Delete view for the administrator to remove
         * returns a view with the admin if found on the database
         */
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            administrator administrator = db.administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        /*
         * Delete the selected admin
         * Parameter: id for the admin
         * Use Remove to delete the admin
         * Return: Redirect to the Index of the admin List
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            administrator administrator = db.administrators.Find(id);
            db.administrators.Remove(administrator);
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
