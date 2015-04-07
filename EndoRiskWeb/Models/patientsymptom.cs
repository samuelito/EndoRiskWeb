using System;
using System.Collections.Generic;

namespace EndoRiskWeb.Models
{
    /*
     * Documentation: Samuel
     * Includes:
     * Patient -> ID
     * Symptom -> Name
     * hasSymtom -> Value (1 or 0) 
     */
    public partial class patientsymptom
    {
        public int idPatient { get; set; }
        public string symptom { get; set; }

        public int hasSymptom { get; set; }
    }
}
