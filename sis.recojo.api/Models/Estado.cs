using System;
using System.Collections.Generic;

namespace sis.recojo.api.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Registros = new HashSet<Registro>();
        }

        public int EstadoId { get; set; }
        public string Estado1 { get; set; } = null!;

        public virtual ICollection<Registro> Registros { get; set; }
    }
}
