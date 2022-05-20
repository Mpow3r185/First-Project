using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProject.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            Logins = new HashSet<Login>();
            Patienthistories = new HashSet<Patienthistory>();
            Reviews = new HashSet<Review>();
            Testimonials = new HashSet<Testimonial>();
        }

        public decimal Patientid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public decimal? Age { get; set; }
        public string Gender { get; set; }
        public string Imgpath { get; set; }
        [NotMapped]
        public virtual IFormFile ImgFile { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Disease { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Patienthistory> Patienthistories { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Testimonial> Testimonials { get; set; }
    }
}
