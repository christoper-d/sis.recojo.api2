using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sis.recojo.api.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Buzons = new HashSet<Buzon>();
            Clientes = new HashSet<Cliente>();
            InverseAdministrador = new HashSet<Usuario>();
            Registros = new HashSet<Registro>();
        }

        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public string usuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public string? Puesto { get; set; }
        public string? AreaAsignada { get; set; }
        public int? AdministradorId { get; set; }

        [JsonIgnore]
        public virtual Usuario? Administrador { get; set; }
        public virtual ICollection<Buzon> Buzons { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Usuario> InverseAdministrador { get; set; }
        public virtual ICollection<Registro> Registros { get; set; }
    }
}
