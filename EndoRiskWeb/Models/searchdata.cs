using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndoRiskWeb.Models
{
    public class searchdata
    {

        public patient datosPaciente { get; set; }

        public IList<endoanswer> respuestasPaciente { get; set; }

        public IList<patientsymptom> sintomasPaciente { get; set; }
    }
}