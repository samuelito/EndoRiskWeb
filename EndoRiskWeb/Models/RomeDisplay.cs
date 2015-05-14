using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndoRiskWeb.Models
{
    public class romeDisplay
    {
        public IList<questionsRome> questions { get; set; }
        public IList<string> diseasesList { get; set; }
        public IList<int> numberList { get; set; }
    }
}