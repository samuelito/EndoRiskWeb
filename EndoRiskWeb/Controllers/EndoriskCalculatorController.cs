using EndoRiskWeb.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

/**
 * @author Luz M. González
 * Controller for the acctions required to: display Endorisk questions,
 * receive Endorik questions, and display Endorisk Result.
 **/
 
namespace EndoRiskWeb.Controllers
{
    public class EndoriskCalculatorController : Controller
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
        // int questionIndex = 0;
        // int symptomIndex = 0;

        /*
         * Documentation: Samuel
         * 
         * View for the Index in Home
         * Index shows the Question for Endometriosis Calculator
         * 
         */
        public ActionResult EndoriskQuestions()
        {
            IndexViewModel allQuestions = new IndexViewModel();
            List<IndexViewModel> allQuestionsList = new List<IndexViewModel>();
            allQuestions.quest = new List<questions>();
            allQuestions.symp = new List<symptom>();
            allQuestions.prexCond = new List<preExistingConditions>();

            allQuestions.quest.ToList();
            allQuestions.symp.ToList();
            allQuestions.prexCond.ToList();
            allQuestionsList.Add(allQuestions);

            var endoQuestionList = db.endoquestions.ToList();      //variable contains all questions for Endometriosis
            var endoChoiceList = db.endochoices.ToList();        //variable contains choices for endometriosis questions
            var symptomList = db.symptoms.ToList();
            var prexCondList = db.preExistingConditions.ToList();

            questions q = new questions();                     //q is a new variable of type : questions
            symptom s = new symptom();
            preExistingConditions prexCond = new preExistingConditions();


            foreach (var questionItem in endoQuestionList)
            {
                q = new questions();
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
            {
                s = new symptom();
                symptomList = new List<symptom>();
                s.symptom1 = symptomItem.symptom1;
                s.idSymptom = symptomItem.idSymptom;
                s.abbr = symptomItem.abbr;

                allQuestions.symp.Add(s);

            }

            foreach (var prexCondItem in prexCondList)
            {
                prexCond = new preExistingConditions();
                prexCondList = new List<preExistingConditions>();
                prexCond.condition = prexCondItem.condition;            //add the questions to q
                prexCond.idPreCond = prexCondItem.idPreCond;         //adds the id of question to q
                prexCond.abbr = prexCondItem.abbr;                         //adds the abbreviation to q


                allQuestions.prexCond.Add(prexCond);
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
        static Semaphore semaphore = new Semaphore(1, 1);
        public ActionResult EndoriskResult(FormCollection endoForm)
        {
            
            //Model to store patients data in Database
            patient paciente = new patient();


            //Calculate Lifetime Risk: Using R prediction models
            //verify convertions of parameters of the required data
            //Testing example using linear prediction
            object[,] answerList = endoAnswerlist(endoForm);
            semaphore.WaitOne();
            C_Sharp_RExcel pred = new C_Sharp_RExcel();
            object [] prediccion = pred.Prediction(answerList);
            double riesgo = (double) prediccion[0];
            
            
            semaphore.Release();

            //Calculos del paciente
            int tempPaciente = 0;
            var numeroPaciente = endoForm.GetValue("PID").AttemptedValue;

            //Store the Patient ID in temporary variable
            if (!(numeroPaciente.Equals("")))
            {
                tempPaciente = Convert.ToInt32(numeroPaciente);
            }

            //Verify if patient ID input exist in the database
            var checkpatiente = db.patients.Where(m => m.idPatient.Equals(tempPaciente)).Select(m => m.idPatient).FirstOrDefault().ToString();

            //Verifiy if patient ID exist              
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

            paciente.risk = (float) riesgo;             //Lifetime risk result 
            paciente.severity = prediccion[1].ToString();
            paciente.time = DateTime.Now;                   //time of the quiz

            //Verify if logged in

            if (User.Identity.IsAuthenticated == true)
            {
                paciente.verified = User.Identity.Name.Split(',')[0];
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


            //Store the Patients Answers & Symptoms to the database
            var symp = endoForm.GetValue("symp").AttemptedValue;
            var prexCond = endoForm.GetValue("prexCond").AttemptedValue;
            storeAnswers(idquiz.First(), endoForm);
            storeSymptoms(idquiz.First(), symp);
            storeConditions(idquiz.First(), prexCond);

            //Return a patient type to the Risk view: 
            //Includes-> idquiz, paciente id, resultado, verified
            //float? thePercent = paciente.risk == null ? -1 : paciente.risk * 100;
            float lifetimeRiskPercent = (float)(riesgo * 100);
            string severityPercent = prediccion[1].ToString();
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
            using (endoriskContext a = new endoriskContext())
            {
                endoanswer respuestas;
                var data = db.endoquestions;

                //Go through every element in the form
                //Starts at 3 since first elements are not part of the questions
                //(Validation - Enable Test - PID ) -> First Elements
                // x < c.count-2 _> since last 2 element in form contains the symptoms and preconditions.
                
                for (int x = 3; x < c.Count; x++)
                {
                    if (!(c.GetKey(x).Equals("symp")  || c.GetKey(x).Equals("prexCond")))
                    {
                        respuestas = new endoanswer();
                        string ans = c.GetKey(x);                   //get the answer from the form

                        var ques = from q in data                   //Select the question id by checking
                                   where q.abbr.Equals(ans)         //the abbreviation from the form in the 
                                   select q.idQuestion;             //question table -> returns the ID of the question

                        int idpreg = (int)ques.FirstOrDefault();     //get the first value in 

                        //Add the answers of the question 
                        respuestas.answer = c.Get(x);               //the answer to the question
                        respuestas.idQuestion = idpreg;             //quiestion id of the answer
                        respuestas.idQuiz = idq;                    //quiz of the answered question

                        //Store all the answers in an array for risk calculation
                        a.endoanswers.Add(respuestas);
                        a.SaveChanges();
                    }
                }
            }

        }
        /*
         * Documentation: Samuel
         * Function to store the symptoms of the quiz in the database
         * Parameters: 
         *  idq = id of the quiz
         *  symp = string containing the symptoms
         */
        public void storeSymptoms(int idq, string symp)
        {
            using (endoriskContext s = new endoriskContext())
            {
                patientsymptom sintoma;
                var data = db.symptoms;
                // Symptoms Array
                string[] userSymptomsList = symp.Split(',');

                //Go through every element in the symptoms list
                foreach (var item in data)
                {
                    sintoma = new patientsymptom();
                    sintoma.idQuiz = idq;
                    sintoma.symptom = item.abbr;
                    if (userSymptomsList.Contains(item.abbr))
                    {
                        sintoma.hasSymptom = 1;
                    }
                    else
                    {
                        sintoma.hasSymptom = 0;
                    }

                    s.patientsymptoms.Add(sintoma);
                    s.SaveChanges();
                }
            }
        }
        /*
         * Documentation: Samuel
         * Function to store the symptoms of the quiz in the database
         * Parameters: 
         *  idq = id of the quiz
         *  preCond = string containing preconditions
         */
        public void storeConditions(int idq, string preCond)
        {
            using (endoriskContext pC = new endoriskContext())
            {
                patientsprecond cond;
                
                var data = db.preExistingConditions;
                // Conditions Array
                string[] userPreCondList = preCond.Split(',');

                //Go through every element in the pre-existing conditions list
                foreach (var item in data)
                {
                    cond = new patientsprecond();
                    cond.idQuiz = idq;
                    cond.preCondition = item.condition;
                    cond.preAbbr= item.abbr;
                    if (userPreCondList.Contains(item.abbr))
                    {
                        cond.haspreCond = 1;
                    }
                    else
                    {
                        cond.haspreCond = 0;
                    }

                    pC.patientspreconds.Add(cond);
                    pC.SaveChanges();
                }
            }
        }


        public object[,] endoAnswerlist(FormCollection endoAnswersCollection)
        {
            object[,] endoAnswerList = new object[endoAnswersCollection.Count - 3, 2];        

            //Add all user answers coming from the screen to an array
            for (int x = 3; x < endoAnswersCollection.Count; x++)
            {
                if (endoAnswersCollection.Get(x) == "Sí")
                {
                    endoAnswerList[x - 3, 0] = 1;
                    endoAnswerList[x - 3, 1] = endoAnswersCollection.GetKey(x);
                }

                else if (endoAnswersCollection.Get(x) == "No" || endoAnswersCollection.Get(x) == "No Aplica" || endoAnswersCollection.Get(x) == "No sé" || endoAnswersCollection.Get(x) == null || endoAnswersCollection.Get(x) == "")
                {
                    endoAnswerList[x - 3, 0] = 0;
                    endoAnswerList[x - 3, 1] = endoAnswersCollection.GetKey(x);
                }

                else if (endoAnswersCollection.Get(x).Equals("Hermana"))
                {
                    endoAnswerList[x - 3, 0] = 1;
                    endoAnswerList[x - 3, 1] = endoAnswersCollection.GetKey(x);
                }

                else if (endoAnswersCollection.GetKey(x).Equals("symp") || endoAnswersCollection.GetKey(x).Equals("prexCond"))
                {
                   
                }

                else if (endoAnswersCollection.GetKey(x).Equals("AWSS"))
                { 
                    endoAnswerList[x - 4, 0] = endoAnswersCollection.Get(x);
                    endoAnswerList[x - 4, 1] = endoAnswersCollection.GetKey(x);
                }

                else if (endoAnswersCollection.Get(x) == "Madre" || endoAnswersCollection.Get(x) == "Hija" || endoAnswersCollection.Get(x) == "Abuela" || endoAnswersCollection.Get(x) == "Tía" || endoAnswersCollection.Get(x) == "Sobrina" || endoAnswersCollection.Get(x) == "Prima")
                {
                    endoAnswerList[x - 3, 0] = 0;
                    endoAnswerList[x - 3, 1] = endoAnswersCollection.GetKey(x);
                }

                else
                {
                    endoAnswerList[x - 3, 0] = endoAnswersCollection.Get(x);
                    endoAnswerList[x - 3, 1] = endoAnswersCollection.GetKey(x);
                }
            }

            // Symptoms Array
            //userSymptomsList = endoAnswersCollection.Get(endoAnswersCollection.Count - 2).Split(',');
           string[] userSymptomsList = endoAnswersCollection.GetValue("symp").AttemptedValue.Split(',');
            // Pre-existing conditions Array


            //Big array to store all user answers and symptoms
            var allSymptomsLength = db.symptoms.Count();
            var allPrexCondLength = db.preExistingConditions.Count();

            object[,] allUserAnswers = new object[(endoAnswersCollection.Count + allSymptomsLength + allPrexCondLength) - 5, 2];

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
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - (((allSymptomsLength - 1) + (allPrexCondLength)) - allUserAnswersCounter), 0] = 1;
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - (((allSymptomsLength - 1) + (allPrexCondLength)) - allUserAnswersCounter), 1] = userSymptomsList.ElementAt(z);
                        allUserAnswersCounter = allUserAnswersCounter + 1;
                        break;
                    }

                    else if (z == userSymptomsList.Count() - 1)
                    {
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - (((allSymptomsLength - 1) + (allPrexCondLength)) - allUserAnswersCounter), 0] = 0;
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - (((allSymptomsLength - 1) + (allPrexCondLength)) - allUserAnswersCounter), 1] = symptomItem.abbr;
                        allUserAnswersCounter = allUserAnswersCounter + 1;
                    }
                }
            }

            string[] userPrexCondList = endoAnswersCollection.GetValue("prexCond").AttemptedValue.Split(',');
            allUserAnswersCounter = 0;
            //Add all user symptoms coming from the symptoms array of to the big array allUserAnswers[]
            foreach (var prexCondItem in db.preExistingConditions.ToList())
            {
                for (int z = 0; z <= userPrexCondList.Count() - 1; z++)
                {
                    if (prexCondItem.abbr.Equals(userPrexCondList.ElementAt(z)))
                    {
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - ((allPrexCondLength - 1) - allUserAnswersCounter), 0] = 1;
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - ((allPrexCondLength - 1) - allUserAnswersCounter), 1] = userPrexCondList.ElementAt(z);
                        allUserAnswersCounter = allUserAnswersCounter + 1;
                        break;
                    }

                    else if (z == userPrexCondList.Count() - 1)
                    {
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - ((allPrexCondLength - 1) - allUserAnswersCounter), 0] = 0;
                        allUserAnswers[(allUserAnswers.GetLength(0) - 1) - ((allPrexCondLength - 1) - allUserAnswersCounter), 1] = prexCondItem.abbr;
                        allUserAnswersCounter = allUserAnswersCounter + 1;
                    }
                }
            }
            return allUserAnswers;
        }
    }

}
