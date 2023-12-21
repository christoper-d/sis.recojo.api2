using System;
using System.Collections.Generic;

namespace sis.recojo.api.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Registros = new HashSet<Registro>();
        }

        public string Ruc { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Ubicacion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
        public virtual ICollection<Registro> Registros { get; set; }
    }
}
