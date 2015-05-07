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

        //-----------------------------------------------------------------------//


        //--------------------------------Methods--------------------------------//

        // GET: RomeDiseaseDiagnosis
        public ActionResult QuestionsCriteria(int quiz, IList<String> enfermedades, IList<disease> criteria)
        {
            //Aqui tengo que calcular lo de las preguntas, voy por la lista de 'enfermedades' y llamando el otro metodo.
            //tengo que generar las listas de truediseases, false diseases y eso :)
            return View(); 
        }


        //LOS ERRORES DE LOS METODOS LOS PUEDO IR PONIENTO EN VIEW BAGS O ALGO ASI*****

        /* 
         * Method to calculate each disease according to parameters.
         * Parameters:
         *      disease = is the text of the disease that should be calculated. (Text string must be equal to the one in the database)
         *      questionsResult = is the list of the results (true or false) of the comparison for each question in order, for the desired disease.
         *      truePreDiseases = string list that contains the name of the prediseases that have been calculated before, an its result was true. (Empty if no pre-diseases are fulfilled)
         *      truePreDiseases = string list that contains the name of the prediseases that have been calculated before, an its result was false. (Empty if no pre-diseases are not fulfilled)
         */
        public bool calculateDisease(String disease, List<bool> questionsResult, List<String> truePreDiseases, List<String> falsePreDiseases)
        {
            bool step = false; //Hold the result of each "step" of the calculation.
            List<bool> finalList = new List<bool>();  //This list will contains the result of each step in the disease diagnosis.
            List<bool> methodList = new List<bool>();  //This list will contains the result of each parameter in the step.

            int questionNumber = 0; //Number of the question in the disease to be evaluated
            bool comparedValue = false; //Value to compare the result of the question (default = false)
            
            var diseaseSteps = db.romesteps.Where(m => m.disease3.Equals(disease)).ToList(); //Get the steps of the 'disease' and pass them to a list      

            if (diseaseSteps.Count != 0) //To ensure not to start the process if the list is empty.
            {
                /* 
                 * Start the for to go for each step of the 'disease'.
                 */
                for(int i = 1; i <= diseaseSteps.Count; i++) 
                {
                    var stepParameters = db.romeparameters.Where(m => m.step == i && m.disease4.Equals(disease)).ToList(); //Get the 'parameters' of the step 'i'
                    
                    if(stepParameters.Count != 0) //To ensure not to start the comparison of parameter if the list is empty.
                    {
                        /* 
                         * Start the for to go for each 'parameter' of the 'step'.
                         */
                        foreach(var item in stepParameters) //Goes for each parameter of the 'step'
                        {
                            //Compared value will be used by all 3 options
                            comparedValue = false; //Value to compare the result of the question (default = false)       
                            if(item.boolValue.Equals("1"))
                            {
                                comparedValue = true;
                            }

                            /*
                             * Start the comparison to determine if the parameter is a question, a disease, another step, or if there is an undefined parameter(error message)
                             */
                            if(item.que.Equals("Q")) // || item.que.Equals("q") || item.que.Equals("Question") || item.que.Equals("question")) //Verify if the parameter of the step is a question
                            {
                                questionNumber = item.cual; //Number of the question in the disease to be evaluated
    
                                methodList.Add(questionsResult[questionNumber].Equals(comparedValue)); //Add the result of the parameter comparison to the method list.

                                System.Diagnostics.Debug.Write("Parámetro número: " + item.idOrder + " de " + item.disease4 + " step " + item.step  + ", es Q y resultado de comparación es: " + methodList.Last()); //For testing
                            }

                            else if(item.que.Equals("D")) //Verify if the parameter of the step is a disease
                            {
                                if(comparedValue) //If disease has to be true
                                {
                                    if(truePreDiseases.Contains(item.cual.ToString())) //If in the list of true prediseases exist the disease parameter, add true
                                    {
                                        methodList.Add(true);
                                    }
                                }

                                else if(!comparedValue) //If disease has to be false
                                {
                                    if(falsePreDiseases.Contains(item.cual.ToString())) //If in the list of false prediseases exist the disease parameter, add true
                                    {
                                        methodList.Add(true);
                                    }
                                }

                                else //Did not fulfill the comparison criteria, therefore result is false
                                {
                                    methodList.Add(false);
                                }

                                System.Diagnostics.Debug.Write("Parámetro número: " + item.idOrder + " de " + item.disease4 + " step " + item.step  + ", es D y resultado de comparación es: " + methodList.Last()); //For testing
                            }

                            else if(item.que.Equals("S")) //Verify if the parameter of the step is another step
                            {
                                if(item.cual < stepParameters.Count) //***************************Verificar si es (menor o igual) o (menor solamente) <tendría que poner un -1 o algo así> porque puede accesar un valor que no esta, o no poder acceder un valor que necesita respectivamente.
                                {
                                    if(finalList[item.cual].Equals(comparedValue))
                                    {
                                        methodList.Add(true);
                                    }
                                
                                    else
                                    {
                                        methodList.Add(false);
                                    } 
                                }

                                else
                                {
                                    //Se está intentando accesar un 'step' invalido.
                                }
                            }

                            else
                            {
                                // Parámetro no está definido. Otro texto más
                            }
                        }

                        /*
                         * Start the comparison of what method to call
                         */
                        if(diseaseSteps[i-1].method.Equals("OR")) //***************************Verificar si a i le tengo que restar 1 para que vaya de (0 a n-1) en vez de (1 a n)
                        {
                            step = OrCriteria(methodList);
                        }

                        else if(diseaseSteps[i-1].method.Equals("AND")) //***************************Verificar si a i le tengo que restar 1 para que vaya de (0 a n-1) en vez de (1 a n)
                        {
                            step = AndCriteria(methodList);
                        }

                        else if(diseaseSteps[i-1].method.Equals("NMORE")) //***************************Verificar si a i le tengo que restar 1 para que vaya de (0 a n-1) en vez de (1 a n)
                        {
                            step = nOrMoreCriteria(diseaseSteps[i].quantityN, methodList);
                        }

                        finalList.Add(step);
                        methodList.Clear();
                    }

                    else
                    {
                        //Enviar texto de que no hay parámetros para ese paso.
                    }
                }
            }

            else
            {
                //Enviar texto de que no hay pasos en la base de datos para esa enfermedad.
            }

            return finalList.Last(); //Envío el final de la lista siempre, y en donde la reciba, si es null busco la lista de errores que debí haber guardado aquí o algo asi.
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

            System.Diagnostics.Debug.Write(counter); //For testing
            return result;
        }
    }
}