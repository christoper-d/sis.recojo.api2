using System;
using System.Collections.Generic;

namespace sis.recojo.api.Models
{
    public partial class Buzon
    {
        public int BuzonId { get; set; }
        public int UsuarioId { get; set; }
        public int SolicitudId { get; set; }

        public virtual Solicitude Solicitud { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
