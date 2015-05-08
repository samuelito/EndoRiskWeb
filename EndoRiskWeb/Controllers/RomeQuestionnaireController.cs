﻿using EndoRiskWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndoRiskWeb.Controllers
{    
    public class RomeQuestionnaireController : Controller
    {
        //---------------------------General Variables---------------------------//
        
        private endoriskContext db = new endoriskContext();

        private static List<String> displayDiseases = new List<String>(); //Lista de las enfermedades que se supone que sean, sin repeticiones(Sin-pre)
        private static List<String> orderDisplayDiseases = new List<String>(); //Lista de las enfermedades a desplegar en ORDEN.(Con-pre)
        private static List<int> questionsNumbers = new List<int>(); //Contiene la lista de los números de las preguntas de la paciente.
        private static List<disease> diseasesCriteria = new List<disease>(); //Lista de elementos tipo disease para las enfermedades que se van a calcular.

        private static String errorStatus = ""; //String used to determine if there is an error when searching symptoms, searching diseases, etc.

        private int IDquiz = 13;
        private int IDPatient = 23;
        //private String passWord = "123"; //No es utilizado por el momento
        //-----------------------------------------------------------------------//



        //--------------------------------Methods--------------------------------//

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
            //if (User.Identity.IsAuthenticated)
            //{
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                    
                    //Por el momento obtengo el Quiz y el Paciente por parámetro.

                    IDquiz = quizNum;
                    IDPatient = patientNum;


                    /*
                     * Utilizar esta lógica de enviar lista vacía con un mensaje para cuando el id de paciente con el quiz, no sea válido
                    if(IDquiz == 13 && passWord.Equals("12"))
                    {
                        List<questionsRome> romeQuestionsListi = new List<questionsRome>(); //lisa vacia para que no le salga nada en el view.
                        return View (romeQuestionsListi);
                    }
                    */

                    determineDiseases(IDquiz); //Call the method to determine the diseases of this patient's quiz, according to her symptoms.

                    //If patient has no symptoms
                    //Crear otro view para decir que no tiene enfermedades relacionadas y hacer un redirect to action
                    if (errorStatus.Equals("Patient has no symptoms to compare"))
                    {
                        return RedirectToAction("NoRomeSymptoms", "RomeQuestionnaire");
                    }

                    //Else continue normally:

                    System.Diagnostics.Debug.Write("\nDeben estar las enfermedades de la paciente según sus síntomas.\n"); //For testing

                    //At this point variable displayDiseases has all diseases obtained from the symptoms
                    //but did not include pre-diseases.  In the next step, pre-diseases should be added.


                    /* 
                     * Logic to determine the order in which diseases should be calculated. (LIST OF STRINGS)
                     */
                    var dependencies = db.romedependencies.ToList(); //Get all the dependencies of the diseases.

                    for (int i = 0; i < displayDiseases.Count; i++)
                    {
                        if (dependencies.Select(m => m.disease).Contains(displayDiseases[i]))
                        {
                            determineOrder(displayDiseases[i]); //Call the method to store diseases in order, according to their pre-diseases dependencies.

                            if (!orderDisplayDiseases.Contains(displayDiseases[i]))
                            {
                                orderDisplayDiseases.Add(displayDiseases[i]); //Add the disease that generates the first call of the recursive method.
                            }
                        }
                        else
                        {
                            orderDisplayDiseases.Add(displayDiseases[i]); //Add the disease to the list in any order if it did not have any pre-disease dependency.
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
                        System.Diagnostics.Debug.Write("Enfermedad: " + siguiente); //For testing
                        patientDiseases.AddRange(db.diseases.Where(m => m.disease1.Equals(siguiente)).ToList());
                    }
                    System.Diagnostics.Debug.Write("\nDeben estar la lista de variables tipo 'disease'='idDisease, disease, idRomeQuestion, criteria, comparedValue' de las enfermedades que son. \n"); //For testing

                    //At this point a list of variables of type disease, which includes 'idDisease, disease, idRomeQuestion, criteria, comparedValue'
                    //from all the supposed diseased and pre-diseases should be created.

                    diseasesCriteria = patientDiseases; //To send criteria to the RomeDiseaseDiagnosisController

                    /* 
                     * Logic to select the questions in order without repetitions. (LIST OF VAR TYPE ROMEQUESTIONS)
                     */
                    var allquestions = db.romequestions.ToList(); //Contains all the questions stored in the DB.
                    int firstID = (int)allquestions[0].idRomeQuestion; //To add a first dummy element to create var questions.     
                    var questions = db.romequestions.Where(m => m.idRomeQuestion.Equals(firstID)).ToList(); //Variable questions contains all the questions to be displayed

                    for (int j = 0; j < allquestions.Count; j++)
                    {
                        int nextID = (int)allquestions[j].idRomeQuestion;

                        if (patientDiseases.Select(m => m.idRomeQuestion).ToList().Contains(nextID))
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

                    foreach (var item in questions)
                    {
                        question = new questionsRome(); //Create new question

                        question.questionRome = item.romeQuestion1; //Set "questionRome" field of 'question' to the "romeQuestion1" field of the 'item' variable   
                        question.questionID = (int)item.idRomeQuestion; //Set "questionID" field of 'question' to the "idRomeQuestion" field of the 'item' variable          
                        question.choiceSet = item.romeChoice; //Set "choiceSet" field of 'question' to the "romeChoice" field of the 'item' variable 
                        question.choices = new List<string>(); //Set "choice" field of 'question' with a new List of strings

                        foreach (var item2 in choices)
                        {
                            if (question.choiceSet.Equals(item2.romeChoice1))
                            {
                                question.choices.Add(item2.romeOption.ToString());
                            }
                        }
                        romeQuestionsList.Add(question); //Add the new created question structure to the list of all the questions of Rome III
                    }

                    /* 
                     * Finally return the view for this Action method.                     
                     */
                    return View(romeQuestionsList);                    
                }

                else
                
                   // return View("~/Views/Notifications/AccessDenied.cshtml");
                
            //}
                                   
            return View("~/Views/Notifications/AccessDenied.cshtml");
           
        }


        /*
         * Method: RomeAnswers(FormCollection c)
         * Parameter: FormCollection of the answers of the patient
         * 
         * This method stores the answers of the patient in the database.
         * And finally calls the method of the RomeDiseaseDiagnosisController
         * to determine the diseases.
         */
        public ActionResult RomeAnswers(FormCollection c)
        {
            int newRomeQuiz = generateNewRomeQuiz(IDquiz, IDPatient);

            for (int i = 1; i < c.Count; i++) //Starts in 1, because the first element in the FormCollection is not part of the answers
            {
                romeanswer respuesta = new romeanswer(); //Create a new instance of an romequestion response.
                var romePreguntas = db.romequestions.ToList(); //List of all the questions in the DB.  Required to search the romeChoice field of each question.
                int idPreg = i - 1; //ID de pregunta en la lista

                //questionChoice contains the specific choice set of the question
                var questionChoice = romePreguntas.Where(m => m.idRomeQuestion==questionsNumbers[idPreg]).Select(m => m.romeChoice).First();

                //RomeOpciones contains the set of values of the specific choice set (ex. A) of the current question.
                var romeOpciones = db.romechoices.Where(m => m.romeChoice1.Equals(questionChoice)).ToList();               

                String valor = c.Get(i).ToString(); //Get the response of the user
                int valorEntero = -1; //Default integer value of the response (-1 implies a N/A answer)

                if(romeOpciones.Select(m => m.romeOption).Contains(valor)) //To verify if the answer is in the values of the romeOptions for the choice set 'questionChoice'
                {
                    valorEntero = (int) romeOpciones.Where(m => m.romeOption.Equals(valor)).Select(j => j.value).First(); //If the value exist, get its integer value.
                }

                respuesta.idRomeQuiz = newRomeQuiz;
                respuesta.idRomeQuestion = questionsNumbers[idPreg];
                respuesta.answer = valorEntero;

                System.Diagnostics.Debug.Write("Se guardó respuesta para quiz: " + newRomeQuiz + " Preg#: " + questionsNumbers[idPreg] + " Valor: " + valorEntero + "\n"); //For testing
                
                db.romeanswers.Add(respuesta);
                db.SaveChanges();
            }


            return RedirectToAction("QuestionsCriteria", "RomeDiseaseDiagnosis", new { quiz = IDquiz, enfermedades = orderDisplayDiseases, criteria = diseasesCriteria });
            // La lista que los tiene es: patientDiseases
            //diseaseQuestionsCriteria(String disease, IList<disease> diseaseList)
            //@Html.ActionLink("Cuestionario Enfermedades Gastrointestinales", "DisplayRomeQuestionnaire", "RomeQuestionnaire", new {quizNum = Model.idQuiz, patientNum = Model.idPatient}, null);
            //return RedirectToAction("EndoriskResult", "EndoriskCalculator"); //De aquí devolver al controller de RomeDiseaseDiagnosis
        }


        /*
         * Method: NoRomeSymptoms()
         * 
         * Return: A view with a message that patient has no Rome symptoms.        
         * 
         * Return a view with an error message if the patient did not have any symptom
         * that triggers a Rome disease.
         */
        public ActionResult NoRomeSymptoms()
        {
            return View();
        }


        /*
         * Method: determineDiseases(int IDquiz)
         * Parameter: ID of the quiz where the system will go to verify the symptoms this patient has.
         * 
         * According to the patient's symptoms and the list of "Symptoms to Diseases"(RomeIII) in the DB
         * this method will generate a string list containing all the names of the diseases without 
         * repetitions that will be asked to the patient.
         */
        public void determineDiseases(int IDquiz)
        {
            var patientS = db.patientsymptoms.Where(m => m.idQuiz == IDquiz && m.hasSymptom == 1).Select(m => m.symptom).ToList(); //Get only true symptoms of the desired quiz.

            if (patientS.Count > 0) //Verify if the patient has true symptoms
            {
                foreach (var item in patientS)
                {
                    var symptomsToDisease = db.symptomstodiseases.Where(m => m.symptom2.Equals(item)).ToList(); //In each item there is the true symptom I am looking for.

                    if (symptomsToDisease.Count() > 0) //Verify if the symptom tested, brings some disease to the patient
                    {
                        for (int i = 0; i < symptomsToDisease.Count(); i++)
                        {
                            System.Diagnostics.Debug.Write("Enfermedad que trae: " + item.ToString() + " es: " + symptomsToDisease[i].disease2.ToString()); //For testing

                            if (!displayDiseases.Contains(symptomsToDisease[i].disease2.ToString()))
                            {
                                displayDiseases.Add(symptomsToDisease[i].disease2.ToString());
                            }
                        }
                    }
                }
            }

            else
            {
                errorStatus = "Patient has no symptoms to compare"; //No hay síntomas ciertos con qué comparar.     
            }
        }


        /*
         * Method: determineOrder(String disease)
         * Parameter: String of the disease to determine their previous diseases.
         * 
         * A disease name (string) is received as a parameter and this method will check in the
         * "romedependencies" table in the database, to determine if this diseases has pre-diseases
         * to calculate.  This method use recursion in case this pre-disease has another predisease.
         * The method modify the orderDisplayDiseases list var, in where all the diseases required
         * will be in order.
         */
        public void determineOrder(String disease)
        {
            var diseaseDependencies = db.romedependencies.Where(m => m.disease.Equals(disease)).ToList();

            foreach(var item in diseaseDependencies)
            {
                if(db.romedependencies.Select(m => m.disease).Contains(item.preDisease))
                {
                    determineOrder(item.preDisease); //**Recursion here
                }

                else if(!orderDisplayDiseases.Contains(item.preDisease))
                {
                    orderDisplayDiseases.Add(item.preDisease);
                }
            }
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
        public int generateNewRomeQuiz(int endoQuizID, int patientID) //Test this method
        {
            romequestionnaire cuestionarioRome = new romequestionnaire();
            cuestionarioRome.idQuiz = endoQuizID;
            cuestionarioRome.idPatient = patientID;

            //db.romequestionnaires.Add(cuestionarioRome);
            //db.SaveChanges();

            var romeCreado = db.romequestionnaires.Where(m => m.idQuiz.Equals(endoQuizID) && m.idPatient.Equals(patientID)).Select(i => i.idRomeQuiz).ToList();

            return (int) romeCreado.Last();
        }

    }
}