using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace EndoRiskWeb.Models
{
    public class IndexViewModel
    {
        public IList<questions> quest {get; set;}
        public IList<symptom> symp {get; set;}

    }
}