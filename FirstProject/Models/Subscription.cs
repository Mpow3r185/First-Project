using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Subscription
    {
        public Subscription()
        {
            Bills = new HashSet<Bill>();
        }

        public decimal Subid { get; set; }
        public string SubType { get; set; }
        public string Cost { get; set; }
        public string Service1 { get; set; }
        public string Service2 { get; set; }
        public string Service3 { get; set; }
        public string Service4 { get; set; }
        public string Service5 { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
