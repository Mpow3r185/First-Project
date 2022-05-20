using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models
{
    public class JoinTable
    {
        public Appointment appointment { get; set;}
        public Employee employee { get; set; }
        public Patient patient{ get; set; }
        public Bill bill { get;  set; }
        public Clinic clinic { get;  set; }
    }
}
