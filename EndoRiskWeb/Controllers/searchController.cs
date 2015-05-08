using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndoRiskWeb.Models;
using System.Threading;

namespace EndoRiskWeb.Controllers
{
    public class searchController : Controller
    {
         // GET: search
        public ActionResult SearchPage()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Int32.Parse(User.Identity.Name.Split(',')[1]) == 0)
                {
                     using (endoriskContext db = new endoriskContext()) 
                    {
                        search s = new search();
                        s.symptomsSearch = new List<symptom>();
                        s.ethnicitySearch = new List<endochoice>();

                        var ethnicity = db.endoquestions.Where(m => m.abbr.Equals("EBA")).Select(m=>m.choiceSet).FirstOrDefault().ToString();

                        var listSymptoms = db.symptoms;
                        var listChoices = db.endochoices.Where(m => m.choiceSet.Equals(ethnicity));
                
                        foreach (var item in listSymptoms)
                        {  
                            s.symptomsSearch.Add(item);    
                        }
                
                        foreach (var item in listChoices)
                        {
                            s.ethnicitySearch.Add(item);
                        }

                        return View(s);
                    }
                }

                else { return View("~/Views/Notifications/AccessDenied.cshtml"); }
            }

            else { return View("~/Views/Notifications/AccessDenied.cshtml"); }           
        }

        public ActionResult SearchResult(FormCollection searchValues)
        {
            using (endoriskContext db = new endoriskContext())
            {
                var age = searchValues.Get(1);
                var eth = searchValues.Get(2);
                var ver = searchValues.Get(3);
                var ltr = searchValues.Get(4);
                var sev = searchValues.Get(5);
              
                ViewBag.Test = "Success";
                string [] symptomsList;
                if (searchValues.Count == 6)
                {
                    symptomsList = new string[0];
                }
                else
                {
                    symptomsList = searchValues.Get(6).Split(',');
                }
                 
                searchresult resultData = new searchresult();
                resultData.resultados = new List<searchdata>();
                resultData.variablesPreguntas = new List<endoquestion>();
                resultData.variablesSintomas = new List<symptom>();
                IList<int> allQuices = new List<int>();
                int removed = 0;
       
                //Add the variables to show in the result 
                foreach (var questionItem in db.endoquestions)
                {
                    resultData.variablesPreguntas.Add(questionItem);
                }
                foreach (var symptomItem in db.symptoms)
                {
                    resultData.variablesSintomas.Add(symptomItem);
                }

                /*
                 * Get the AGE Quices
                 */
                using (endoriskContext ageDB = new endoriskContext())
                {
                    //Get the quiz id for the specific age
                    if (!(age.Equals("")))
                    {
                        string ageID = ageDB.endoquestions.Where(m => m.abbr.Equals("AGE")).Select(m => m.idQuestion).FirstOrDefault().ToString();
                        
                        int ageInt = Convert.ToInt32(ageID);
                        var ageQuices = ageDB.endoanswers.Where(m => m.idQuestion == ageInt && m.answer.Equals(age)).Select(m=>m.idQuiz).ToList();
                       
                        //Verficar si no hay nulos!!!!!!!
                        if (ageQuices.Count != 0) 
                        { 
                            //Verficar si no hay nulos!!!!!!!
                            foreach (var item in ageQuices)
                            {             
                                allQuices.Add(item);
                            }
                        }
                        else
                        {
                            ViewBag.Test = "There were no matched patients with age: " + age;
                            RedirectToAction("Result", "Search");
                            
                            return View();
                        }
                    }
                }

                /*
                 * Get the Ethnicity Quices
                 */
                using (endoriskContext eDB = new endoriskContext())
                {
                    //Get the quiz id for the specific ethnicity
                    if (!(eth.Equals("TODOS")))
                    {
                        string ebaID = eDB.endoquestions.Where(m => m.abbr.Equals("EBA")).Select(m => m.idQuestion).FirstOrDefault().ToString();
                        int ebaInt = Convert.ToInt32(ebaID);
                        var ebaQuices = eDB.endoanswers.Where(m => m.idQuestion == ebaInt && m.answer.Equals(eth)).Select(m=>m.idQuiz).ToList();
                        if (ebaQuices.Count != 0)
                        {
                            //If no Quiz id has been added or removed 
                            if (allQuices.Count == 0 && removed != 1)
                            {
                                foreach (var item in ebaQuices)
                                {
                                    allQuices.Add(item);
                                }
                            }
                            else
                            {
                                for (var i = allQuices.Count - 1; i >= 0; i--)
                                {
                                    if (!ebaQuices.Contains(allQuices[i]))       //Remove quizid that are not included 
                                    {
                                        allQuices.RemoveAt(i);
                                        removed = 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Test = "No matched result for ethnicity: " + eth;
                            return View();
                        }
                    }
                }

                /*
                 * Get the Verified Quices
                 */
                using (endoriskContext verDB = new endoriskContext())
                {
                    //Get the quiz id for the specific verified variable
                    if (!(ver.Equals("TODOS")))
                    {
                        IList<int> verIDs = new List<int>();

                        if(ver.Equals("NO")){

                            verIDs = verDB.patients.Where(m => m.verified.Equals("No")).Select(m=>m.idQuiz).ToList();
                        }
                        else{
                            verIDs = verDB.patients.Where(m => !m.verified.Equals("No") && !m.verified.Equals("")).Select(m=>m.idQuiz).ToList();
                                    
                        }
                 
                        if ( verIDs.Count != 0)
                        {
                            //If no Quiz id has been added or removed 
                            if (allQuices.Count == 0 && removed != 1)
                            {
                                foreach (var item in verIDs)
                                {
                                    allQuices.Add(item);
                                }
                            }
                            else
                            {    
                                for (var i = allQuices.Count - 1; i >= 0; i--)
                                {
                                    if (!verIDs.Contains(allQuices[i]))       //Remove quizid that are not included 
                                    {
                                        allQuices.RemoveAt(i);
                                        removed = 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Test = "No result matched Verficied: " + ver;
                            return View();
                        }
                    }
                }
                /*
                 * Get the LifetimeRisk Quices
                 */
                using (endoriskContext ltrDB = new endoriskContext())
                {
                    //Get the quiz id for the risk
                    if (!(ltr.Equals("TODOS")))
                    {
                        float ltrMax = 50.00f;
                        float ltrMin = 0.00f;
                        if (ltr.Equals("ALTO"))
                        {
                            ltrMax = 100.00f;
                            ltrMin = 70.00f;
                        }
                        else if(ltr.Equals("MEDIANO")){
                            ltrMax= 70.00f;
                            ltrMin= 50.00f;
                        }
                        
                        var ltrIDs = ltrDB.patients.Where(m=> m.risk < ltrMax && ltrMin <= m.risk).Select(m=>m.idQuiz).ToList();

                        if (ltrIDs.Count != 0)
                        {
                            //If no Quiz id has been added or removed 
                            if (allQuices.Count == 0 && removed != 1)
                            {
                                foreach (var item in ltrIDs)
                                {
                                    allQuices.Add(item);
                                }
                            }
                            else
                            {
                                for (var i = allQuices.Count - 1; i >= 0; i--)
                                {
                                    if (!ltrIDs.Contains(allQuices[i]))       //Remove quizid that are not included 
                                    {
                                        allQuices.RemoveAt(i);
                                        removed = 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Test = "No hay resuldatos con el riesgo: " + ltr;
                           
                            return View();
                        }
                    }
                }

                /*
                * Get the SeverityRisk Quices
                */
                using (endoriskContext sevDB = new endoriskContext())
                {
                    //Get the quiz id for the Severity
                    if (!(sev.Equals("TODOS")))
                    {
                        
                        var sevIDs = sevDB.patients.Where(m => m.severity.Equals(sev)).Select(m => m.idQuiz).ToList();

                        if (sevIDs.Count != 0)
                        {
                            //If no Quiz id has been added or removed 
                            if (allQuices.Count == 0 && removed != 1)
                            {
                                foreach (var item in sevIDs)
                                {
                                    allQuices.Add(item);
                                }
                            }
                            else
                            {
                                for (var i = allQuices.Count - 1; i >= 0; i--)
                                {
                                    if (!sevIDs.Contains(allQuices[i]))       //Remove quizid that are not included 
                                    {
                                        allQuices.RemoveAt(i);
                                        removed = 1;
                                    }
                                }
                            }
                        }
                        else
                        {
                            ViewBag.Test = "No existen resultados con severidad: " + sev;
                            return View();
                        }
                    }
                }
                
                /*
                 * Add the symptoms quices:
                 * 
                 * if symptomList contains symptoms -> add the symptoms
                 * else goto -> add all quices.
                 */
                if (symptomsList.Length != 0)
                {
                    using (endoriskContext dbSym = new endoriskContext()) { 
                    /*
                     *  If allQuices == 0 and removed ==0
                     *      add all quices with specific symptoms to the list:
                     */
                    if(allQuices.Count == 0 && removed == 0){

                        var allSymptomsId = dbSym.patientsymptoms.Select(m => m.idQuiz).Distinct().ToList();
                        
                        for (int j = 0; j < allSymptomsId.Count; j++) {
                            int tempId = allSymptomsId[j];
                            
                            var allSymptoms = db.patientsymptoms.Where(q => q.idQuiz == tempId).ToList();
                            var symptomCounter = 0;
                            for (int i = 0; i < symptomsList.Length; i++)
                            {
                                foreach (var item in allSymptoms)
                                {
                                    if (symptomsList[i].Equals(item.symptom) && item.hasSymptom == 1)
                                    {
                                        symptomCounter++;
                                        break;
                                    }
                                    else if (symptomsList[i].Equals(item.symptom) && item.hasSymptom == 0)
                                    {
                                        i = symptomsList.Length + 1;
                                        break;
                                    }
                                }
                            }
                            if (symptomCounter == symptomsList.Length)
                            {
                                allQuices.Add(allSymptomsId[j]);
                            }
                        }
                    }
                    /*
                     * Else:
                     *      Get all the symptoms of the quices in "allQuices"    
                     *      Eliminate the Quices that dont match the symptoms:
                     */
                    else
                    {
                        //var allSymptomsId = dbSym.patientsymptoms.Select(m => m.idQuiz).Distinct().ToList();
                        for(int j= allQuices.Count-1 ; j>=0 ;j--)
                        {
                            var quiz = allQuices[j];
                            var allSymptoms = db.patientsymptoms.Where(q => q.idQuiz == quiz).ToList();
                            var symptomCounter = 0;
                            for (int i = 0; i < symptomsList.Length; i++)
                            {
                                foreach (var item in allSymptoms)
                                {
                                    if (symptomsList[i].Equals(item.symptom) && item.hasSymptom == 1)
                                    {
                                        symptomCounter++;
                                        break;
                                    }
                                    else if (symptomsList[i].Equals(item.symptom) && item.hasSymptom == 0)
                                    {
                                        i = symptomsList.Length + 1;
                                        break;
                                    }
                                }
                            }
                            if (symptomCounter != symptomsList.Length)
                            {
                                allQuices.RemoveAt(j);
                                removed = 1;
                            }
                        }

                    }
                }
                }

                /*
                 * Go through the list of allQuices and gather the required info
                 */
                if (allQuices.Count != 0)
                {
                    using (endoriskContext resultDB = new endoriskContext())
                    {
                        foreach (var idquiz in allQuices)
                        {
                            searchdata tempPatient = new searchdata();
                            tempPatient.datosPaciente = new patient();
                            tempPatient.respuestasPaciente = new List<endoanswer>();
                            tempPatient.sintomasPaciente = new List<patientsymptom>();
                            var patientsData = resultDB.patients.Where(m => m.idQuiz == idquiz).ToList();
                            var patientsAnswers = resultDB.endoanswers.Where(m => m.idQuiz == idquiz).ToList(); ;
                            var patientSymptoms = resultDB.patientsymptoms.Where(m => m.idQuiz == idquiz).ToList();

                            tempPatient.datosPaciente = patientsData[0];

                            foreach(var item in patientsAnswers)
                            {
                                tempPatient.respuestasPaciente.Add(item);
                            }

                            foreach (var item in patientSymptoms)
                            {
                                tempPatient.sintomasPaciente.Add(item);
                            }

                            resultData.resultados.Add(tempPatient);
                        }                   
                    }
                }
                else
                {   //add all quices from the database:
                    if(allQuices.Count == 0 && removed == 0)
                    {
                        using (endoriskContext resultDB = new endoriskContext())
                        {
                            var quicesAll = db.patients.Select(m => m.idQuiz);
                            foreach (var idquiz in quicesAll)
                            {
                                searchdata tempPatient = new searchdata();
                                tempPatient.datosPaciente = new patient();
                                tempPatient.respuestasPaciente = new List<endoanswer>();
                                tempPatient.sintomasPaciente = new List<patientsymptom>();
                                var patientsData = resultDB.patients.Where(m => m.idQuiz == idquiz).ToList();
                                var patientsAnswers = resultDB.endoanswers.Where(m => m.idQuiz == idquiz).ToList(); ;
                                var patientSymptoms = resultDB.patientsymptoms.Where(m => m.idQuiz == idquiz).ToList();

                                tempPatient.datosPaciente = patientsData[0];

                                foreach (var item in patientsAnswers)
                                {
                                    tempPatient.respuestasPaciente.Add(item);
                                }

                                foreach (var item in patientSymptoms)
                                {
                                    tempPatient.sintomasPaciente.Add(item);
                                }

                                resultData.resultados.Add(tempPatient);
                            }
                        }
                    }
                }
               
                
                return View(resultData);
            }
        }

    }
}