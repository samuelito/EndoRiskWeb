using EndoRiskWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndoRiskWeb.Controllers
{
    /*V
     * RomeQuestionnaireController Class - responsible of determine patient diseases,
     * questions to be displayed, create the list of criteria, store patient answers,
     * and call the method to calculate the disease in the RomeDiseaseDiagnosisControler.
     * 
     * Methods:
     *      DisplayRomeQuestionnaire(int quizNum, int patientNum) 
     *      RomeAnswers(FormCollection c)
     *      NoRomeSymptoms()
     *      determineDiseases(int IDquiz)
     *      determinePreDiseaseOrder(String disease)
     *      generateNewRomeQuiz(int endoQuizID, int patientID)
     *      
     * @author Luz M. González González
     */
    public class RomeQuestionnaireController : Controller
    {
        //---------------------------General Variables---------------------------//
        
        private endoriskContext db = new endoriskContext();

        private List<String> displayDiseases = new List<String>(); //Lista de las enfermedades que trae cada s, sin repeticiones(Sin-pre)
        private List<String> orderDisplayDiseases = new List<String>(); //Lista de las enfermedades a desplegar en ORDEN.(Con-pre)
        private List<int> questionsNumbers = new List<int>(); //Contiene la lista de los números de las preguntas de la paciente.
        private List<String> trueDiseasesResult = new List<String>(); //Lista para guardar las enfermedades ciertas que reciba del diagnóstico.

        private String errorStatus = ""; //String used to determine if there is an error when searching symptoms, searching diseases, etc.
        private String errorStatusDiagnosis = ""; //String used to send the error status ocurred in the diagnosis methods.
        private String noSymptomMessage = "";

        private int IDquiz = 0;
        private int IDPatient = 0;
        private int IDromequiz = 0;
        //-----------------------------------------------------------------------//


        //-----------------------------------------Methods-----------------------------------------//

        /*
         * Method: RomeDiagnosisResult()
         * 
         */
        //public ActionResult RomeDiagnosisResult()
        //{
        //    String trueDisease = "";
        //    var allDiagnostics = db.romediagnosis.ToList();
        //    List<romediagnosi> diagnosticos = new List<romediagnosi>();

        //    //Call method to search the diagnostics of the true diseases.
        //    for (int k = 0; k < trueDiseasesResult.Count; k++)
        //    {
        //        trueDisease = trueDiseasesResult[k];

        //        if (!trueDisease.Equals(""))
        //        {
        //            diagnosticos.Add(allDiagnostics.Where(m => m.disease.Equals(trueDisease)).Last()); //Select the diagnosis for current [k] disease.
        //        }
        //    }

        //    ViewBag.Message = "El resultado es el siguiente: ";
        //    ViewBag.NoSymp = noSymptomMessage;
        //    ViewBag.diagnosticos = diagnosticos;

        //    return View(diagnosticos);
        //}

        /*
         * Method: RomeDiagnosisError()
         * 
         */
        public ActionResult RomeDiagnosisError()
        {
            ViewBag.Message = "Ha ocurrido un error en el diagnóstico.";
            return View();
        }

        /*
         * Method: RomeDisplayError()
         * 
         * Return: A view with an error message of the Rome Display.  
         * 
         * Return a view with an error message if the patient did not have any symptom
         * that triggers a Rome disease.
         */
        public ActionResult RomeDisplayError()
        {
            ViewBag.Message = "Ha ocurrido un error en cuestionario Rome III.";
            return View();
        }


        /*
         * Method: DisplayRomeQuestionnaire(int quizNum, int patientNum)
         * Parameters: 
         *      quizNum - the number of the quiz already created for the endorisk questionnaire.
         *      patientNum - the number of identification of the current patient.
         * 
         * This method, with the quizNum get the patient's diseases that are related to RomeIII,
         * verify pre-diseases, put all diseases in order, set the total questions in order without
         * repetitions, and finally displays them.
         */
        public ActionResult DisplayRomeQuestionnaire(int quizNum, int patientNum) 
        {
            //Get the quiz ID and patient ID from the parameters.  
            IDquiz = quizNum;
            IDPatient = patientNum;

            System.Diagnostics.Debug.Write("\nQuiz: " + IDquiz + " Paciente: " + IDPatient + "\n"); //For testing

            /*
             * Logic to verify if the pair of IDQuiz and IDpatient is a real current patient in the system.
             * If the list of patient with this IDQuiz and IDPatient is empty, send an empty view, with the
             * error message.
             */
            if(!(db.patients.Where(m => m.idQuiz == IDquiz && m.idPatient == IDPatient).ToList().Count > 0))
            {
                ViewBag.Message = "El número de quiz y de paciente que está intentanto accesar no está en el sistema.";
                List<questionsRome> romeQuestionsListi = new List<questionsRome>(); //Empty list to send to the view.
                return View(romeQuestionsListi); //Returns an empty view.
            }

            /*
             * Call the method to determine the diseases of this patient's quiz, according to the symptoms.
             */
            displayDiseases = determineDiseases(IDquiz); 

            /*
             * Logic to verify if the patient did not has any true symptoms or the
             * list of patient symptoms related to Rome Questionnaire is empty,
             * (No symptoms related to Functional Gastrointestinal Disorders)
             */
            //if(errorStatus.Equals("No Rome Symptoms") || errorStatus.Equals("No True Symptoms"))
            //{
            //    ViewBag.Message = "Se determinó que usted no tiene síntomas relacionados a este cuestionario de enfermedades gastrointestinales";
            //    List<questionsRome> romeQuestionsListi = new List<questionsRome>(); //Empty list to send to the view.
            //    return View(romeQuestionsListi);
            //}
            String mensaje = "Se determinó que usted no tiene síntomas relacionados a este cuestionario de enfermedades gastrointestinales";
            ViewBag.Message = mensaje;

            if (displayDiseases.Count == 0)
            {
                //String mensaje = "Se determinó que usted no tiene síntomas relacionados a este cuestionario de enfermedades gastrointestinales";
                //ViewBag.Message = mensaje;
                //List<romeDisplay> romeQuestionsListi = new List<romeDisplay>(); //Empty list to send to the view.
                romeDisplay romeQuestionsListi = new romeDisplay();
                return View(romeQuestionsListi);
            }

            System.Diagnostics.Debug.Write("\nDeben estar las enfermedades de la paciente según sus síntomas.\n"); //For testing

            //At this point variable displayDiseases has all diseases obtained from the symptoms
            //but did not include pre-diseases.  In the next step, pre-diseases should be added.


            /* 
             * Logic to determine the order in which diseases should be calculated. (LIST OF STRINGS)
             */        

            var dependencies = db.romedependencies.ToList(); //Get all the dependencies of the diseases.
            bool orderResult;

            for (int i = 0; i < displayDiseases.Count; i++)
            {
                if (dependencies.Select(m => m.disease).Contains(displayDiseases[i]))
                {
                    orderResult = determinePreDiseaseOrder(displayDiseases[i]); //Call the method to store diseases in order, according to their pre-diseases dependencies.

                    if (!orderDisplayDiseases.Contains(displayDiseases[i]))
                    {
                        orderDisplayDiseases.Add(displayDiseases[i]); //Add the disease that generates the first call of the recursive method.
                    }
                }
                else
                {
                    if (!orderDisplayDiseases.Contains(displayDiseases[i]))
                    {
                        orderDisplayDiseases.Add(displayDiseases[i]); //Add the disease to the list in any order if it did not have any pre-disease dependency.
                    }              
                }
            }

            System.Diagnostics.Debug.Write("\nDeben estar ya las enfermedades de la paciente originales, más las pre-enfermedades en orden\n"); //For testing

            //At this point variable orderDisplayDiseases has all diseases obtained from the symptoms and
            //the pre-diseases.  This are the ones that should be "showed" (the questions) to the user.


            /* 
             * Logic to select the desired diseases criteria and questions items in order. (LIST OF VAR TYPE DISEASES)
             */
            String primera = orderDisplayDiseases[0]; //Se tiene que hacer esto porque si se pone directo dentro del equals de abajo, tira un exception.
            var patientDiseases = db.diseases.Where(m => m.disease1.Equals(primera)).ToList(); //Contiene las lista de diseases con id de preguntas, criterio, valor.
            System.Diagnostics.Debug.Write("Enfermedad: " + primera); //For testing

            for (int i = 1; i < orderDisplayDiseases.Count; i++)
            {
                String siguiente = orderDisplayDiseases[i];
                System.Diagnostics.Debug.Write("Enfermedad: " + siguiente + "\n"); //For testing
                patientDiseases.AddRange(db.diseases.Where(m => m.disease1.Equals(siguiente)).ToList());
            }
            System.Diagnostics.Debug.Write("\nDeben estar la lista de variables tipo 'disease'='idDisease, disease, idRomeQuestion, criteria, comparedValue' de las enfermedades que son. \n"); //For testing

            //At this point a list of variables of type disease, which includes 'idDisease, disease, idRomeQuestion, criteria, comparedValue'
            //from all the supposed diseased and pre-diseases should be created.


            /* 
             * Logic to select the questions in order without repetitions. (LIST OF VAR TYPE ROMEQUESTIONS)
             */
            var allquestions = db.romequestions.ToList(); //Contains all the questions stored in the DB.
            int firstID = (int) allquestions[0].idRomeQuestion; //To add a first dummy element to create var questions.     
            var questions = db.romequestions.Where(m => m.idRomeQuestion.Equals(firstID)).ToList(); //Variable questions will contains all the questions to be displayed
            //In this variable questions, a first "dummy" element is added for the creation, and at the end this element is removed.

            for(int j = 0; j < allquestions.Count; j++)
            {
                int nextID = (int) allquestions[j].idRomeQuestion;

                if(patientDiseases.Select(m => m.idRomeQuestion).ToList().Contains(nextID))
                {
                    System.Diagnostics.Debug.Write("Se añadió pregunta: " + nextID + "\n"); //For testing
                    questions.Add(allquestions[j]);   
                }
            }
            questions.RemoveAt(0); //Removes first dummy element.
            System.Diagnostics.Debug.Write("\nDeben estar las preguntas que son y en orden.\n"); //For testing

            //At this point the list of all the questions that needs to be displayed to the user
            //have been created.


            /*
             * Logic to create the list of numbers of this questions, to be used when the answers are stored.
             */
            questionsNumbers = questions.Select(m => m.idRomeQuestion).ToList(); //List of integers that contains the idRomeQuestions who will be used when the answers are stored.


            /* 
             * Logic to display the questions with their choices in the view. 
             */
            var choices = db.romechoices.ToList();      //variable contains choices for Rome questions

            List<questionsRome> romeQuestionsList = new List<questionsRome>(); //New list of questionsRome items.
            questionsRome question = null; //New questionsRome item

            foreach(var item in questions)
            {         
                question = new questionsRome(); //Create new question
  
                question.questionRome = item.romeQuestion1; //Set "questionRome" field of 'question' to the "romeQuestion1" field of the 'item' variable   
                question.questionID = (int)item.idRomeQuestion; //Set "questionID" field of 'question' to the "idRomeQuestion" field of the 'item' variable          
                question.choiceSet = item.romeChoice; //Set "choiceSet" field of 'question' to the "romeChoice" field of the 'item' variable 
                question.choices = new List<string>(); //Set "choice" field of 'question' with a new List of strings

                foreach(var item2 in choices)
                {
                    if(question.choiceSet.Equals(item2.romeChoice1))
                    {
                        question.choices.Add(item2.romeOption.ToString());
                    }
                }
                romeQuestionsList.Add(question); //Add the new created question structure to the list of all the questions of Rome III
            }


            /* 
             * Finally return the view for this Action method.
             */
            romeDisplay modelForView = new romeDisplay();

            modelForView.questions = new List<questionsRome>();
            modelForView.diseasesList = new List<String>();
            modelForView.numberList = new List<int>();

            modelForView.questions = romeQuestionsList;
            modelForView.diseasesList = orderDisplayDiseases;
            modelForView.numberList = questionsNumbers;

            //ViewBag.Message = "Por favor conteste las siguientes preguntas:";
            ViewBag.quiz = quizNum;
            ViewBag.patient = patientNum;
            return View(modelForView);
        }


        /*
         * Method: RomeAnswers(FormCollection c)
         * Parameter: FormCollection of the answers of the patient
         * 
         * This method stores the answers of the patient in the database.
         * And finally calls the method of the RomeDiseaseDiagnosisController
         * to determine the diseases.
         */
        public ActionResult RomeDiagnosisResult(int quizNum, int patientNum, FormCollection c)
        {
            IDromequiz = generateNewRomeQuiz(quizNum, patientNum);
            System.Diagnostics.Debug.Write("\nRome Quiz Generado es: " + IDromequiz + "\n"); //For testing

            displayDiseases = determineDiseases(quizNum); //Determine diseases again

            /* 
             * Logic to determine the order in which diseases should be calculated. (LIST OF STRINGS)
             */

            var dependencies = db.romedependencies.ToList(); //Get all the dependencies of the diseases.
            bool orderResult;

            for (int i = 0; i < displayDiseases.Count; i++)
            {
                if (dependencies.Select(m => m.disease).Contains(displayDiseases[i]))
                {
                    orderResult = determinePreDiseaseOrder(displayDiseases[i]); //Call the method to store diseases in order, according to their pre-diseases dependencies.

                    if (!orderDisplayDiseases.Contains(displayDiseases[i]))
                    {
                        orderDisplayDiseases.Add(displayDiseases[i]); //Add the disease that generates the first call of the recursive method.
                    }
                }
                else
                {
                    if (!orderDisplayDiseases.Contains(displayDiseases[i]))
                    {
                        orderDisplayDiseases.Add(displayDiseases[i]); //Add the disease to the list in any order if it did not have any pre-disease dependency.
                    }
                }
            }

            System.Diagnostics.Debug.Write("\nDeben estar ya las enfermedades de la paciente originales, más las pre-enfermedades en orden\n"); //For testing


            for (int i = 1; i < c.Count; i++) //Starts in 1, because the first element in the FormCollection is not part of the answers
            {
                romeanswer respuesta = new romeanswer(); //Create a new instance of a romequestion response.
                var romePreguntas = db.romequestions.ToList(); //List of all the questions in the DB.  Required to search the romeChoice field of each question.
                int idPreg = Convert.ToInt32(c.GetKey(i)); //ID de pregunta en la lista


                System.Diagnostics.Debug.Write("Numero de Pregunta: " + idPreg + "\n"); //For testing

                //questionChoice contains the specific choice set of the question
                var questionChoice = romePreguntas.Where(m => m.idRomeQuestion==idPreg).Select(m => m.romeChoice).First();

                //RomeOpciones contains the set of values of the specific choice set (ex. A) of the current question.
                var romeOpciones = db.romechoices.Where(m => m.romeChoice1.Equals(questionChoice)).ToList();               

                String valor = c.Get(i).ToString(); //Get the response of the user
                int valorEntero = -1; //Default integer value of the response (-1 implies a N/A answer)

                if(romeOpciones.Select(m => m.romeOption).Contains(valor)) //To verify if the answer is in the values of the romeOptions for the choice set 'questionChoice'
                {
                    valorEntero = (int) romeOpciones.Where(m => m.romeOption.Equals(valor)).Select(j => j.value).First(); //If the value exist, get its integer value.
                }

                respuesta.idRomeQuiz = IDromequiz;
                respuesta.idRomeQuestion = idPreg;
                respuesta.answer = valorEntero;

                //System.Diagnostics.Debug.Write("Se guardó respuesta para quiz: " + IDromequiz + " Preg#: " + questionsNumbers[idPreg] + " Valor: " + valorEntero + "\n"); //For testing
                
                db.romeanswers.Add(respuesta);
                db.SaveChanges();
            }

            /*
             * Logic to finally (if the parameters are valid) call the methods to calculate the diagnosis of Rome III diseases, and call the error or diagnosis view.
             */
            if((orderDisplayDiseases.Count > 0) && IDromequiz != 0)
            { 
                RomeDiseaseDiagnosisController diagnostico = new RomeDiseaseDiagnosisController();            
                trueDiseasesResult = diagnostico.Diagnostic(IDromequiz, orderDisplayDiseases); //Le envio el quiz que es, y la lista de enfermedades que debo calcular y recibo la lista de enfermedades ciertas o el error, si hubo alguno.
                
                if(trueDiseasesResult.Count == 0)
                {
                    //Enviarle mensaje de que dado a sus síntomas y contestaciones no tiene RomeIII diseases.
                    ViewBag.NoSymp = "*No presenta ninguna enfermedad gastrointestinal de acuerdo al criterio 'Rome'";
                    return View();
                }

                else
                {
                    if(trueDiseasesResult.Contains("ERROR"))
                    {
                        errorStatusDiagnosis = trueDiseasesResult.Last();
                        //String generalError = "No se ha podido determinar sus enfermedades para este cuestionario.";
                        return RedirectToAction("RomeDiagnosisError", "RomeQuestionnaire");
                    }

                    else
                    {                       
                        String trueDisease = "";
                        var allDiagnostics = db.romediagnosis.ToList();
                        List<romediagnosi> diagnosticos = new List<romediagnosi>();

                        //Call method to search the diagnostics of the true diseases.
                        for (int k = 0; k < trueDiseasesResult.Count; k++)
                        {
                            trueDisease = trueDiseasesResult[k];

                            if (!trueDisease.Equals(""))
                            {
                                diagnosticos.Add(allDiagnostics.Where(m => m.disease.Equals(trueDisease)).Last()); //Select the diagnosis for current [k] disease.
                            }
                        }

                        ViewBag.Message = "El resultado es el siguiente: ";

                        return View(diagnosticos);
                    }
                }
            }

            return RedirectToAction("RomeDisplayError", "RomeQuestionnaire");
        }


        /*
         * Method: determineDiseases(int IDquiz)
         * Parameter: ID of the quiz where the system will go to verify the symptoms this patient has.
         * 
         * According to the patient's symptoms and the list of "Symptoms to Diseases"(RomeIII) in the DB
         * this method will generate a string list containing all the names of the diseases without 
         * repetitions that will be asked to the patient.
         */
        public List<String> determineDiseases(int IDquiz)
        {
            List<String> selectionOfDiseases = new List<String>(); //List that will contain the first diseases of the patient
            var patientS = db.patientsymptoms.Where(m => m.idQuiz == IDquiz && m.hasSymptom == 1).Select(m => m.symptom).ToList(); //Get only true symptoms of the desired quiz.

            if (patientS.Count > 0) //Verify if the patient has true symptoms
            {
                foreach (var item in patientS)
                {
                    var symptomsToDisease = db.symptomstodiseases.Where(m => m.symptom2.Equals(item)).ToList(); //In each item there is a true symptom.

                    if (symptomsToDisease.Count() > 0) //Verify if the symptom tested, brings some Rome disease to the patient
                    {
                        for (int i = 0; i < symptomsToDisease.Count(); i++)
                        {
                            System.Diagnostics.Debug.Write("Enfermedad que trae: " + item.ToString() + " es: " + symptomsToDisease[i].disease2.ToString() + "\n"); //For testing

                            if (!selectionOfDiseases.Contains(symptomsToDisease[i].disease2.ToString()))
                            {
                                selectionOfDiseases.Add(symptomsToDisease[i].disease2.ToString());
                            }
                        }
                    }
                }

                if(!(selectionOfDiseases.Count > 0))
                {
                    errorStatus = "No Rome Symptoms"; //From the true symptoms of the patient, none of the symptoms are related to Rome Questionnaire
                    selectionOfDiseases.Add(errorStatus); //Añade a la lista el error
                }
            }

            else
            {
                errorStatus = "No True Symptoms"; //No true symptoms to compare  
                selectionOfDiseases.Add(errorStatus); //Añade a la lista el error
            }

            //Ahora devuelvo la lista de strings con las enfermedades
            return selectionOfDiseases;
        }
 
        /*
         * Method: determinePreDiseaseOrder(String disease)
         * Parameter: String of the disease to determine their previous diseases.
         * 
         * A disease name (string) is received as a parameter and this method will check in the
         * "romedependencies" table in the database, to determine if this diseases has pre-diseases
         * to calculate.  This method use recursion in case this pre-disease has another predisease.
         * The method modify the orderDisplayDiseases list var, in where all the diseases required
         * will be in order.
         */

        public bool determinePreDiseaseOrder(String disease)
        {
            var diseaseDependencies = db.romedependencies.Where(m => m.disease.Equals(disease)).ToList();

            foreach(var item in diseaseDependencies)
            {
                if(db.romedependencies.Select(m => m.disease).Contains(item.preDisease))
                {
                    determinePreDiseaseOrder(item.preDisease); //**Recursion here
                 
                    if (!(orderDisplayDiseases.Contains(item.disease)))
                    {
                        orderDisplayDiseases.Add(item.preDisease);
                    }                   
                    
                    return true;
                }

                else if(!(orderDisplayDiseases.Contains(item.preDisease)))
                {
                    orderDisplayDiseases.Add(item.preDisease);
                }
            }
            return true;
        }

        /*
         * Method: generateNewRomeQuiz
         * Parameters: ID of the quiz for which a RomeQuestionnaire wants to be created
         *             ID of the patient
         * Return: Integer with the ID number of the new Rome Quiz         
         * 
         * According to this two parameters, a new number is generated in the dababase 
         * for this new quiz
         */
        public int generateNewRomeQuiz(int endoQuizID, int patientID)
        {
            romequestionnaire cuestionarioRome = new romequestionnaire();
            cuestionarioRome.idQuiz = endoQuizID;
            cuestionarioRome.idPatient = patientID;

            var currentRomes = db.romequestionnaires.Where(m => m.idQuiz.Equals(endoQuizID) && m.idPatient.Equals(patientID)).ToList();

            if(currentRomes.Count != 0)
            {
                errorStatus = "Error: There is a Rome Quiz with these patient values.";
                RedirectToAction("RomeDiagnosisError");
            }

            db.romequestionnaires.Add(cuestionarioRome);
            db.SaveChanges();

            var romeCreado = db.romequestionnaires.Where(m => m.idQuiz.Equals(endoQuizID) && m.idPatient.Equals(patientID)).Select(i => i.idRomeQuiz).ToList();

            return (int) romeCreado.Last();
        }

        //-----------------------------------------------------------------------------------------//
    }
}