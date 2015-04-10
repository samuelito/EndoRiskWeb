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
    * Controller for the comments
    */
    public class commentsController : Controller
    {
        private endoriskContext db = new endoriskContext();

        /*
         * Index shows the view for the comments
         * returns the view a list of the comments from the database
         */
        public ActionResult Index()
        {
            return View(db.comments.ToList());
        }

        /*
         * Details for the comment with id in parameter
         * If the comment is found, returns the view with the comment details
         */
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        /*
          * This method show the view to create a new comment
          * The view present fields to create a new comment
          */
        public ActionResult Create()
        {
            return View();
        }

        /*
          * POST Method for creating and adding the new comment to database
          * Validates AntiForgeryToken to avoid attacks on the user end.
          * Parameter: 
          *  Use the bind method to bind the inputs to the variable comment
          * Return:
          *  Redirects to the Index view of the comments
          */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idComment,title,content,email")] comment comment)
        {
            //comments includes id, title, content and email
            //time is missing - use datetime.now
            //use new endorisk context for the database entry - testing options
            using (endoriskContext c = new endoriskContext())
            {
                comment.time = DateTime.Now;    //Set the time for comment to actual day and time
               
                if (ModelState.IsValid)
                {
                    c.comments.Add(comment);    //Add the comment entity to the context c
                    c.SaveChanges();            //Save changes to database 
                    return RedirectToAction("Index");  //Return Comments Index
                }

                return View(comment);
            }
        }



        /*
        * Delete view for the comment to remove
        * returns a view with the comment if found on the database
        */
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        /*
         * Delete the selected comment
         * Parameter: id for the comment
         * Use Remove to delete the comment
         * Return: Redirect to the Index of the comment List
         */
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
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
