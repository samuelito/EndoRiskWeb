using System;
using System.Collections.Generic;

namespace EndoRiskWeb.Models
{
    public partial class disease
    {
        public int idDiseases { get; set; }
        public string disease1 { get; set; }
        public Nullable<int> idRomeQuestion { get; set; }
        public string criteria { get; set; }
        public int comparedValue { get; set; }
    }
}
