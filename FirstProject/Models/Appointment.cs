using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Bills = new HashSet<Bill>();
        }

        public decimal Appointmentid { get; set; }
        public string Healthcase { get; set; }
        public string Status { get; set; }
        public DateTime? Appointmenttime { get; set; }
        public decimal? Empid { get; set; }
        public decimal? Patientid { get; set; }

        public virtual Employee Emp { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }

        public static implicit operator Appointment(string v)
        {
            throw new NotImplementedException();
        }
    }
}
