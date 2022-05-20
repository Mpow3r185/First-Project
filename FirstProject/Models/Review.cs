using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Review
    {
        public decimal Revid { get; set; }
        public decimal? Patientid { get; set; }
        public string Reviewcontent { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
