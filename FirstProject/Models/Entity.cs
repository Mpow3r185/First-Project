using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Entity
    {
        public decimal Entityid { get; set; }
        public decimal? Navbarid { get; set; }
        public string Href { get; set; }

        public virtual Navbar Navbar { get; set; }
    }
}
