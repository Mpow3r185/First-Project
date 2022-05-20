using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Testimonial
    {
        public decimal Testytid { get; set; }
        public decimal? Patientid { get; set; }
        public string Testimonialcontent { get; set; }
        public string Status { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
