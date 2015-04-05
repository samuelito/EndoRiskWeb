using System;
using System.Collections.Generic;

namespace EndoRiskWeb.Models
{
    public partial class comment
    {
        public long idComment { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> time { get; set; }
    }
}
