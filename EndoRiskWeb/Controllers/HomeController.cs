using EndoRiskWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;

namespace EndoRiskWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/Endorisk
        public ActionResult Endorisk()
        {
            ViewBag.Message = "Calculadora de riesgo de sufrir endometriosis a lo largo de su vida.";
            return View();
        }

        // GET: /Home/About
        public ActionResult About()
        {
            ViewBag.Message = "Acerca de esta herramienta.";
            return View();
        }

        // GET: /Home/EndoInfo
        public ActionResult EndoInfo()
        {
            ViewBag.Message = "Aprenda sobre la Endometriosis.";
            return View();
        }
       

        /*
         * Partial view for the login
         * Returns Login Page
         */
        public ActionResult Login()
        {   
            
            return View(); 
        }

        //Documentation: Samuel
        //Login for the adminsitrators
        //Paramater: Administrator model
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = false, ErrorMessage = "Username and Password are Required")]
        public ActionResult Login(administrator admin)
        {
            if (ModelState.IsValid)
            {   
                //Additional Implementation of using db context
                using (endoriskContext c = new endoriskContext())
                {
                    //Query to search for the first instance of email and password in database
                    var v = c.administrators.Where(model => model.email.Equals(admin.email) && model.password.Equals(admin.password)).FirstOrDefault();

                    if (v != null)
                    {
                        //Current Session Logged in: 
                        //Store email, first name and type (admin logged in)
                        Session["LoggedUserEmail"] = v.email.ToString();
                        Session["LoggedUserName"] = v.firstname.ToString();
                        Session["LoggedUserType"] = v.subadmin.ToString();
                        return RedirectToAction("Admin");                   //redirect to the Admin View()
                    }
                    else
                    {   //TODO: Invalid inputs if user is not in the system
                        //Mensaje: No es usuario valido
                    }
                }
            }
            
            return View(admin);
        }

        /*
         * Documentation: Samuel
         * Returns the view for the administrator 
         */
        public ActionResult Admin()
        {                      
            return View(); 
        }

    }
}
