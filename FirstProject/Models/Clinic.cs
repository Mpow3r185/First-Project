using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProject.Models
{
    public partial class Clinic
    {
        public Clinic()
        {
            Employees = new HashSet<Employee>();
        }

        public decimal Clinicid { get; set; }
        public string Clincname { get; set; }
        public string Phonenumber { get; set; }
        public string ClinicImg { get; set; }
        [NotMapped]
        public virtual IFormFile ClinicImgFile { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
