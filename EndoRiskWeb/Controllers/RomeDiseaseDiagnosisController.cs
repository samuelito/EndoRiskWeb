using EndoRiskWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EndoRiskWeb.Controllers
{
    /*
     * RomeDiseaseDiagnosisController Class - responsible of determine the
     * diagnosis for each disease received.
     * 
     * Methods:
     *      Diagnostic(int IDRomeQuiz, List<String> enfermedades)
     *      questionsCriteria(int romequizID, String enfermedad, List<disease> criterios)
     *      calculateDisease(String disease, List<bool> questionsResults)   
     *      AndCriteria(List<bool> param)
     *      OrCriteria(List<bool> param)
     *      nOrMoreCriteria(int n, List<bool> param)
     *      comparison(int answer, int value, String comparisonSymbol)
     *      greaterThan(int answer, int value)
     *      smallerThan(int answer, int value)
     *      equalTo(int answer, int value) 
     *      
     * @author LMGG
     */
    public class RomeDiseaseDiagnosisController : Controller
    {
        //---------------------------General Variables---------------------------//

        private endoriskContext db = new endoriskContext(); //Instance variable of the database, to perform the search of data.

        private List<String> trueDiseases = new List<String>(); //Lista de las enfermedades que me van dando ciertas
        private List<String> falseDiseases = new List<String>(); //Lista de las enfermedades que me van dando falsas

        //-----------------------------------------------------------------------//


        //--------------------------------Methods--------------------------------//

        /* 
         * Method to make the complete diagnosis of all possible diseases of the current patient.
         * Parameters:
         *      int IDRomeQuiz - number of the current Rome Questionnaire Quiz.
         *      List<String> - list of the diseases to be determined.
         *      
         * Return: The list of true diseases, or the error.    
         */
        public List<String> Diagnostic(int IDRomeQuiz, List<String> enfermedades)
        {
            trueDiseases.Clear();
            falseDiseases.Clear();

            if (enfermedades.Count.Equals(0))
            {
                trueDiseases.Add("ERROR");
                trueDiseases.Add("Error: Diagnostic method did not receive diseases in the list.");
                return trueDiseases;
            }

            else
            {
                for (int i = 0; i < enfermedades.Count; i++)
                {
                    String eachDisease = enfermedades[i];
                    var eachDiseaseCriteria = db.diseases.Where(m => m.disease1.Equals(eachDisease)).ToList();

                    if (eachDiseaseCriteria.Count > 0)
                    {
                        //Call questionsCriteria method:
                        List<bool> criterio = questionsCriteria(IDRomeQuiz, eachDisease, eachDiseaseCriteria); //Envia la enfermedad que esté en la lista en el indice i, y sus criterios.

                        if (criterio.Equals(null)) //To verify there is no errors in the questionsCriteria method.
                        {
                            trueDiseases.Add("ERROR");
                            trueDiseases.Add("Error: Questions criteria cannot be completed.");
                            return trueDiseases;
                        }

                        //Call calculateDisease method:
                        String resultado = calculateDisease(eachDisease, criterio);                       

                        if(resultado.Equals("True")) // La enfermedad resultó cierta.
                        {
                            trueDiseases.Add(eachDisease);
                            System.Diagnostics.Debug.Write(eachDisease + " se añadió a trueDiseases \n");
                        }

                        else if(resultado.Equals("False")) // La enfermedad resultó falsa.
                        {
                            falseDiseases.Add(eachDisease);
                            System.Diagnostics.Debug.Write(eachDisease + " se añadió a falseDiseases \n");
                        }

                        else // Hubo un error al calcular la enfermedad.
                        {
                            trueDiseases.Add("ERROR");
                            trueDiseases.Add(resultado);
                            return trueDiseases;
                        }
                    }

                    else
                    {
                        trueDiseases.Add("ERROR");
                        trueDiseases.Add("Error: No criteria for this disease in the DB.");
                        return trueDiseases;
                    }
                }
            }

            //Testing:
            System.Diagnostics.Debug.Write("Terminé todas: \n");

            System.Diagnostics.Debug.Write("      Ciertas son: \n");
            for (int i = 0; i < trueDiseases.Count; i++)
            {
                System.Diagnostics.Debug.Write(trueDiseases[i] + "\n");
            }

            System.Diagnostics.Debug.Write("      Falsas son: \n");
            for (int i = 0; i < falseDiseases.Count; i++)
            {
                System.Diagnostics.Debug.Write(falseDiseases[i] + "\n");
            }

            System.Diagnostics.Debug.Write("Yayyy!! :D \n");

            //Ahora aquí con las true diseases, envio los diagnósticos.****

            return trueDiseases;
        }

        /* 
         * Method to calculate each question criteria for the disease received.
         * Parameters:
         *      romequizID = integer of the rome quiz number.
         *      enfermedad = string with the name of the disease.
         *      criterios = questions criteria to evaluate the disease.
         *      
         * Return: The list of boolean results of each question criteria comparison.
         */
         public List<bool> questionsCriteria(int romequizID, String enfermedad, List<disease> criterios)
         {
             List<bool> questionsResults = new List<bool>();

             //Set default values:
             int idquestion = 0; //Id de la pregunta que voy a buscar.
             String comparisonSymbol = ""; //Symbolo de comparación.
             int testValue = -1;  //Valor con el que voy a comparar.
             int patientAnswer = -1;  //Valor que contestó el usuario.

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
                     }    
                    
                     else
                     {
                         questionsResults.Clear(); //"Error: No patient answer for a question.";
                         i = criterios.Count + 1; //To "break" the for.                       
                     }
                 }
             }

             else
             {
                 questionsResults.Clear(); //"Error: No Rome answers for this patient.";
             }

             return questionsResults;
         }

         /* 
          * Method to calculate each disease according to parameters.
          * Parameters:
          *      disease = is the text of the disease that should be calculated. (Text string must be equal to the one in the database)
          *      questionsResult = is the list of the results (true or false) of the comparison for each question in order, for the desired disease.
          *      
          * This method determine if the disease received as parameter is true or false, according to its questionsResults
          * and associated steps and parameters of this disease.
          * 
          * Return: Result of the disease or the error message.
          */
         public String calculateDisease(String disease, List<bool> questionsResults)
        {
            bool step = false; //Hold the result of each "step" of the calculation.
            List<bool> stepList = new List<bool>();  //This list will contains the result of each step in the disease diagnosis.
            List<bool> parameterList = new List<bool>();  //This list will contains the result of each parameter in the step.

            stepList.Add(false); //Ensure the list never return empty
            parameterList.Add(false); //Ensure the list never return empty

            int questionNumber = 0; //Number of the question in the disease to be evaluated (Default = 0)
            bool comparedValue = false; //Value to compare the result of the question (Default = false)
            
            if(!db.romesteps.Select(m => m.disease3).Contains(disease)) //Si los steps no contienen esa enfermedad
            {
                return "Error: Disease is not found in steps list."; 
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

                    var parametrosTotalesDisease = db.romeparameters.Where(m => m.disease4.ToString().Equals(disease)).ToList();
                    var stepParameters = parametrosTotalesDisease.Where(m => (int)m.step == currentRealStep).ToList();

                    int sizeOfParams = stepParameters.Count;

                    if(sizeOfParams == 0) //To ensure not to start the comparison of parameter if the list is empty.
                    {
                        return "Error: There is no parameters for this step.";
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

                            /*
                             * Start the comparison to determine if the parameter is a question, a disease, another step, or if there is an undefined parameter(error message)
                             */
                            if(QUE.Equals("Q")) //Verify if the parameter of the step is a question
                            {
                                //String valorCual = item.cual.ToString(); //Convert to string to be sure.

                                questionNumber = Convert.ToInt32(CUAL); //Number of the question in the disease to be evaluated (foreach de item comienza desde 0)
                                
                                int realIndex = questionNumber - 1;                             

                                bool valorComparacionPregunta = questionsResults[realIndex].ToString().Equals(comparedValue.ToString()); 
    
                                parameterList.Add(valorComparacionPregunta); //Add the result of the parameter comparison to the method list.
                            }

                            else if(QUE.Equals("D")) //Verify if the parameter of the step is a disease
                            {
                                if(comparedValue) //If disease has to be true
                                {
                                    for (int m = 0; m < trueDiseases.Count; m++)
                                    {
                                        if(trueDiseases[m].Equals(CUAL))
                                        {
                                            parameterList.Add(true);
                                            m = trueDiseases.Count + 1;
                                        }
                                    }

                                    for (int m = 0; m < falseDiseases.Count; m++)
                                    {
                                        if (falseDiseases[m].Equals(CUAL))
                                        {
                                            parameterList.Add(false);
                                            m = falseDiseases.Count + 1;
                                        }
                                    }
                                }

                                else if(!comparedValue) //If disease has to be false
                                {
                                    for (int n = 0; n < falseDiseases.Count; n++)
                                    {
                                        if (falseDiseases[n].Equals(CUAL))
                                        {
                                            parameterList.Add(true);
                                            n = falseDiseases.Count + 1;
                                        }
                                    }

                                    for (int n = 0; n < trueDiseases.Count; n++)
                                    {
                                        if (trueDiseases[n].Equals(CUAL))
                                        {
                                            parameterList.Add(false);
                                            n = trueDiseases.Count + 1;
                                        }
                                    }
                                }

                                else //Did not fulfill the comparison criteria.
                                {
                                    return "Error: Some required disease have been not calculated before.";
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
                                    }
                                
                                    else
                                    {
                                        parameterList.Add(false);
                                    } 
                                }

                                else
                                {
                                    //Se está intentando accesar un 'step' invalido.
                                    return "Error: Trying to access an invalid step.";
                                }
                            }

                            else
                            {
                                // Parámetro no está definido. 
                                //errorStatus = "Error: Parameter is not defined (Q, S, or D).";
                                return "Error: Parameter is not defined (Q, S, or D).";
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
                        }

                        else if(methodToCompare.Equals("AND")) 
                        {
                            step = AndCriteria(parameterList);
                            stepList.Add(step);
                        }

                        else if(methodToCompare.Equals("NMORE")) 
                        {
                            int quantityN = (int) diseaseSteps[i].quantityN;
                            step = nOrMoreCriteria(quantityN, parameterList);
                            stepList.Add(step);
                        }

                        else
                        {
                            //Invalid comparison method
                            return "Error: Invalid method (OR, AND, NMORE).";
                        }

                        parameterList.Clear();
                    }                   
                } 
                //Aquí terminé con los steps.
            }

            else
            {
                //Enviar texto de que no hay pasos en la base de datos para esa enfermedad.
                return "Error: There is no steps for this disease.";
            }

            if(stepList.Count == 0) //Asegurarme de enviar un valor siempre.
            {
                return "Nothing";
            }

            return stepList.Last().ToString(); //Envío el final de la lista siempre porque es el que contiene el resultado de la enfermedad.
        }

        /* 
         * Method: AndCriteria(List<bool> param)
         * 
         * Parameter: 
         *      param - is a list of bool items, to which the operation is going to be applied.  
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
         *      param - is a list of bool items, to which the operation is going to be applied.  
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
         *      n - integer to specify the number of conditions the list of parameters must fulfill.
         *      param - is a list of bool items, to which the operation is going to be applied.  
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