using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class DepartmentAndEmployee
    {
        public decimal Empid { get; set; }
        public string Fname { get; set; }
        public decimal? Salary { get; set; }
        public string Depname { get; set; }
    }
}
