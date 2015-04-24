using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndoRiskWeb.Models
{
    public class searchresult
    {
        public int idQuiz { get; set; }

        public int paciente { get; set; }

        public string verificado { get; set; }

        public float riesgo { get; set; }

        public string severidad { get; set; }

        public IList<string> datosPaciente { get; set; }
    }
}