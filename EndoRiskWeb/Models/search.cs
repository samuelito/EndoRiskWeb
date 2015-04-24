using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndoRiskWeb.Models
{
    public class search
    {
        public IList<endochoice> ethnicitySearch { get; set; }

        public List<symptom> symptomsSearch { get; set; }
    }
}