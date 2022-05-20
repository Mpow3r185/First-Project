using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProject.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Appointments = new HashSet<Appointment>();
            Attendances = new HashSet<Attendance>();
        }

        public decimal Empid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Specialist { get; set; }
        public decimal? Salary { get; set; }
        public DateTime? Hiredate { get; set; }
        public string Imgpath { get; set; }
        [NotMapped]
        public virtual IFormFile ImgFile { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public decimal? Roleid { get; set; }
        public decimal? Clinicid { get; set; }
        public string Abouthim { get; set; }

        public virtual Clinic Clinic { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
