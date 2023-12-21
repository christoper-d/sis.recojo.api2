using System;
using System.Collections.Generic;

namespace sis.recojo.api.Models
{
    public partial class Solicitude
    {
        public Solicitude()
        {
            Buzons = new HashSet<Buzon>();
        }

        public int SolicitudId { get; set; }
        public string Solicitud { get; set; } = null!;

        public virtual ICollection<Buzon> Buzons { get; set; }
    }
}
