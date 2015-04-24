using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndoRiskWeb.Models;

namespace EndoRiskWeb.Controllers
{
    public class searchController : Controller
    {
        // GET: search
        public ActionResult Index()
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

        public ActionResult Result(FormCollection searchValues)
        {
            using (endoriskContext db = new endoriskContext())
            {

                var age = searchValues.Get(1);
                var eth = searchValues.Get(2);
                var ver = searchValues.Get(3);
                var ltr = searchValues.Get(4);
                var sev = searchValues.Get(5);
                string[] symptomsList = searchValues.Get(6).Split(',');
               

                ViewBag.Age = age;
                ViewBag.Eth = eth;
                ViewBag.Ver = ver;
                ViewBag.Ltr = ltr;
                ViewBag.Sev = sev;
                

                return View();
            }
        }

    }
}