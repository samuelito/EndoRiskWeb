using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EndoRiskWeb.Models
{
    /*
     * Documentation: Samuel Feliciano
     * New Model for the Endometriosis Questions Includes:
     * Question ID, Question, Abbreviation, Set of Choices and List of the Choices Values
     */
    public class questions
    {
        public int questionid { get; set; }
        public string question {get; set;}
        public string abbr { get; set; }
        public string set { get; set; }
        public IList<string> choices { get; set; }
      

    }

}