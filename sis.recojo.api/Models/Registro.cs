using System;
using System.Collections.Generic;

namespace sis.recojo.api.Models
{
    public partial class Registro
    {
        public int RegistroId { get; set; }
        public string Ruc { get; set; } = null!;
        public int UsuarioId { get; set; }
        public int EstadoId { get; set; }

        public virtual Estado Estado { get; set; } = null!;
        public virtual Cliente RucNavigation { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
