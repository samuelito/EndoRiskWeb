using System;
using System.Collections.Generic;

namespace EndoRiskWeb.Models
{
    public partial class romeanswer
    {
        public int idRomeQuiz { get; set; }
        public int idRomeQuestion { get; set; }
        public Nullable<int> answer { get; set; }
    }
}
