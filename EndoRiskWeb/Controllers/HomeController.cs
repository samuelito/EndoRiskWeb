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
        /*
         * Documentation: SAMUEL  
         * 
         * Variables:
         * 
         * type: endoriskContext
         * name: db ==> variable context for the database
         * 
         * type: int
         * name: pid ==> id of the patient (set to the first patient)
         */
        
        private endoriskContext db = new endoriskContext();
        private int pid = 0; 
        
        /*
         * Documentation: Samuel
         * 
         * View for the Index in Home
         * Index shows the Question for Endometriosis Calculator
         * 
         */
        public ActionResult Index()
        {
        
        var p = db.endoquestions.ToList();      //variable contains all questions for Endometriosis
        var c = db.endochoices.ToList();        //variable contains choices for endometriosis questions

        List<questions> result = new List<questions>();  //List of type "questions" to store the result

        questions q = null;        //q is a new variable of type : questions
      
        foreach (var item in p){               //iterates over every item in p (each question)
            q = new questions();
            q.question = item.endoQuestion1;            //add the questions to q
            q.questionid = (int) item.idQuestion;       //adds the id of question to q
            q.abbr = item.abbr;                         //adds the abbreviation to q
            q.set = item.choiceSet;                     //adds set of choices to q
            q.choices = new List<string>();             //create a new List to store choices for the question

            foreach (var item2 in c)                    //for each value in c (choice table)
            {
                if (q.set.Equals(item2.choiceSet))                  //if set of choices for the question EQUALS
                {                                                   //the set of choices in table c then:
                    q.choices.Add(item2.choiceOption.ToString());   //add the option to the choices. 
                }
            }

            result.Add(q);              //finally add the question object to the resulting list
            
        }
            return View(result);        // return a view with the result list 
        }

        /*
         * Documentation: Samuel
         * 
         * Function to generate patients ID
         * updates: the value of pid by 1
         * returns: new value of pid (pid++)
         * 
         */
        public int generatePatient()
        {
            pid = db.patients.Max(m=> m.idPatient);
            return pid;
        }

        /*
         * Documentation: Samuel
         * 
         * Process(function) to show the Lifetime Risk
         * Argument: Form Collection (representns answers from questions of endometriosis)
         * 
         */
        public ActionResult Risk(FormCollection c)
        {
                //Models to store de data in Database
                patient paciente = new patient();                   //contains patient
             
                //Calculate Lifetime Risk: Using R prediction models
                //verify convertions of parameters of the required data
                //Testing example using linear prediction
                var riesgo = (float) 56.78;
                var severity = "0";
                //Calculate Severity:
                if (riesgo > 50)
                {
                    severity = "75"; //Calculate Severity
                }
     
                //Store the results on the database

                /*---Patiente---*/
                
                //Verifiy if patient ID exist
                if (c.GetValue("PID").AttemptedValue.Equals("") || c.GetValue("PID").AttemptedValue == null )
                {
                    paciente.idPatient = generatePatient();                    //Generate new patient ID
                }
                else
                {
                   paciente.idPatient = Convert.ToInt32(c.GetValue("PID").AttemptedValue);  //Get Patient ID from the Form  
                }
                    
                paciente.risk = riesgo;             //Lifetime risk result 
                paciente.time = DateTime.Now;       //time of the quiz

                //Verify if logged in
                paciente.verified = "Yes";          

                //Store Values into Patients Database
                
                db.patients.Add(paciente);  //adds patients result to database
                db.SaveChanges();
                
                //Obtaine the Id Quiz from the patient already added 
                //To cpntinue adding the data
                //Query To Do -> Select idQuiz From Patients Where idPatient == idpaciente
                var idquiz = db.patients.Where(m => m.idPatient == paciente.idPatient).LastOrDefault().idQuiz;

                //int idquiz = 12;
                severity severidad = new severity();
                severidad.idQuiz = idquiz;
                severidad.severity1 = severity;
                db.severities.Add(severidad);
                db.SaveChanges();

                storeAnswers(idquiz, c);
                //Return a patient type to the Risk view: 
                //Includes-> idquiz, paciente id, resultado, verified
                
                return View(paciente);
   
        }

        public void storeAnswers(int idq, FormCollection c)
        {
                endoanswer respuestas = new endoanswer();
                
                var data = db.endoquestions;
  
                for (int x = 0; x < c.Count; x++) { 
                
                int idpreg = Convert.ToInt32(data.Where(m => m.abbr.Equals(c.GetKey(x))).Select(m=>m.idQuestion).ToString());
                //Add the answers to the questions 
                respuestas.answer = c.Get(c.GetKey(x));
                respuestas.idQuestion =  idpreg;            //quiestion id of the answer
                respuestas.idQuiz = idq;                    //quiz of the answered question

                db.endoanswers.Add(respuestas);           //adds patient answers to database
                db.SaveChanges(); 
                }
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
                        //Mensaje: No es usuario valido
                    }
                }
            }
            return View(admin);
        }

        public ActionResult Admin()
        {
            return View(); //Returns the view for the administrator
        }

    }
}
