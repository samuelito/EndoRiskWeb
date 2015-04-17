using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EndoRiskWeb.Models;


namespace EndoRiskWeb.Controllers
{
    public class SearchResultsController : Controller
    {
        private endoriskContext db = new endoriskContext();
        

        public ActionResult Index()
        {
            List<SearchResult> result = new List<SearchResult>();

            SearchResult infoPaciente = new SearchResult();

            infoPaciente.idQuiz = 1001;
            infoPaciente.paciente = 1111;
            infoPaciente.riesgo = (float) 56.32;
            infoPaciente.severidad = "52.34";
            infoPaciente.verificado = "Si";

            foreach(var item in db.endoanswers)
            {
                if(item.idQuiz == 37){
                    infoPaciente.datosPaciente.Add(item.answer);  
                }
            }

            result.Add(infoPaciente);

            return View(result);
        }
    }
}