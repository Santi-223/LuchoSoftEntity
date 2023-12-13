using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Role
    {
        public Role()
        {
            RolesPermisos = new HashSet<RolesPermiso>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
        public string DescripcionRol { get; set; } = null!;
        public byte EstadoRol { get; set; }

        public virtual ICollection<RolesPermiso> RolesPermisos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
