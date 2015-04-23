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
        int questionIndex = 0;
        int symptomIndex = 0;

        /*
         * Documentation: Samuel
         * 
         * View for the Index in Home
         * Index shows the Question for Endometriosis Calculator
         * 
         */
        public ActionResult Index()
        {

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
                s.symptom1 = symptomItem.symptom1;            //add the questions to q
                s.idSymptom = symptomItem.idSymptom;         //adds the id of question to q
                s.abbr = symptomItem.abbr;                         //adds the abbreviation to q
                //adds set of choices to q

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
            pid = db.patients.Max(m => m.idPatient);
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
        public ActionResult Risk(FormCollection endoForm)
        {
            //Model to store patients data in Database
            patient paciente = new patient();

            //Calculate Lifetime Risk: Using R prediction models
            //verify convertions of parameters of the required data
            //Testing example using linear prediction
            object[,] answerList = endoAnswerlist(endoForm);
            C_Sharp_RExcel pred = new C_Sharp_RExcel();

            double [] riesgo = pred.Macro(answerList);

            //Verifiy if patient ID exist
            if (endoForm.GetValue("PID").AttemptedValue.Equals("") || endoForm.GetValue("PID").AttemptedValue == null)
            {
                paciente.idPatient = generatePatient();                    //Generate new patient ID
            }
            else
            {
                paciente.idPatient = Convert.ToInt32(endoForm.GetValue("PID").AttemptedValue);  //Get Patient ID from the Form  
            }

            paciente.risk = (float)riesgo[0];             //Lifetime risk result 
            paciente.time = DateTime.Now;       //time of the quiz

            //Verify if logged in
            //Testing purpose => "Yes"
            if (User.Identity.IsAuthenticated == true)
            {
                paciente.verified = "Yes";
            }

            else
            {
                paciente.verified = "No";
            }


            db.patients.Add(paciente);  //adds patients result to database
            db.SaveChanges();

            //Obtain the idquiz from the patient added to Database
            var idquiz = from p in db.patients
                         where p.idPatient == paciente.idPatient
                         orderby p.idQuiz descending
                         select p.idQuiz;

            //Add the severity of the patient
            //if riesgo -> medium or high
            //Calculate Severity:
            if (riesgo[0] > 50)
            {
                var severity = riesgo[1].ToString();                               //Calculate Severity
                severity severidad = new severity();            //new severity object
                severidad.idQuiz = idquiz.First();              //set the idQuiz to match severity IdQuiz Variable
                severidad.severity1 = severity;                 //Value for the severity
                db.severities.Add(severidad);                   //Add the severity to the Database
                db.SaveChanges();
            }


            //Store the Patients Answers to the database
            //Arguments: 
            //quizId -> identify the quiz of the answers
            //c -> Form with the answers
            storeAnswers(idquiz.First(), endoForm);

            //Return a patient type to the Risk view: 
            //Includes-> idquiz, paciente id, resultado, verified
            //float? thePercent = paciente.risk == null ? -1 : paciente.risk * 100;
            float lifetimeRiskPercent = (float)(paciente.risk * 100);
            float severityPercent = (float)(riesgo[1]* 100);
            ViewBag.RiskPercent = lifetimeRiskPercent;
            ViewBag.SeverityPercent = severityPercent;
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
            for (int x = 2; x < c.Count; x++)
            {

                string ans = c.GetKey(x);                   //get the answer from the form

                var ques = from q in data                   //Select the question id by checking
                           where q.abbr.Equals(ans)         //the abbreviation from the form in the 
                           select q.idQuestion;             //question table -> returns the ID of the question

                int idpreg = (int)ques.FirstOrDefault();     //get the first value in 

                //Add the answers to the questions 
                respuestas.answer = c.Get(x);               //the answer to the question
                respuestas.idQuestion = idpreg;             //quiestion id of the answer
                respuestas.idQuiz = idq;                    //quiz of the answered question

                //Store all the answers in an array for risk calculation

                db.endoanswers.Add(respuestas);           //adds patient answers to database
                db.SaveChanges();                           //save changes in the database
            }

        }

        public object[,] endoAnswerlist(FormCollection endoAnswersCollection)
        {
            object[,] endoAnswerList = new object[endoAnswersCollection.Count - 2, 2];

            string b = "";
            Type d = b.GetType();
          
            //Add all user answers coming from the screen to an array
            for (int x = 2; x < endoAnswersCollection.Count; x++)
            {                 
                if (endoAnswersCollection.Get(x) == "Si")
                {
                    endoAnswerList[x - 2, 0] = 1;
                }

                else if (endoAnswersCollection.Get(x) == "No" || endoAnswersCollection.Get(x) == "No Aplica" || endoAnswersCollection.Get(x) == null || endoAnswersCollection.Get(x) == "")
                {
                    endoAnswerList[x - 2, 0] = 0;
                }
                    
                else if (endoAnswersCollection.Get(x).GetType().Equals(d))
                {
                    endoAnswerList[x - 2, 0] = 1;
                }

                else
                   
                    endoAnswerList[x - 2, 0] = endoAnswersCollection.Get(x);
                    endoAnswerList[x - 2, 1] = endoAnswersCollection.GetKey(x);
            }

            // Symptoms Array
            string[] userSymptomsList = endoAnswersCollection.Get(endoAnswersCollection.Count - 1).Split(',');

            //Big array to store all user answers and symptoms
            var allSymptomsLength = db.symptoms.Count();
            object[,] allUserAnswers = new object[(endoAnswersCollection.Count + allSymptomsLength) - 3, 2];

            //Add all user answers coming from the array of answers to the big array allUserAnswers[]
            for (int y = 0; y < endoAnswerList.GetLength(0) - 1; y++)
            {
                allUserAnswers[y, 0] = endoAnswerList.GetValue(y, 0);
                allUserAnswers[y, 1] = endoAnswerList.GetValue(y, 1);
            }

            int allUserAnswersCounter = 0;
            //Add all user symptoms coming from the symptoms array of to the big array allUserAnswers[]
            foreach (var symptomItem in db.symptoms.ToList())
            {
                for (int z = 0; z <= userSymptomsList.Count() - 1; z++)
                {
                    if (symptomItem.abbr.Equals(userSymptomsList.ElementAt(z)))
                    {
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - ((allSymptomsLength - 1) - allUserAnswersCounter), 0] = 1;
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - ((allSymptomsLength - 1) - allUserAnswersCounter), 1] = userSymptomsList.ElementAt(z);
                        allUserAnswersCounter = allUserAnswersCounter + 1;
                        break;
                    }

                    else if (z == userSymptomsList.Count() - 1)
                    {
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - ((allSymptomsLength - 1) - allUserAnswersCounter), 0] = 0;
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - ((allSymptomsLength - 1) - allUserAnswersCounter), 1] = symptomItem.abbr;
                        allUserAnswersCounter = allUserAnswersCounter + 1;
                    }                   
                }
            }
            return allUserAnswers;
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
