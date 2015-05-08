using EndoRiskWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndoRiskWeb.Controllers
{
    public class RomeDiseaseDiagnosisController : Controller
    {
        //---------------------------General Variables---------------------------//

        private endoriskContext db = new endoriskContext(); //Instance variable of the database, to perform the search of data.

        private static int romequizID = 0; //ID del quiz de Rome 
        private static List<String> listOfDiseases = new List<String>(); //Lista de todas las enfermedades que recibe que se deben calcular
        //private static List<disease> listOfCriteria = new List<disease>();  //Lista de todos los criterios para calcular estas enfermedades. (esto lo podía buscar aquí también, pero verifico luego qué me conviene más)

        private static List<bool> questionsResults = new List<bool>(); //Lista de los resultados de cada comparacion de las preguntas para cada enfermedad en orden.

        private static List<String> truePreDiseases = new List<String>(); //Lista de las enfermedades que me van dando ciertas
        private static List<String> falsePreDiseases = new List<String>(); //Lista de las enfermedades que me van dando falsas

        private static String errorStatus = "";

        //-----------------------------------------------------------------------//


        //--------------------------------Methods--------------------------------//
        public ActionResult RomeDiagnosisResult()
        {
            return View();
        }

        public ActionResult RomeDiagnosisError()
        {
            ViewBag.Message = errorStatus;
            return View();
        }

        
        public ActionResult Diagnostic(int IDRomeQuiz, List<String> enfermedades)
        {
            romequizID = IDRomeQuiz;
            listOfDiseases = enfermedades;

             System.Diagnostics.Debug.Write("Quiz de Rome es: " + romequizID + "\n");

            for (int i = 0; i < listOfDiseases.Count; i++)
            {
                String eachDisease = listOfDiseases[i];
                List<disease> eachDiseaseCriteria = db.diseases.Where(m => m.disease1.Equals(eachDisease)).ToList();
           
                if(eachDiseaseCriteria.Count > 0)
                {
                    //Call questionsCriteria method:
                    questionsCriteria(eachDisease, eachDiseaseCriteria); //Envia la enfermedad que esté en la lista en el indice i, y sus criterios.

                    if (!errorStatus.Equals("")) //To verify there is no errors in the questionsCriteria method.
                    {
                        ViewBag.Message = errorStatus;
                        return RedirectToAction("RomeDiagnosisError");
                    }

                    //Call calculateDisease method:
                    bool resultado = calculateDisease(eachDisease);

                    if (!errorStatus.Equals("")) //To verify there is no errors in the calculateDisease method.
                    {
                        ViewBag.Message = errorStatus;
                        return RedirectToAction("RomeDiagnosisError");
                    }

                    else if (resultado)
                    {
                        truePreDiseases.Add(eachDisease);
                    }

                    else
                    {
                        falsePreDiseases.Add(eachDisease);
                    }
                }

                else
                {
                    errorStatus = "Error: No criteria for this disease in the DB.";
                    return RedirectToAction("RomeDiagnosisError");
                }
            }

            //Ahora aquí con las true diseases, envio los diagnósticos.****
                
            return RedirectToAction("RomeDiagnosisResult", "RomeDiseaseDiagnosis");
        }

        // GET: RomeDiseaseDiagnosis
        public void questionsCriteria(String enfermedad, List<disease> criterios)
        {
            questionsResults.Clear(); //Clear the content of this list, to store the new values

            //Set default values:
            int idquestion = 0; //Id de la pregunta que voy a buscar
            String comparisonSymbol = ""; //Symbolo de comparacion
            int testValue = -1;  //Valor con el que voy a comparar
            int patientAnswer = -1;  //Valor que contestó la paciente

            var patientRomeAnswers = db.romeanswers.Where(m => m.idRomeQuiz == romequizID).ToList();

            if(patientRomeAnswers.Count > 0) //Si hay contestaciones de esta paciente para el quiz.
            { 
                for(int i = 0; i < criterios.Count; i++) //For por la lista de los criterios.
                {
                    idquestion = (int) criterios[i].idRomeQuestion; //ID of the question to search
                    comparisonSymbol = criterios[i].criteria.ToString(); //Symbol used to compared the suppossed value, and the patient value
                    testValue = (int) criterios[i].comparedValue; // Value to compare with

                    if(patientRomeAnswers.Select(m => m.idRomeQuestion).Contains(idquestion)) //If the question number exist in the list of criteria
                    {
                        patientAnswer = (int) patientRomeAnswers.Where(m => m.idRomeQuestion == idquestion).Select(n => n.answer).Last();
                        questionsResults.Add(comparison(patientAnswer, testValue, comparisonSymbol)); //Añade resultado i
                        System.Diagnostics.Debug.Write("Resultado pregunta: " + i + " añadido para " + enfermedad + " es: " + questionsResults[i] + "\n");
                    }    
                    
                    else
                    {
                        errorStatus = "Error: No patient answer for a question.";
                        i = criterios.Count + 1; //To "break" the for.                       
                    }

                    System.Diagnostics.Debug.Write("\n");
                    //System.Diagnostics.Debug.Write("Resultado num: " + i + " añadido para " + enfermedad + " es: " + questionsResults[i] + "\n");
                }
            }

            else
            {
                errorStatus = "Error: No Rome answers for this patient.";
            }
        }

        /* 
         * Method to calculate each disease according to parameters.
         * Parameters:
         *      disease = is the text of the disease that should be calculated. (Text string must be equal to the one in the database)
         *      questionsResult = is the list of the results (true or false) of the comparison for each question in order, for the desired disease.
         *      truePreDiseases = string list that contains the name of the prediseases that have been calculated before, an its result was true. (Empty if no pre-diseases are fulfilled)
         *      truePreDiseases = string list that contains the name of the prediseases that have been calculated before, an its result was false. (Empty if no pre-diseases are not fulfilled)
         */
        public bool calculateDisease(String disease)   //, List<bool> questionsResult, List<String> truePreDiseases, List<String> falsePreDiseases)
        {
            bool step = false; //Hold the result of each "step" of the calculation.
            List<bool> stepList = new List<bool>();  //This list will contains the result of each step in the disease diagnosis.
            List<bool> parameterList = new List<bool>();  //This list will contains the result of each parameter in the step.


            //int num = Convert.ToInt32(string);

            int questionNumber = 0; //Number of the question in the disease to be evaluated
            bool comparedValue = false; //Value to compare the result of the question (default = false)
            
            var diseaseSteps = db.romesteps.Where(m => m.disease3.Equals(disease)).ToList(); //Get the steps of the 'disease' and pass them to a list      

            parameterList.Add(false); //Ensure the list never return empty

            if (diseaseSteps.Count != 0) //To ensure not to start the process if the list of steps is empty.
            {
                /* 
                 * Start the for to go for each step of the 'disease'.
                 */
                for(int i = 1; i <= diseaseSteps.Count; i++) 
                {
                    
                    var stepParameters = db.romeparameters.Where(m => m.step.Equals(i) && m.disease4.Equals(disease)).ToList(); //Get the 'parameters' of the step 'i'
                    
                    if(stepParameters.Count != 0) //To ensure not to start the comparison of parameter if the list is empty.
                    {
                        /* 
                         * Start the for to go for each 'parameter' of the 'step'.
                         */
                        foreach(var item in stepParameters) //Goes for each parameter of the 'step'
                        {
                            /*
                             * Set the compare value of each step
                             */
                            //Compared value will be used by all 3 options
                            comparedValue = false; //Value to compare the result of the question (default = false) 
      
                            if(item.boolValue.ToString().Equals("1"))
                            {
                                comparedValue = true;
                            }

                            System.Diagnostics.Debug.Write("Value to compare for parameter: " + comparedValue + "\n"); //For testing

                            /*
                             * Start the comparison to determine if the parameter is a question, a disease, another step, or if there is an undefined parameter(error message)
                             */
                            if(item.que.Equals("Q")) // || item.que.Equals("q") || item.que.Equals("Question") || item.que.Equals("question")) //Verify if the parameter of the step is a question
                            {
                                questionNumber = (int)item.cual; //Number of the question in the disease to be evaluated (foreach de item comienza desde 0)
                                System.Diagnostics.Debug.Write("Q - Numero de pregunta: " + questionNumber + "\n"); //For testing

                                //bool valorComparacionPregunta = false;
                                
                                bool valorComparacionPregunta = questionsResults[questionNumber-1].Equals(comparedValue);
    
                                parameterList.Add(valorComparacionPregunta); //Add the result of the parameter comparison to the method list.

                                System.Diagnostics.Debug.Write("Q - Parámetro número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString()  + ", resultado de comparación es: " + parameterList.Last().ToString() + "\n"); //For testing
                            }

                            else if(item.que.Equals("D")) //Verify if the parameter of the step is a disease
                            {
                                if(comparedValue) //If disease has to be true
                                {
                                    if(truePreDiseases.Contains(item.cual.ToString())) //If in the list of true prediseases exist the disease parameter, add true
                                    {
                                        parameterList.Add(true);
                                        System.Diagnostics.Debug.Write("D - Disease " + item.cual.ToString() + " is true, se añadió true a parámetros \n");
                                    }
                                }

                                else if(!comparedValue) //If disease has to be false
                                {
                                    if(falsePreDiseases.Contains(item.cual.ToString())) //If in the list of false prediseases exist the disease parameter, add true
                                    {
                                        parameterList.Add(false);
                                        System.Diagnostics.Debug.Write("D - Disease " + item.cual.ToString() + " is false, se añadió true a parámetros \n");
                                    }
                                }

                                else //Did not fulfill the comparison criteria.
                                {
                                    errorStatus = "Error: Some required disease have been not calculated before. \n";
                                    return false;
                                }

                                //System.Diagnostics.Debug.Write("Parámetro número: " + item.idOrder + " de " + item.disease4 + " step " + item.step  + ", es D y resultado de comparación es: " + parameterList.Last()); //For testing
                            }

                            else if(item.que.Equals("S")) //Verify if the parameter of the step is another step
                            {
                                int cual = (int) item.cual;

                                if(cual < i) //Lower than i, because i is the current step.
                                {
                                    if(stepList[cual-1].Equals(comparedValue))
                                    {
                                        parameterList.Add(true);
                                        System.Diagnostics.Debug.Write("S - Step " + item.cual.ToString() + " is true, se añadió true a parámetros \n");
                                    }
                                
                                    else
                                    {
                                        parameterList.Add(false);
                                        System.Diagnostics.Debug.Write("S - Step " + item.cual.ToString() + " is false, se añadió false a parámetros \n");
                                    } 
                                }

                                else
                                {
                                    //Se está intentando accesar un 'step' invalido.
                                    errorStatus = "Error: Trying to access an invalid step.";
                                    return false;
                                }
                            }

                            else
                            {
                                // Parámetro no está definido. Otro texto más
                                errorStatus = "Error: Parameter is not defined (Q, S, or D).";
                                return false;
                            }
                        }


                        /*
                         * Start the comparison of what method to call
                         */
                        if(diseaseSteps[i-1].method.Equals("OR")) //***************************Verificar si a i le tengo que restar 1 para que vaya de (0 a n-1) en vez de (1 a n)
                        {
                            step = OrCriteria(parameterList);
                            stepList.Add(step);
                            System.Diagnostics.Debug.Write("OR result in: " + step + "\n");
                        }

                        else if(diseaseSteps[i-1].method.Equals("AND")) //***************************Verificar si a i le tengo que restar 1 para que vaya de (0 a n-1) en vez de (1 a n)
                        {
                            step = AndCriteria(parameterList);
                            stepList.Add(step);
                            System.Diagnostics.Debug.Write("AND result in: " + step + "\n");
                        }

                        else if(diseaseSteps[i-1].method.Equals("NMORE")) //***************************Verificar si a i le tengo que restar 1 para que vaya de (0 a n-1) en vez de (1 a n)
                        {
                            step = nOrMoreCriteria(diseaseSteps[i-1].quantityN, parameterList);
                            stepList.Add(step);
                            System.Diagnostics.Debug.Write("NMORE result in: " + step + "\n");
                        }

                        else
                        {
                            //Invalid comparison method
                            errorStatus = "Error: Invalid method (OR, AND, NMORE).";
                            return false;
                        }

                        parameterList.Clear();
                    }

                    else
                    {
                        //Enviar texto de que no hay parámetros para ese paso.
                        errorStatus = "Error: There is no parameters for this step.";
                        return false;
                    }
                }
            }

            else
            {
                //Enviar texto de que no hay pasos en la base de datos para esa enfermedad.
                errorStatus = "Error: There is no steps for this disease.";
                return false;
            }

            return stepList.Last(); //Envío el final de la lista siempre, y en donde la reciba, si es null busco la lista de errores que debí haber guardado aquí o algo asi.
        }

        /* 
         * Method: AndCriteria(List<bool> param)
         * 
         * Parameter: 
         * param - is a list of bool items, to which the operation is going to be applied.  
         * 
         * This method makes an "AND" of all the elements in the received list,
         * returns true if the "AND" is fulfilled (all items in param are true) and
         * returns false if at least one element is false.
         */
        public bool AndCriteria(List<bool> param)
        {
            bool result;

            result = !param.Contains(false);

            return result;
        }

        /* 
         * Method: OrCriteria(List<bool> param)
         * 
         * Parameter: 
         * param - is a list of bool items, to which the operation is going to be applied.  
         * 
         * This method makes an "OR" of all the elements in the received list,
         * returns true if the "OR" is fulfilled (at least one element is true) and
         * returns false if all the elements are false.
         */
        public bool OrCriteria(List<bool> param)
        {
            bool result;
            
            result = param.Contains(true);
         
            return result;       
        }

        /* 
         * Method: nOrMoreCriteria(int n, List<bool> param)
         * 
         * Parameters: 
         * n - integer to specify the number of conditions the list of parameters must fulfill.
         * param - is a list of bool items, to which the operation is going to be applied.  
         * 
         * This method makes an "N or more Criteria" of all the elements in the received list,
         * returns true if at least N elements (or more) are true in the list.
         * returns false if this condition is not fulfilled.
         */
        public bool nOrMoreCriteria(int n, List<bool> param)
        {
            bool result = false;
            int counter = 0;
         
            for (int i = 0; i < param.Count; i++)
            {
                if(param.ElementAt(i).Equals(true))
                {
                    counter++;
                }
            }

            if(counter >= n)
            {
                result = true;
            }

            System.Diagnostics.Debug.Write("Counter de NMore:" + counter); //For testing
            return result;
        }


        /* 
         * Method: comparison()
         */
        public bool comparison(int answer, int value, String comparisonSymbol)
        {
            bool result = false; //Variable that contains the result of the comparison.

            if(comparisonSymbol.Equals(">"))
            {
                result =  greaterThan(answer, value);
            }

            else if(comparisonSymbol.Equals("<"))
            {
                result = smallerThan(answer, value);
            }

            else if (comparisonSymbol.Equals("="))
            {
                result = equalTo(answer, value);
            }

            else
            {
                errorStatus = "Error: Comparison Symbol";
                RedirectToAction("RomeDiagnosisError"); 
            }

            return result;
        }

        /* 
         * Method: greaterThan(int, int)
         */
        public bool greaterThan(int answer, int value)
        {
            return (answer > value); //Answer of the user is greater than the compared value?
        }

        /* 
         * Method: smallerThan(int, int)
         */
        public bool smallerThan(int answer, int value)
        {
            return (answer < value); //Answer of the user is greater than the compared value?
        }

        /* 
         * Method: equalTo(int, int)
         */
        public bool equalTo(int answer, int value)
        {
            return (answer == value); //Answer of the user is greater than the compared value?
        }
    }
}