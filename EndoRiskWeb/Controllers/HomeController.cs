using EndoRiskWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace EndoRiskWeb.Controllers
{
    public class HomeController : Controller
    {
        private endoriskContext db = new endoriskContext();
        public ActionResult Index()
        {
            //var p = db.endoquestions.ToList();
            /*
            questions q = new questions();

            var b = db.endochoices.Where(m => m.idChoice.Equals(1)).ToString();
            var a = db.endoquestions.Where(m => m.idQuestion.Equals(1)).ToString();
            q.set(a,b);

           /* q.choices = ;
            q.question = ;*/
          
            return View(db.endoquestions.ToList());
        }

        public ActionResult Result()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Login()
        {
            return View(); //Returns the view for the login page
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(administrator admin)
        {
            if (ModelState.IsValid)
            {
                using (endoriskContext c = new endoriskContext())
                {
                    var v = c.administrators.Where(model => model.email.Equals(admin.email) && model.password.Equals(admin.password)).FirstOrDefault();

                    if (v != null)
                    {
                        Session["LoggedUserEmail"] = v.email.ToString();
                        Session["LoggedUserName"] = v.firstname.ToString();
                        return RedirectToAction("Admin");
                    }
                }
            }
            return View(admin);
        }

        public ActionResult Admin()
        {

            return View();
        }

    }
}
