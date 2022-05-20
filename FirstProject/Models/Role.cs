using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
            Logins = new HashSet<Login>();
        }

        public decimal Roleid { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
    }
}
