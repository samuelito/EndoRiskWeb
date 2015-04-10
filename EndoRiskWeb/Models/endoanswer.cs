using System;
using System.Collections.Generic;

namespace EndoRiskWeb.Models
{
    public partial class endoanswer
    {
        public int idAnswer { get; set; }
        public int idQuiz { get; set; }
        public int idQuestion { get; set; }
        public string answer { get; set; }
    }
}
