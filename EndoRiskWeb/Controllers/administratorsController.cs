using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EndoRiskWeb.Models;
using System.Web.Security;

namespace EndoRiskWeb.Controllers
{
    public class administratorsController : Controller
    {
        private endoriskContext db = new endoriskContext();

        // GET: administrators
        public ActionResult Index()
        {
            return View(db.administrators.ToList());
        }

        // GET: administrators/Details/5
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

        // GET: administrators/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: administrators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAdmin,email,password,firstname,lastname,subadmin")] administrator administrator)
        {
            using (endoriskContext a = new endoriskContext())
            {
                if (ModelState.IsValid)
                {
                    a.administrators.Add(administrator);
                    a.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(administrator);
            }
        }

        // GET: administrators/Edit/5
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

        // POST: administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: administrators/Delete/5
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

        // POST: administrators/Delete/5
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

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.administrator admin)
        {
            if(ModelState.IsValid)
            {
                if (IsValid(admin.email, admin.password))
                {
                    FormsAuthentication.SetAuthCookie(admin.email, false);//ver bien q hace este metodo, ver si cambio el false a true trabaja lo de cookies
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect.");
                }
            }
            return View(admin);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Models.administrator admin)
        {
            if (ModelState.IsValid)
            {
                using (endoriskContext db = new endoriskContext())
                {
                    //var crypto = new SimpleCrypto.PBKDF2();
                    //var encrPass = crypto.Compute(admin.password);
                    var sysAdmin = db.administrators.Create();
                    sysAdmin.firstname = admin.firstname;
                    sysAdmin.lastname = admin.lastname;
                    sysAdmin.email = admin.email;
                    sysAdmin.password = admin.password; //borrar linea, dejar las otras para encryption
                    //sysAdmin.password = encrPass;
                    //sysAdmin.passwordSalt = crypto.Salt;
                    //aqui va anadir el user id, esto depende de como lo haga samuel
                    //si no es autoincrement puede ser asi:
                    //sysAdmin.idAdmin = Guid.NewGuid();//hay q hacer q parsee
                    sysAdmin.idAdmin = 6;//borrar linea, usar la de arriba, dependiendo de el DB
                    
                    db.administrators.Add(sysAdmin);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(admin);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Home");
        }

        private bool IsValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;
            using (endoriskContext db = new endoriskContext())
            {
                var admin = db.administrators.FirstOrDefault(u => u.email == email);
                if(admin != null){
                    //for password encryption
                    /*if(admin.password == crypto.Compute(password, admin.passwordSalt))
                    {
                        isValid = true;
                    }*/
                    //else
                    if (admin.password == password)
                    {
                        isValid = true;
                    }

                }
            } 



            return isValid;
        }
    }
}
