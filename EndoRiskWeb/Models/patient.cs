using System;
using System.Collections.Generic;

namespace EndoRiskWeb.Models
{
    public partial class patient
    {
        public int idQuiz { get; set; }
        public int idPatient { get; set; }
        public Nullable<float> risk { get; set; }

        public string severity { get; set; }
        public string verified { get; set; }
        public Nullable<System.DateTime> time { get; set; }
    }
}
