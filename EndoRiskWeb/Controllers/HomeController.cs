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
        /*
        var p = db.endoquestions.ToList();      //variable contains all questions for Endometriosis
        var c = db.endochoices.ToList();        //variable contains choices for endometriosis questions

        List<questions> result = new List<questions>();  //List of type "questions" to store the result

        questions q = null;                     //q is a new variable of type : questions
      
        foreach (var item in p){                //iterates over every item in p (each question)
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
            return View(result);        // return a view with the result list*/

            IndexViewModel allQuestions = new IndexViewModel();
            List<IndexViewModel> allQuestionsList = new List<IndexViewModel>();
            allQuestions.quest = new List<questions>();
            allQuestions.symp = new List<symptom>();
            allQuestions.quest.ToList();
            allQuestions.symp.ToList();
            allQuestionsList.Add(allQuestions);


            var endoQuestionList = db.endoquestions.ToList();      //variable contains all questions for Endometriosis
            var endoChoiceList = db.endochoices.ToList();        //variable contains choices for endometriosis questions
            var symptomList = db.symptoms.ToList();

            questions q = new questions();//q is a new variable of type : questions
            symptom s = new symptom();


            foreach (var questionItem in endoQuestionList)
            {                //iterates over every item in p (each question)
                q = new questions();
                // List<endochoice> endoChoiceList = new List<endochoice>();
                q.question = questionItem.endoQuestion1;            //add the questions to q
                q.questionid = (int)questionItem.idQuestion;       //adds the id of question to q
                q.abbr = questionItem.abbr;                         //adds the abbreviation to q
                q.set = questionItem.choiceSet;                     //adds set of choices to q
                q.choices = new List<string>();             //create a new List to store choices for the question

                foreach (var item2 in endoChoiceList)                    //for each value in c (choice table)
                {
                    if (q.set.Equals(item2.choiceSet))                  //if set of choices for the question EQUALS
                    {                                                   //the set of choices in table c then:
                        q.choices.Add(item2.choiceOption.ToString());   //add the option to the choices. 
                    }

                }

                allQuestions.quest.Add(q);

                //finally add the question object to the resulting list

            }

            foreach (var symptomItem in symptomList)
            {                //iterates over every item in p (each question)
                s = new symptom();
                symptomList = new List<symptom>();
                s.symptom1 = symptomItem.symptom1;            //add the symptom to s
                s.idSymptom = symptomItem.idSymptom;         //adds the id of symptom to s
                s.abbr = symptomItem.abbr;                         //adds the symptom to s

                allQuestions.symp.Add(s);
                // return a view with the result list 
            }
            allQuestionsList[0] = allQuestions;
            return View(allQuestionsList);
        }

        /*
         * Documentation: Samuel
         * 
         * Function to generate patients ID using max ID in the database
         * updates: the value of pid by 1
         * returns: new value of pid (pid++)
         * 
         */
        public int generatePatient()
        {
            pid = db.patients.Max(m=> m.idPatient);
            pid++;
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
                //Model to store patients data in Database
                patient paciente = new patient();                   
             
                //Calculate Lifetime Risk: Using R prediction models
                //verify convertions of parameters of the required data
                //Testing example using linear prediction
                var riesgo = (float) 26.78;
                int tempPaciente = 0;
                var numeroPaciente = c.GetValue("PID").AttemptedValue;
                
            //Store the Patient ID in temporary variable
                if (!(numeroPaciente.Equals("")))
                {
                    tempPaciente = Convert.ToInt32(numeroPaciente);
                }
                
                //Verify if patient ID input exist in the database
                var checkpatiente = db.patients.Where( m=> m.idPatient.Equals(tempPaciente)).Select(m => m.idPatient).FirstOrDefault().ToString();

                if (checkpatiente.Equals("0") || numeroPaciente.Equals(""))
                {
                    //Patient ID input is not in database or was not provided
                    //Generate new patient ID
                   paciente.idPatient = generatePatient();
                } 
                else
                {
                    //Patient ID input exists in database -> assign the input Patient ID provided 
                   paciente.idPatient = tempPaciente;
                }
                         
                paciente.risk = riesgo;             //Lifetime risk result 
                paciente.time = DateTime.Now;       //time of the quiz

                //Verify if logged in
                //Testing purpose => "Yes"
                paciente.verified = "Yes";          

                                                
              //db.patients.Add(paciente);  //adds patients result to database
              //db.SaveChanges();
                
                //Obtain the idquiz from the patient added to Database
                var idquiz = from p in db.patients
                             where p.idPatient == paciente.idPatient
                             orderby p.idQuiz descending
                             select p.idQuiz;

                //Add the severity of the patient
                //if riesgo -> medium or high
                //Calculate Severity:
                if (riesgo > 50)
                {
                    var severity = "76";                               //Calculate Severity
                    severity severidad = new severity();            //new severity object
                    severidad.idQuiz = idquiz.First();              //set the idQuiz to match severity IdQuiz Variable
                    severidad.severity1 = severity;                 //Value for the severity
               //     db.severities.Add(severidad);                   //Add the severity to the Database
               //     db.SaveChanges(); 
                }
                                              

                //Store the Patients Answers to the database
                //Arguments: 
                //quizId -> identify the quiz of the answers
                //c -> Form with the answers
                //storeAnswers(idquiz.First(), c);
                //storeSymptoms(idquiz.First(), c);
                storeSymptoms(20, c);
             
                //Return a patient type to the Risk view: 
                //Includes-> idquiz, paciente id, resultado, verified
                return View(paciente);
        }
        
        /*
         * Documentation: Samuel
         * Function to store the answers of the quiz in the database
         * Parameters: 
         *  idq = id of the quiz
         *  c = form collection containing the answers
         */
        public void storeAnswers(int idq, FormCollection c)
        {
                endoanswer respuestas = new endoanswer();
                var data = db.endoquestions;
                
                //Go through every element in the form
                //Starts at 2 since first elements are not part of the questions
                for (int x = 2; x < c.Count-1; x++) {             
 
                    string ans = c.GetKey(x);                   //get the answer from the form
                    
                    var ques = from q in data                   //Select the question id by checking
                               where q.abbr.Equals(ans)         //the abbreviation from the form in the 
                               select q.idQuestion;             //question table -> returns the ID of the question

                    int idpreg = (int) ques.FirstOrDefault();     //get the first value in 
               
                    //Add the answers to the questions 
                    respuestas.answer = c.Get(x);               //the answer to the question
                    respuestas.idQuestion = idpreg;             //quiestion id of the answer
                    respuestas.idQuiz = idq;                    //quiz of the answered question


                   db.endoanswers.Add(respuestas);           //adds patient answers to database
                   db.SaveChanges();                           //save changes in the database
                }
                
        }
        /*
         * Documentation: Samuel
         * Function to store the symptoms of the quiz in the database
         * Parameters: 
         *  idq = id of the quiz
         *  c = form collection containing the answers
         */
        public void storeSymptoms(int idq, FormCollection c)
        {
            patientsymptom sintoma = new patientsymptom();
            var data = db.symptoms;
            
            // Symptoms Array
            string[] userSymptomsList = c.Get(c.Count - 1).Split(',');

            //Go through every element in the symptoms list
            foreach (var item in data)
            {
                
                sintoma.idQuiz = idq;
                string temp = item.abbr.ToString();
                sintoma.symptom = temp;
                if(userSymptomsList.Contains(temp))
                {
                    sintoma.hasSymptom = 1;
                }
                else
                {
                    sintoma.hasSymptom = 0;
                }

                db.patientsymptoms.Add(sintoma);
                db.SaveChanges();
               
            }
        }
        /*
         * View for the About Section in the Home Page
         */
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        /*
         * View for the Contact Section in the Home Page
         */

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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
