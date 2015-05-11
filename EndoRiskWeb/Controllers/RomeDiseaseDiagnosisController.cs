using EndoRiskWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndoRiskWeb.Controllers
{
    /*
     * RomeDiseaseDiagnosisController Class - responsible of calculate question
     * criteria, and determine true or false diseases.
     * 
     * Methods:
     *      
     *      
     * @author Luz M. González González
     */
    public class RomeDiseaseDiagnosisController : Controller
    {
        //---------------------------General Variables---------------------------//

        private endoriskContext db = new endoriskContext(); //Instance variable of the database, to perform the search of data.

        private static int romequizID = 0; //ID del quiz de Rome 
        private static List<String> listOfDiseases = new List<String>(); //Lista de todas las enfermedades que recibe que se deben calcular
        //private static List<disease> listOfCriteria = new List<disease>();  //Lista de todos los criterios para calcular estas enfermedades. (esto lo podía buscar aquí también, pero verifico luego qué me conviene más)

        private static List<bool> questionsResults = new List<bool>(); //Lista de los resultados de cada comparacion de las preguntas para cada enfermedad en orden.

        private  List<String> truePreDiseases = new List<String>(); //Lista de las enfermedades que me van dando ciertas
        private  List<String> falsePreDiseases = new List<String>(); //Lista de las enfermedades que me van dando falsas

        private static String errorStatus = "";

        //-----------------------------------------------------------------------//


        //--------------------------------Methods--------------------------------//

        /* 
         * Method to make the complete diagnosis of all possible diseases of the current patient.
         * Parameters:
         *      
         */
        public List<String> Diagnostic(int IDRomeQuiz, List<String> enfermedades)
        {
            romequizID = IDRomeQuiz;
            listOfDiseases = enfermedades;

            truePreDiseases.Clear();
            falsePreDiseases.Clear();

            truePreDiseases.Add("");
            falsePreDiseases.Add("");
           
            System.Diagnostics.Debug.Write("Quiz de Rome es: " + romequizID + "\n");

            if (enfermedades.Count.Equals(0))
            {
                errorStatus = "Error: Diagnostic did not receive diseases in the list.";
                truePreDiseases.Add("ERROR");
                truePreDiseases.Add(errorStatus);
                return truePreDiseases;
            }

            else
            {
                for (int i = 0; i < listOfDiseases.Count; i++)
                {
                    String eachDisease = listOfDiseases[i];
                    var eachDiseaseCriteria = db.diseases.Where(m => m.disease1.Equals(eachDisease)).ToList();

                    if (eachDiseaseCriteria.Count > 0)
                    {
                        //Call questionsCriteria method:
                        questionsCriteria(eachDisease, eachDiseaseCriteria); //Envia la enfermedad que esté en la lista en el indice i, y sus criterios.
                        System.Diagnostics.Debug.Write("\n Terminé questionCriteria de: " + eachDisease + "\n");

                        if (!errorStatus.Equals("")) //To verify there is no errors in the questionsCriteria method (no puedo darle redirect allá, porque ese método no es ActionResult.
                        {
                            truePreDiseases.Add("ERROR");
                            truePreDiseases.Add(errorStatus);
                            return truePreDiseases;
                        }

                        //Call calculateDisease method:
                        bool resultado = calculateDisease(eachDisease);                       

                        if (!errorStatus.Equals("")) //To verify there is no errors in the calculateDisease method.
                        {
                            truePreDiseases.Add("ERROR");
                            truePreDiseases.Add(errorStatus);
                            return truePreDiseases;
                        }

                        else if(resultado)
                        {
                            truePreDiseases.Add(eachDisease);
                            System.Diagnostics.Debug.Write(eachDisease + " se añadió a trueDiseases \n");
                        }

                        else
                        {
                            falsePreDiseases.Add(eachDisease);
                            System.Diagnostics.Debug.Write(eachDisease + " se añadió a falseDiseases \n");
                        }

                        System.Diagnostics.Debug.Write("Terminé calculateDisease de: " + eachDisease + " y resultó ser: " + resultado.ToString() + "\n");
                    }

                    else
                    {
                        errorStatus = "Error: No criteria for this disease in the DB.";
                        truePreDiseases.Add("ERROR");
                        truePreDiseases.Add(errorStatus);
                        return truePreDiseases;
                    }
                }
            }

            System.Diagnostics.Debug.Write("Terminé todas: \n");

            System.Diagnostics.Debug.Write("      Ciertas son: \n");
            for (int i = 0; i < truePreDiseases.Count; i++)
            {
                System.Diagnostics.Debug.Write(truePreDiseases[i] + "\n");
            }

            System.Diagnostics.Debug.Write("      Falsas son: \n");
            for (int i = 0; i < falsePreDiseases.Count; i++)
            {
                System.Diagnostics.Debug.Write(falsePreDiseases[i] + "\n");
            }

            System.Diagnostics.Debug.Write("Yayyy!! :D \n");

            //Ahora aquí con las true diseases, envio los diagnósticos.****

            return truePreDiseases;
        }

        /* 
         * Method to calculate each question criteria for the disease received.
         * Parameters:
         */
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
                         //System.Diagnostics.Debug.Write("Resultado pregunta: " + i + " añadido para " + enfermedad + " es: " + questionsResults[i] + "\n");
                     }    
                    
                     else
                     {
                         errorStatus = "Error: No patient answer for a question.";
                         i = criterios.Count + 1; //To "break" the for.                       
                     }

                     //System.Diagnostics.Debug.Write("\n");
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

            stepList.Add(false); //Ensure the list never return empty
            parameterList.Add(false); //Ensure the list never return empty

            int questionNumber = 0; //Number of the question in the disease to be evaluated (Default = 0)
            bool comparedValue = false; //Value to compare the result of the question (Default = false)
            
            if(!db.romesteps.Select(m => m.disease3).Contains(disease)) //Si los steps contienen esa enfermedad
            {
                errorStatus = "Error: Disease is not fount in steps list.";
                return false;   
            }

            var diseaseSteps = db.romesteps.Where(m => m.disease3.Equals(disease)).ToList(); //Get the steps of the 'disease' and pass them to a list          

            if (diseaseSteps.Count != 0) //To ensure not to start the process if the list of steps is empty.
            {
                stepList.Clear();
                /* 
                 * Start the for to go for each step of the 'disease'.
                 */
                for(int i = 0; i < diseaseSteps.Count; i++) 
                {
                    int currentRealStep = i + 1;

                    //var stepParameters = db.romeparameters.Where(m => m.step.Equals(currentRealStep) && m.disease4.Equals(disease)).ToList(); //Get the 'parameters' of the step 'i'
                    var parametrosTotalesDisease = db.romeparameters.Where(m => m.disease4.ToString().Equals(disease)).ToList();
                    var stepParameters = parametrosTotalesDisease.Where(m => (int)m.step == currentRealStep).ToList();

                    int sizeOfParams = stepParameters.Count;

                    System.Diagnostics.Debug.Write("\nPara enfermedad " + disease + ", step " + currentRealStep + " hay " + sizeOfParams + " parámetros. \n"); //For testing

                    if(sizeOfParams == 0) //To ensure not to start the comparison of parameter if the list is empty.
                    {
                        //Enviar texto de que no hay parámetros para ese paso.
                        errorStatus = "Error: There is no parameters for this step.";
                        System.Diagnostics.Debug.Write("Hubo error en: " + errorStatus + "\n"); //For testing
                        return false;
                    }
                    
                    else //Else continue normally
                    {
                        parameterList.Clear();

                        /* 
                         * Start the for to go for each 'parameter' of the 'step'.
                         */
                        foreach(var item in stepParameters) //Goes for each parameter of the 'step'
                        {
                            /*
                             * Set the compare value of each step, all parameters needs this value in the same way
                             */
                            //Compared value will be used by all 3 options
                            comparedValue = false; //Value to compare the result of the question (default = false) 
      
                            if(item.boolValue.ToString().Equals("1"))
                            {
                                comparedValue = true;
                            }

                            String QUE = item.que.ToString();
                            String CUAL = item.cual.ToString();

                            System.Diagnostics.Debug.Write("Value to compare for parameter: " + comparedValue + "\n"); //For testing

                            /*
                             * Start the comparison to determine if the parameter is a question, a disease, another step, or if there is an undefined parameter(error message)
                             */
                            if(QUE.Equals("Q")) //Verify if the parameter of the step is a question
                            {
                                //String valorCual = item.cual.ToString(); //Convert to string to be sure.

                                questionNumber = Convert.ToInt32(CUAL); //Number of the question in the disease to be evaluated (foreach de item comienza desde 0)
                                
                                int realIndex = questionNumber - 1;

                                System.Diagnostics.Debug.Write("Accessing index in questionsResult[" + realIndex + "], Cual = : " + questionNumber + "\n"); //For testing                              

                                bool valorComparacionPregunta = questionsResults[realIndex].ToString().Equals(comparedValue.ToString()); //Compara con el valor que se supone que sea.&&&&&&&&&&&&&&&&&&&&&&&&&Verificar si dejo comparación con Equals

                                System.Diagnostics.Debug.Write("Comparing value: " + comparedValue.ToString() + " con valor en resultado de pregs: " + valorComparacionPregunta.ToString() + "\n"); //For testing 
    
                                parameterList.Add(valorComparacionPregunta); //Add the result of the parameter comparison to the method list.

                                System.Diagnostics.Debug.Write("Parámetro Q número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString()  + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                            }

                            else if(QUE.Equals("D")) //Verify if the parameter of the step is a disease
                            {
                                if(comparedValue) //If disease has to be true
                                {
                                    for (int m = 0; m < truePreDiseases.Count; m++)
                                    {
                                        if(truePreDiseases[m].Equals(CUAL))
                                        {
                                            parameterList.Add(true);
                                            m = truePreDiseases.Count + 1;
                                            System.Diagnostics.Debug.Write("Parámetro D (true) número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString() + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                                        }
                                    }

                                    for (int m = 0; m < falsePreDiseases.Count; m++)
                                    {
                                        if (falsePreDiseases[m].Equals(CUAL))
                                        {
                                            parameterList.Add(false);
                                            m = falsePreDiseases.Count + 1;
                                            System.Diagnostics.Debug.Write("Parámetro D (false) número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString() + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                                        }
                                    }
                                        //if (truePreDiseases.Any(str => str.Contains(CUAL)))
                                        ////if(truePreDiseases.Contains(CUAL)) //If in the list of true prediseases exist the disease parameter, add true
                                        //{
                                        //    parameterList.Add(true);
                                        //    System.Diagnostics.Debug.Write("Parámetro D (true) número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString() + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                                        //}
                                }

                                else if(!comparedValue) //If disease has to be false
                                {
                                    for (int n = 0; n < falsePreDiseases.Count; n++)
                                    {
                                        if (falsePreDiseases[n].Equals(CUAL))
                                        {
                                            parameterList.Add(true);
                                            n = falsePreDiseases.Count + 1;
                                            System.Diagnostics.Debug.Write("Parámetro D (true) número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString() + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                                        }
                                    }

                                    for (int n = 0; n < truePreDiseases.Count; n++)
                                    {
                                        if (truePreDiseases[n].Equals(CUAL))
                                        {
                                            parameterList.Add(false);
                                            n = truePreDiseases.Count + 1;
                                            System.Diagnostics.Debug.Write("Parámetro D (false) número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString() + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                                        }
                                    }
                                    //if (falsePreDiseases.Any(str => str.Contains(CUAL)))
                                    ////if(falsePreDiseases.Contains(CUAL)) //If in the list of false prediseases exist the disease parameter, add true
                                    //{
                                    //    parameterList.Add(false);
                                    //    System.Diagnostics.Debug.Write("Parámetro D (false) número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString() + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                                    //}
                                }

                                else //Did not fulfill the comparison criteria.
                                {
                                    errorStatus = "Error: Some required disease have been not calculated before. \n";
                                    System.Diagnostics.Debug.Write("Hubo error en: " + errorStatus + "\n"); //For testing
                                    return false;
                                }
                            }

                            else if(QUE.Equals("S")) //Verify if the parameter of the step is another step
                            {
                                int cual = Convert.ToInt32(CUAL);

                                int realStep = cual - 1;

                                if(realStep < i) //Lower than i, because i is the current step.
                                {
                                    if(stepList[realStep].ToString().Equals(comparedValue.ToString()))
                                    {
                                        parameterList.Add(true);
                                        System.Diagnostics.Debug.Write("Parámetro S número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString() + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                                    }
                                
                                    else
                                    {
                                        parameterList.Add(false);
                                        System.Diagnostics.Debug.Write("Parámetro S número: " + item.idOrder.ToString() + " de " + item.disease4.ToString() + " step " + item.step.ToString() + ", resultado es: " + parameterList.Last().ToString() + "\n"); //For testing
                                    } 
                                }

                                else
                                {
                                    //Se está intentando accesar un 'step' invalido.
                                    errorStatus = "Error: Trying to access an invalid step.";
                                    System.Diagnostics.Debug.Write("Hubo error en: " + errorStatus + "\n"); //For testing
                                    return false;
                                }
                            }

                            else
                            {
                                // Parámetro no está definido. 
                                errorStatus = "Error: Parameter is not defined (Q, S, or D).";
                                System.Diagnostics.Debug.Write("Hubo error en: " + errorStatus + "\n"); //For testing
                                return false;
                            }
                        }                         
                        //Aquí terminé con los parámetros


                        /*
                         * Start the comparison of what method to call for the current STEP
                         */
                        String methodToCompare = diseaseSteps[i].method.ToString();

                        if(methodToCompare.Equals("OR")) //
                        {
                            step = OrCriteria(parameterList);
                            stepList.Add(step);
                            System.Diagnostics.Debug.Write("Step " + currentRealStep + " comparison OR result is: " + step.ToString() + "\n");
                        }

                        else if(methodToCompare.Equals("AND")) 
                        {
                            step = AndCriteria(parameterList);
                            stepList.Add(step);
                            System.Diagnostics.Debug.Write("Step " + currentRealStep + " comparison AND result is: " + step.ToString() + "\n");
                        }

                        else if(methodToCompare.Equals("NMORE")) 
                        {
                            int quantityN = (int) diseaseSteps[i].quantityN;
                            step = nOrMoreCriteria(quantityN, parameterList);
                            stepList.Add(step);
                            System.Diagnostics.Debug.Write("Step " + currentRealStep + " comparison NMORE result is: " + step.ToString() + "\n");
                        }

                        else
                        {
                            //Invalid comparison method
                            errorStatus = "Error: Invalid method (OR, AND, NMORE).";
                            System.Diagnostics.Debug.Write("Hubo error en: " + errorStatus + "\n"); //For testing
                            return false;
                        }

                        parameterList.Clear();
                    }                   
                } 
                //Aquí terminé con los steps.
            }

            else
            {
                //Enviar texto de que no hay pasos en la base de datos para esa enfermedad.
                errorStatus = "Error: There is no steps for this disease.";
                System.Diagnostics.Debug.Write("Hubo error en: " + errorStatus + "\n"); //For testing
                return false;
            }

            if(stepList.Count == 0) //Asegurarme de enviar un valor siempre.
            {
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