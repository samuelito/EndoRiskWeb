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
        private int pid = 0; //id del paciente #1
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

        //Funcion que genera el id del paciente
        //autoincrementa el paciente
        //devuelve un numero nuevo
        public int generatePatient()
        {
            pid++;
            return pid;
        }
        public ActionResult Result(FormCollection c)
        {
           /* using (endoriskContext data = new endoriskContext())
            {*/

                //Modelos a utilizarse en la base de datos:
                patient paciente = new patient(); //contains patient
                endoanswer respuestas = new endoanswer();  //contains answers to questions
                patientsymptom sintomas = new patientsymptom(); //to store symptoms of patients

                //Calcular riesgo - Utilizar formulas de R
                //Enviar valores del form necesarios... 
                //verificar las converciones sean correctas antes de calcular?

                var riesgo = (float)56.78; //ejemplo

                //Generar el paciente autoincrementando el valor 
                var idpaciente = generatePatient();

                //Guardar Resultados en base de datos 

                /*---Paciente---*/
                paciente.idQuiz = 12;
                paciente.idPatient = idpaciente; //Verificar si el paciente puso id previo en el form 
                //if else 
                paciente.risk = riesgo;           //resultado del risk (utilizando R)
                paciente.time = DateTime.Now;    //tiempo que se tomo el quiz
                paciente.verified = "Yes";

                //guardar paciente en  base de datos -> idquiz se generara solo!
                /*
                data.patients.Add(patient);  //adds patients result to database
                data.SaveChanges();
                 */

                //Obtener el idquiz del paciente acabado de generar
                //Para poder continuar guardando los sintomas y preguntas en la base de datos
                //Query -> Select idQuiz From Patients Where idPatient == idpaciente
                var idquiz = db.patients.Where(model => model.idPatient.Equals(idpaciente)).Select(model => model.idQuiz);

                //for para los sintomas?
                //for ()  { }

                sintomas.idPatient = idpaciente;
                sintomas.symptom = "valor del form collection";
                //a~adir a sintomas el valor 1-0 binario del form

                //trabajar con las respuestas
                respuestas.answer = "valor del form";
                respuestas.idQuestion = 0; //del form
                respuestas.idQuiz = 0; //variable id quiz

                /*
                data.endoanswers.Add(answer); //adds patient answers to database
                data.SaveChanges();           //
                data.patientsymptoms.Add(symptoms); //adds patients symptoms to database
                data.SaveChanges();
                */
                //return RedirectToAction("Result"); //Returns to Result View??

                //devolver el patient result model: idquiz, paciente id, resultado, verificado o no?
                //devolver "patient" return View(patient);
                return View(paciente);
            
           
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
                    else
                    {

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
