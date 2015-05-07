using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndoRiskWeb.Models
{
    public class questionsRome
    {
        public int questionID { get; set; }
        public string questionRome { get; set; }
        public string choiceSet { get; set; }
        public IList<string> choices { get; set; }
    }
}