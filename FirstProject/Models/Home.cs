using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace FirstProject.Models
{
    public partial class Home
    {
        public decimal Homeid { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        public virtual IFormFile LogoFile { get; set; }
        public string Websitename { get; set; }
        public string Imgslider { get; set; }
        [NotMapped]
        public virtual IFormFile ImgFileSlider { get; set; }
        public string Textimg { get; set; }

        public string Openingday { get; set; }
        public string Openinghour { get; set; }
    }
}
