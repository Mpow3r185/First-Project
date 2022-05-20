using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Bill
    {
        public decimal Billid { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Appointmentid { get; set; }
        public DateTime? Billtime { get; set; }
        public string Status { get; set; }
        public decimal? Paymentid { get; set; }
        public decimal? Subid { get; set; }
        public decimal? Patientid { get; set; }
        public virtual Patient Patient { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Subscription Sub { get; set; }
    }
}
