using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models
{
    public partial class Attendance
    {
        public decimal Attenid { get; set; }
        public DateTime? Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        public decimal? Empid { get; set; }

        public virtual Employee Emp { get; set; }
    }
}
