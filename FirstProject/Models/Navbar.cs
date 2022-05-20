using System;
using System.Collections.Generic;

#nullable disable

namespace FirstProject.Models
{
    public partial class Navbar
    {
        public Navbar()
        {
            Entities = new HashSet<Entity>();
        }

        public decimal Navbarid { get; set; }
        public string Entityname { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
    }
}
