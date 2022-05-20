using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Bills = new HashSet<Bill>();
        }

        public decimal Paymentid { get; set; }
        public string Paymentnumber { get; set; }
        public string Cvv { get; set; }
        public string Placeholder { get; set; }
        public string Mm { get; set; }
        public string Yy { get; set; }
        public decimal? Money { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
