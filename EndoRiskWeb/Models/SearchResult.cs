using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndoRiskWeb.Models
{
    public class searchresult
    {
        public IList<searchdata> resultados { get; set; }

        public IList<endoquestion> variablesPreguntas { get; set; }

        public IList<symptom> variablesSintomas { get; set; }

    }
}