using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Login
    {
        public decimal Loginid { get; set; }
        public string Email { get; set; }
        public string Passwordd { get; set; }
        public decimal? Roleid { get; set; }
        public decimal? Patientid { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Role Role { get; set; }
    }
}
