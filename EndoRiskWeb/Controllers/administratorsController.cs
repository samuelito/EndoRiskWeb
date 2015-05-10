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
using WebMatrix.WebData;
using WebMatrix.WebData.Resources;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Newtonsoft.Json;
using System.Net.Mail;

namespace EndoRiskWeb.Controllers
{
    //[Authorize]
    public class administratorsController : Controller
    {
        private endoriskContext db = new endoriskContext();
        // GET: administrators
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    return View(db.administrators.ToList());
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
        }

        [HttpGet]
        //[ChildActionOnly]
        // GET: administrators/Details/5
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

                    administrator administrator = db.administrators.Find(id);

                    if (administrator == null)
                    {
                        return HttpNotFound();
                    }
                    return View(administrator);
                }
                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
        }
             
        [HttpGet]
        // [ChildActionOnly]
        // GET: administrators/Edit/5
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
                    administrator administrator = db.administrators.Find(id);
                    if (administrator == null)
                    {
                        return HttpNotFound();
                    }
                    return View(administrator);
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
        }
        // POST: administrators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ChildActionOnly]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAdmin,email,password,firstname,lastname,subadmin")] administrator administrator)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {

                    if (ModelState.IsValid)
                    {
                        var salt = db.administrators.Where(m => m.email.Equals(administrator.email)).Select(m => m.passwordSalt).FirstOrDefault();
                        administrator.password = administrator.password;
                        administrator.passwordSalt = salt;
                        db.Entry(administrator).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(administrator);
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
        }

        [HttpGet]
       // [ChildActionOnly]
        // GET: administrators/Delete/5
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

                    administrator administrator = db.administrators.Find(id);

                    if (administrator == null)
                    {
                        return HttpNotFound();
                    }

                    return View(administrator);
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
        }

        // POST: administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ChildActionOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    administrator administrator = db.administrators.Find(id);
                    db.administrators.Remove(administrator);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
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
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();
                administrator admins = db.administrators.Where(admin => admin.email.Equals(login.email)).FirstOrDefault();

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                if (admins != null)
                {
                    if (admins.password.Equals(crypto.Compute(login.Password, admins.passwordSalt.ToString())))
                    {
                       
                        FormsAuthentication.SetAuthCookie(admins.email + "," + admins.subadmin + "," + admins.firstname +","+ admins.lastname, false);
                        
                        return RedirectToAction("Admin", "administrators");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Los datos ingresados son incorrectos.");
                    }

                }                

                else
                {
                    ModelState.AddModelError("", "Los datos ingresados son incorrectos.");
                }               
           }

           else
           {
                ModelState.AddModelError("", "Los datos entrados son incorrectos.");
           }
            
            return View(login);
        }

        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("EndoriskQuestions", "EndoriskCalculator");               
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
        }

        [HttpGet]
       // [ChildActionOnly]
        public ActionResult CreateAdmin()
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

        [HttpPost]
        //[ChildActionOnly]
        public ActionResult CreateAdmin(RegisterModel registerAdmin)
        {
            if (ModelState.IsValid)
            {          
                using (endoriskContext db = new endoriskContext())
                {
                    var user1 = db.administrators.Where(user => user.email.Equals(registerAdmin.email)).ToList();

                    if (user1.Count == 0)
                    {
                        var crypto = new SimpleCrypto.PBKDF2();
                        var encrPass = crypto.Compute(registerAdmin.Password);
                        var sysAdmin = db.administrators.Create();
                        sysAdmin.firstname = registerAdmin.firstname;
                        sysAdmin.lastname = registerAdmin.lastname;
                        sysAdmin.email = registerAdmin.email;
                        sysAdmin.password = encrPass;
                        sysAdmin.passwordSalt = crypto.Salt;
                        sysAdmin.subadmin = registerAdmin.subadmin;
                                          
                        db.administrators.Add(sysAdmin);
                        db.SaveChanges();

                        if (sysAdmin.subadmin == 0)
                        {
                            string adminMessage = "Usted ha sido registrado como administrador(a) de EndoRisk. Sus credenciales son:\nCorreo electrónico: " + sysAdmin.email + "\nContraseña: " + registerAdmin.Password;
                            adminMessage = adminMessage.Replace("\n", System.Environment.NewLine);

                            SendEmailModel Email = new SendEmailModel()
                            {
                                From = "epm059@gmail.com",
                                To = sysAdmin.email,
                                Subject = "Bienvenido(a) a EndoRisk!",
                                Body = adminMessage,
                            };

                            SendEmail(Email);
                        }

                        else if (sysAdmin.subadmin == 1)
                        {
                            string subadminMessage = "Usted ha sido registrado como sub-administrador(a) de EndoRisk. Sus credenciales son:\n Correo electrónico: " + sysAdmin.email + "\nContraseña: " + registerAdmin.Password;
                            subadminMessage = subadminMessage.Replace("\n", System.Environment.NewLine);
                            SendEmailModel Email = new SendEmailModel()
                            {
                                From = "epm059@gmail.com",
                                To = sysAdmin.email,
                                Subject = "Bienvenido(a) a EndoRisk!",
                                Body = subadminMessage,
                            };

                            SendEmail(Email);
                        }

                        return View("~/Views/Notifications/EmailConfirmation.cshtml");
                    }

                    else
                    {
                        ModelState.AddModelError("", "El usuario ya existe en el sistema. Por favor ingrese un usuario nuevo.");
                        return View(registerAdmin);
                    }
                }
            }
            return View(registerAdmin);
        }

        public void SendEmail(EndoRiskWeb.Models.SendEmailModel emailObject)
        {           
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(emailObject.To);
                mail.From = new MailAddress(emailObject.From);
                mail.Subject = emailObject.Subject;
                string Body = emailObject.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                //string fileAttach = Server.MapPath("myEmails") + "\\Mypic.jpg";
                //Attachment attach = new Attachment(fileAttach);
                //mail.Attachments.Add(attach);
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("epm059@gmail.com", "EpmJna59");// Enter sender's User name and password
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            else { }      
        }


        [HttpGet]
        public ActionResult RecoverPassword()
        {           
             return View();              
        }

        [HttpPost]
        public ActionResult RecoverPassword(string a)
        {
                    using (endoriskContext db = new endoriskContext())
                    {
                        string mail = Request["recoverPassword"].ToString();
                        var adminUser = db.administrators.Where(user => user.email.Equals(mail)).FirstOrDefault();

                        if (adminUser != null)
                        {
                            //makes temporary password and saves it to the DB
                            var crypto = new SimpleCrypto.PBKDF2(); 
                            string tempPass = "endorisk" + Guid.NewGuid().ToString();
                            var encrypTemp = crypto.Compute(tempPass);
                            var tempSalt = crypto.Salt;
                            adminUser.password = encrypTemp;
                            adminUser.passwordSalt = tempSalt;
                            db.SaveChanges();

                            //send temporary password to user
                            string adminMessage = "Saludos " + mail + "!\n Su contraseña temporera es: " + tempPass + ". Gracias por utilizar Endorisk!";
                            adminMessage = adminMessage.Replace("\n", System.Environment.NewLine);

                            SendEmailModel Email = new SendEmailModel()
                            {
                                From = "epm059@gmail.com",
                                To = mail,
                                Subject = "Bienvenido(a) a EndoRisk!",
                                Body = adminMessage,
                            };

                            SendEmail(Email);

                            ViewBag.email = mail;
                            return RedirectToAction("PasswordSent", "administrators", new {email = mail});
                        }

                        else
                        {
                            var errorMessage = "El usuario no existe en el sistema. Por favor ingrese su correo electrónico correctamente.";
                            ViewData["errorMessage"] = errorMessage; 
                            return View();
                        }
                    }
        }

        [HttpGet]
        public ActionResult ResetPassword(string email)
        {
            
           ViewData["email"] = email;
           return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(LocalPasswordModel userNewPassword)
        {
            if (ModelState.IsValid)
            {
                    using (endoriskContext db = new endoriskContext())
                    {
                        //string mail = Request["recoverPassword"].ToString();
                        var adminUser = db.administrators.Where(user => user.email.Equals(userNewPassword.userEmail)).FirstOrDefault();

                        if (adminUser != null)
                        {
                            //makes temporary password and saves it to the DB
                            var crypto = new SimpleCrypto.PBKDF2();
                            string newPass = userNewPassword.NewPassword;
                            var encrypTemp = crypto.Compute(newPass);
                            var newSalt = crypto.Salt;
                            adminUser.password = encrypTemp;
                            adminUser.passwordSalt = newSalt;
                            db.SaveChanges();                          

                            return View("~/Views/Notifications/PasswordChangeSuccesful.cshtml");
                        }
                     
                    }
            }
             
            return View(userNewPassword);
        }

        [HttpGet]
        public ActionResult PasswordSent(string email)
        {
            ViewData["email"] =  email;
            return View();
        }

        [HttpGet]
        public ActionResult Admin()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    return View();
                }

                else { return RedirectToAction("EndoriskQuestions", "EndoriskCalculator"); }    
            }
            return View("~/Views/Notifications/AccessDenied.cshtml");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("EndoriskQuestions", "EndoriskCalculator");
            }
        }      
    }
}
