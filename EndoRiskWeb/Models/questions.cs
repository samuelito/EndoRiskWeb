using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EndoRiskWeb.Models
{
    public class questions
    {
        public string question {get; set;}
        public string choices { get; set; }

        public void set(string a, string b){
            this.choices = b;
            this.question = a;
        }

    }

}