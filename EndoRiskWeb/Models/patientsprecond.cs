using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EndoRiskWeb.Models
{
    public class patientsprecond
    {
        public int idCondition { get; set; }
        public int idQuiz { get; set; }
        public string preCondition { get; set; }
        public string preAbbr { get; set; }
        public int haspreCond { get; set; }
    }
}